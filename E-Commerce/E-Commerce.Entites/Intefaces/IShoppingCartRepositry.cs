using E_Commerce.Entites.Interfaces;
using E_Commerce.Entites.Models;


namespace E_Commerce.Entites.Intefaces
{
    public interface IShoppingCartRepositry : IGenericRepositry<ShoppingCart>
    {
        void IncreaseCount(ShoppingCart cart, int count);
        void DecreaseCount(ShoppingCart cart, int count);
    }
}
