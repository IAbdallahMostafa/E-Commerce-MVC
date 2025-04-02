using E_Commerce.DataAccess.Repositries;
using E_Commerce.Entites.Intefaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;

namespace E_Commerce.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cliamsIdentity = (ClaimsIdentity)User.Identity;
            var claim = cliamsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(Sessions.SessionKey) == null)
                    HttpContext.Session.SetInt32(Sessions.SessionKey, _unitOfWork.ShoppingCarts.GetProductsCount(claim.Value));

                return View(HttpContext.Session.GetInt32(Sessions.SessionKey));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
