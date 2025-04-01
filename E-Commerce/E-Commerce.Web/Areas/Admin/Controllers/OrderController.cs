using E_Commerce.Entites.Intefaces;
using E_Commerce.Web.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // to bind ordervm
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetails()
        {
            
            var orderHeader = _unitOfWork.OrderHeaders.GetOne(e => e.Id == OrderVM.OrderHeader.Id);

            if (orderHeader == null)
                return NotFound("There No Order Found");

            orderHeader.Name = OrderVM.OrderHeader.Name;
            orderHeader.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeader.City = OrderVM.OrderHeader.City;
            orderHeader.Address = OrderVM.OrderHeader.Address;

            if (OrderVM.OrderHeader.Carrier != null)
                orderHeader.Carrier = OrderVM.OrderHeader.Carrier;
            
            if (OrderVM.OrderHeader.TrackingNumber != null)
                orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
               
            _unitOfWork.Complete();
            TempData["UpdateOrderDetails"] = "Order Details Updated Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartProccess()
        {
            var orderHeader = _unitOfWork.OrderHeaders.GetOne(e => e.Id == OrderVM.OrderHeader.Id);

            if (orderHeader == null)
                return NotFound("There No Order Found");
            
            _unitOfWork.OrderHeaders.UpdateOrderStatus(orderHeader.Id, OrderStauts.Processing, null);
            _unitOfWork.Complete();

            TempData["StartProcessOrder"] = "Order Status Updated To Proccessing";
            return RedirectToAction("Details", new {id = OrderVM.OrderHeader.Id});
        }

        
    }
}
