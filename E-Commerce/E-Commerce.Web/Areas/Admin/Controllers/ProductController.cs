using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.ViewModels.Products;
using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnviornment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviornment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnviornment = webHostEnviornment;
        }

        public IActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddProductViewModel product = new AddProductViewModel()
            {
                Categories = _unitOfWork.Categories.GetAll().Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.Id.ToString()
                })
            };
            return View(product);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                string? image = _unitOfWork.Products.SaveFile(_webHostEnviornment.WebRootPath, "Images/Product", productVM.Image);
                var product = new Product()
                {
                    Id = productVM.Id,
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    CategoryId = productVM.CategoryId,
                    Image = image!,
                };
                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();
                TempData["Create"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }
            productVM.Categories = _unitOfWork.Categories.GetAll().Select(e => new SelectListItem
            {
                Text = e.Name,
                Value = e.Id.ToString()
            });
            return View(productVM);
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
