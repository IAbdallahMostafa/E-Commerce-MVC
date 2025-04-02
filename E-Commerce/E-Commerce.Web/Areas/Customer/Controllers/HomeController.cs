using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;
using E_Commerce.Entities.Models;
using E_Commerce.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;
using X.PagedList.Extensions;

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
        public IActionResult Index(int? page)
        {
            // to paginate the products
            int pageNumber = page ?? 1;
            int pageSize = 8;

            IEnumerable<Product> products = _unitOfWork.Products.GetAll().ToPagedList(pageNumber, pageSize);
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
            // get current user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // check if the same user add the same product before
            var existisCart = _unitOfWork.ShoppingCarts.GetOne(e => e.ProductId == shoppingCart.ProductId && e.ApplicationUserId == claim.Value);

            if (existisCart != null) // user add the same product before
            {
                _unitOfWork.ShoppingCarts.IncreaseCount(existisCart, shoppingCart.Count);
            }
            else // user not add the same product before
            {
                shoppingCart.ApplicationUserId = claim.Value;
                _unitOfWork.ShoppingCarts.Add(shoppingCart);
            }

            _unitOfWork.Complete();

            HttpContext.Session.SetInt32(Sessions.SessionKey,
           _unitOfWork.ShoppingCarts.GetAll(e => e.ApplicationUserId == claim.Value).Select(e => e.Count).Sum()
           );

            TempData["Create"] = "Item Added To Shopping Cart Successfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
