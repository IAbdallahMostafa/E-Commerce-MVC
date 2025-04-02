using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;

namespace E_Commerce.DataAccess.Repositries
{
    public class ShoppingCartRepositry : GenericRepositry<ShoppingCart>, IShoppingCartRepositry
    {
        public ShoppingCartRepositry(AppDBContext context) : base(context) { }

        public void IncreaseCount(ShoppingCart cart, int count)
        {
            cart.Count += count;
        }
        public void DecreaseCount(ShoppingCart cart, int count)
        {
            cart.Count -= count;
        }

        public int GetProductsCount(string userId)
        {
            return GetAll(e => e.ApplicationUserId == userId).Select(e => e.Count).Sum();
        }

    }
}
