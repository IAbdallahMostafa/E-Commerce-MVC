using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;
using E_Commerce.Web.ViewModels.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
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
    }
}
