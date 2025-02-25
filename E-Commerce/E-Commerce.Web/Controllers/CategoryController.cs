using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Interfaces;
using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericRepositry<Category> _repositry;
        public CategoryController(IGenericRepositry<Category > repositry)
        {
            _repositry = repositry;
        }

        public IActionResult Index()
        {
            var categories = _repositry.GetAll();
            return View(categories); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _repositry.Add(category);
                TempData["Create"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = _repositry.GetOne(e => e.Id == id);
            if (category == null)
                return NotFound("This Category Is Not Found!");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _repositry.Update(category);
                TempData["Edit"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _repositry.GetOne(e => e.Id == id);
            if (category == null)
                return NotFound("This Category Is Not Found!");
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(Category category)
        {
                _repositry.Delete(category);
                TempData["Delete"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            
        }
    }
}
