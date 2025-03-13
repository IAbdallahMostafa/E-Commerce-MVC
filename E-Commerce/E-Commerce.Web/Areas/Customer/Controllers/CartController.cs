using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;
using E_Commerce.Web.ViewModels.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult Index()
        {
            // get current user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


            var shoppingCartVM = new ShoppingCartVM
            {
                ShoppingCarts = _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == userId, new[] { "Product" })
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
    }
}
