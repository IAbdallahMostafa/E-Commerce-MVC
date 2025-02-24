using E_Commerce.DataAccess.Data;
using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _context;
        public CategoryController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
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
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Create"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = _context.Categories.Find(id);
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
                Category categoryInDb = _context.Categories.Find(category.Id)!; 
                categoryInDb.Name = category.Name;
                categoryInDb.Description = category.Description;
                _context.SaveChanges();
                TempData["Edit"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound("This Category Is Not Found!");
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["Delete"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            } 
            return NotFound("Category Not Found!");
        }
    }
}
