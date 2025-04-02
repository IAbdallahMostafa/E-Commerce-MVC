using E_Commerce.Entites.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.AllOrders = _unitOfWork.OrderHeaders.GetAll().Count();
            ViewBag.ApprovedOrders = _unitOfWork.OrderHeaders.GetAll(e => e.OrderStatus == OrderStauts.Approved).Count();
            ViewBag.PendingOrders = _unitOfWork.OrderHeaders.GetAll(e => e.OrderStatus == OrderStauts.Pending).Count();
            ViewBag.CancelledOrders = _unitOfWork.OrderHeaders.GetAll(e => e.OrderStatus == OrderStauts.Cancelled).Count();
            ViewBag.AllUsers = _unitOfWork.Users.GetAll().Count();
            ViewBag.AllCategories = _unitOfWork.Categories.GetAll().Count();
            ViewBag.AllProducts = _unitOfWork.Products.GetAll().Count();
            return View();
        }
    }
}
