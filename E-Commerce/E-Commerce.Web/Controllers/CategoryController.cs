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
                return RedirectToAction("Index");
            }
            return View(category);
        }

    }
}
