using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repositries;
using E_Commerce.Entites.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;

namespace E_Commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles = Roles.AdminRole)]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //// return all users except the current user
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var users = _unitOfWork.Users.GetAll(e => e.Id != claim.Value);
            return Json(new { data = users });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
