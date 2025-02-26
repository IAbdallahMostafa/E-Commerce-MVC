using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProudctController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProudctController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var products = _unitOfWork.Products.GetAll();
            return View(products); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();
                TempData["Create"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = _unitOfWork.Products.GetOne(e => e.Id == id);
            if (product == null)
                return NotFound("This Product Is Not Found!");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Update(product);
                _unitOfWork.Complete();
                TempData["Edit"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.GetOne(e => e.Id == id);
            if (product == null)
                return NotFound("This Product Is Not Found!");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(Product product)
        {
             _unitOfWork.Products.Delete(product);
            _unitOfWork.Complete();
             TempData["Delete"] = "Product Deleted Successfully";
             return RedirectToAction("Index");
            
        }
    }
}
