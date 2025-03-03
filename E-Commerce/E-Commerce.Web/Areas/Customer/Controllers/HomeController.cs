using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Models;
using E_Commerce.Web.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Details(int id)
        {
            Product product = _unitOfWork.Products.GetOne(e => e.Id == id, new[] { "Category" });
            if (product == null)
                return NotFound("This Product Is Not Found!");

            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel
            {
                Product = product,
                Count = 1,
                RelatedProducts = _unitOfWork.Products.GetAll(e => e.CategoryId == product.CategoryId).Take(4)
            }; 
            
            return View(shoppingCart);
        }
    }
}
