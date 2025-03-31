using E_Commerce.Entites.Intefaces;
using E_Commerce.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var orders = _unitOfWork.OrderHeaders.GetAll(null, new[] { "ApplicationUser" });
            return Json(new { data = orders });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var orderVM = new OrderVM
            {
                OrderHeader = _unitOfWork.OrderHeaders.GetOne(u => u.Id == id, new[] { "ApplicationUser" }),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == id, new[] { "Product" })
            };

            if (orderVM.OrderHeader == null) 
                return NotFound("There No Order Found");

            return View(orderVM);
        }

    }
}
