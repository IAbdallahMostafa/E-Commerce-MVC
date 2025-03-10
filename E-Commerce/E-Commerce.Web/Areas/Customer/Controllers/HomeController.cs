using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;
using E_Commerce.Entities.Models;
using E_Commerce.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Products.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Product product = _unitOfWork.Products.GetOne(e => e.Id == id, new[] { "Category" });
            if (product == null)
                return NotFound("This Product Is Not Found!");

            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel
            {
                Product = product,
                Count = 1,
                RelatedProducts = _unitOfWork.Products.GetAll(e => e.CategoryId == product.CategoryId && e.Id != id).Take(4)
            }; 
            
            return View(shoppingCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId =  claim.Value;

            _unitOfWork.ShoppingCarts.Add(shoppingCart);
            _unitOfWork.Complete();
            
            return RedirectToAction("Index", "Home");
        }
    }
}
