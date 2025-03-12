using E_Commerce.Entites.Models;

namespace E_Commerce.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
