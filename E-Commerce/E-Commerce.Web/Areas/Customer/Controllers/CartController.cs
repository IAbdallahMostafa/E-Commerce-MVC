using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;
using E_Commerce.Web.ViewModels.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return RedirectToAction("Index");
        }

        public IActionResult MinusQuantity(int cartId)
        {
            var cart = _unitOfWork.ShoppingCarts.GetOne(e => e.CartId == cartId);
            if (cart.Count > 1)
            {
                _unitOfWork.ShoppingCarts.DecreaseCount(cart, 1);
                _unitOfWork.Complete();
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
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Item not found!" });
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
            
            summaryVM.ShoppingCarts = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == GetCurrentUserId(), new[] { "Product" });
            summaryVM.OrderHeader.ApplicationUserId = GetCurrentUserId();
            summaryVM.OrderHeader.ApplicationUser = _unitOfWork.Users.GetOne(e => e.Id == GetCurrentUserId());

            summaryVM.OrderHeader.OrderStatus = OrderStauts.Pending;
            summaryVM.OrderHeader.PaymentStatus = OrderStauts.Pending;
            summaryVM.OrderHeader.PaymenteDate = DateTime.Now;
            summaryVM.OrderHeader.TotalPrice = summaryVM.ShoppingCarts.Select(e => e.Product.Price * e.Count).Sum();

            _unitOfWork.OrderHeaders.Add(summaryVM.OrderHeader);
            _unitOfWork.Complete();


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

            
            TempData["OrderPlaced"] = "Order Placed Successfully";
            return RedirectToAction("Index", "Home");
        }

    }
}
