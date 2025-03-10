using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;

namespace E_Commerce.DataAccess.Repositries
{
    public class ShoppingCartRepositry : GenericRepositry<ShoppingCart>, IShoppingCartRepositry
    {
        public ShoppingCartRepositry(AppDBContext context) : base(context) { }
    }
}
