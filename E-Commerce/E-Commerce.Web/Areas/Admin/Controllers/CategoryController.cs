using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Interfaces;
using E_Commerce.Entities.Models;
using E_Commerce.Web.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(); 
        }
        public IActionResult GetAll()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return Json(new { data = categories });
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
                _unitOfWork.Categories.Add(category);
                _unitOfWork.Complete();
                TempData["Create"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = _unitOfWork.Categories.GetOne(e => e.Id == id);
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
                _unitOfWork.Categories.Update(category);
                _unitOfWork.Complete();
                TempData["Edit"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var category = _unitOfWork.Categories.GetOne(e => e.Id == id);
            if (category == null)
                return Json(new { success = false, message = "Error While Deleting!" });

            try
            {
                _unitOfWork.Categories.Delete(category);
                _unitOfWork.Complete();
                return Json(new { success = true, message = "Category Deleted Successfully!" });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    return Json(new { success = false, message = "Cannot Delete This Category Because It Has Associated Products!" });
                }

                return Json(new { success = false, message = "An Error Occurred While Deleting The Category!" });
            }
        }

    }
}
