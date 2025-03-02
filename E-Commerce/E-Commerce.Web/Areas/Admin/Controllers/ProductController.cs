using AutoMapper;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Models;
using E_Commerce.Web.Settings;
using E_Commerce.Web.Settings.Mapper;
using E_Commerce.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnviornment;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviornment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHostEnviornment = webHostEnviornment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(); 
        }
        
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll(null, new[] {"Category"});
            return Json(new { data = products });
        }
        [HttpGet]
        public IActionResult Create()
        {
            AddProductViewModel product = new AddProductViewModel()
            {
                Categories = _unitOfWork.Categories.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() })
            };
            return View(product);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                string? image = _unitOfWork.Products.SaveFile(_webHostEnviornment.WebRootPath, ConstantsFile.ProductsPath, productVM.Image);
                var product = new Product();
                _mapper.Map(productVM, product);
                product.Image = image;

                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();
                TempData["Create"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }

            productVM.Categories = _unitOfWork.Categories.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = _unitOfWork.Products.GetOne(e => e.Id == id);
            if (product == null)
                return NotFound("This Product Is Not Found!");

            EditProductViewModel productVM = new()
            {
                oldImageName = product.Image,
                Categories = _unitOfWork.Categories.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() })
            };
            _mapper.Map(product, productVM);
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Products.GetOne(e => e.Id == productVM.Id);
                if (product == null)
                    return NotFound("This Product Is Not Found!");

                var oldImageName = product.Image;

                if (productVM.Image is not null)
                {
                    product.Image = _unitOfWork.Products.SaveFile(_webHostEnviornment.WebRootPath, ConstantsFile.ProductsPath, productVM.Image);

                    var pathToDelete = $"{_webHostEnviornment.WebRootPath}{ConstantsFile.ProductsPath}\\{oldImageName}";
                    _unitOfWork.Products.DeleteFile(pathToDelete);
                }
                

                _mapper.Map(productVM, product);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(productVM);
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
