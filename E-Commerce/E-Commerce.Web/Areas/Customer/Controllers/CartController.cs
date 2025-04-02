using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;
using E_Commerce.Entities.Models;
using E_Commerce.Web.ViewModels.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using Utilities;

namespace E_Commerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private string GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
        public IActionResult Index()
        {
            var shoppingCartVM = new ShoppingCartVM
            {
                ShoppingCarts = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == GetCurrentUserId(), new[] { "Product" })
            };
            shoppingCartVM.TotalPrice = shoppingCartVM.ShoppingCarts.Select(e => e.Product.Price * e.Count).Sum();
            return View(shoppingCartVM);
        }

        public IActionResult PlusQuantity(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetOne(e => e.CartId == cartId);
            _unitOfWork.ShoppingCarts.IncreaseCount(cart, 1);
            _unitOfWork.Complete();

            SetSessionNumberOfCarts();

            return RedirectToAction("Index");
        }

        public IActionResult MinusQuantity(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetOne(e => e.CartId == cartId);
            if (cart.Count > 1)
            {
                _unitOfWork.ShoppingCarts.DecreaseCount(cart, 1);
                _unitOfWork.Complete();

                SetSessionNumberOfCarts();
                
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Minimum Quantity Is 1" });
            }

        }
        [HttpPost]
        public IActionResult Delete(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetOne(e => e.CartId == cartId);
            if (cart != null)
            {
                _unitOfWork.ShoppingCarts.Delete(cart);
                _unitOfWork.Complete();

                SetSessionNumberOfCarts();

                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Item not found!" });
        }

        private void SetSessionNumberOfCarts()
        {
            var count = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == GetCurrentUserId()).Select(e => e.Count).Sum();
            HttpContext.Session.SetInt32(Sessions.SessionKey, count);

        }
        [HttpGet]
        public IActionResult Summary()
        {
            SummaryVM summaryVM = new SummaryVM
            {
                ShoppingCarts = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == GetCurrentUserId(), new[] { "Product" }),
                OrderHeader = new()
            };
            
            summaryVM.OrderHeader.ApplicationUserId = GetCurrentUserId();
            summaryVM.OrderHeader.ApplicationUser = _unitOfWork.Users.GetOne(e => e.Id == GetCurrentUserId());
            summaryVM.OrderHeader.Name = summaryVM.OrderHeader.ApplicationUser.Name;
            summaryVM.OrderHeader.PhoneNumber = summaryVM.OrderHeader.ApplicationUser.PhoneNumber;
            summaryVM.OrderHeader.City = summaryVM.OrderHeader.ApplicationUser.City;
            summaryVM.OrderHeader.Address = summaryVM.OrderHeader.ApplicationUser.Address;

            summaryVM.OrderHeader.TotalPrice = summaryVM.ShoppingCarts.Select(e => e.Product.Price * e.Count).Sum();

            return View(summaryVM);
        }

        // when click "Place Order"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary(SummaryVM summaryVM)
        {
            
            // Save OrderHeader Data
            summaryVM.ShoppingCarts = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == GetCurrentUserId(), new[] { "Product" });
            summaryVM.OrderHeader.ApplicationUserId = GetCurrentUserId();
            summaryVM.OrderHeader.ApplicationUser = _unitOfWork.Users.GetOne(e => e.Id == GetCurrentUserId());

            summaryVM.OrderHeader.OrderStatus = OrderStauts.Pending;
            summaryVM.OrderHeader.PaymentStatus = OrderStauts.Pending;
            summaryVM.OrderHeader.OrderDate = DateTime.Now;
            summaryVM.OrderHeader.TotalPrice = summaryVM.ShoppingCarts.Select(e => e.Product.Price * e.Count).Sum();

            _unitOfWork.OrderHeaders.Add(summaryVM.OrderHeader);
            _unitOfWork.Complete();


            // Save OrderDetails Data
            foreach (var product in  summaryVM.ShoppingCarts)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderId = summaryVM.OrderHeader.Id,
                    ProductId = product.ProductId,
                    Count = product.Count,
                    Price = product.Product.Price
                };
                _unitOfWork.OrderDetails.Add(orderDetails);
            }
            _unitOfWork.Complete();


            // Stripe
            var domaion = "https://localhost:7204/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domaion + $"Customer/Cart/OrderConfirmation?id={summaryVM.OrderHeader.Id}",
                CancelUrl = domaion + "Customer/Cart/Index"
            };

            foreach (var item in summaryVM.ShoppingCarts)
            {
                var sessionLineOption = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineOption);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            summaryVM.OrderHeader.SessionId = session.Id;
            _unitOfWork.Complete();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeaders.GetOne(e => e.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            if (session.PaymentStatus == "paid")
            {
                _unitOfWork.OrderHeaders.UpdateOrderStatus(id, OrderStauts.Approved, OrderStauts.Approved);
                orderHeader.PaymenteDate = DateTime.Now;
                orderHeader.PaymentIntentId = session.PaymentIntentId;

                _unitOfWork.Complete();
            }

            // Remove From Shopping Cart Table after user confirm order
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCarts.DeleteRange(shoppingCarts);
            _unitOfWork.Complete();
            return View(id);
        }


    }
}
