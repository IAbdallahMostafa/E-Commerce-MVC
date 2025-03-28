using E_Commerce.Entities.Intefaces;

namespace E_Commerce.Entites.Intefaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepositry Categories {  get; }
        IProductRepositry Products { get; }
        IUserRepositry Users { get; }
        IShoppingCartRepositry ShoppingCarts { get; }
        IOrderHeaderRepositry OrderHeaders { get; }
        IOrderDetailsRepositry OrderDetails { get; }

        int Complete();

        Task<int> CompleteAsync(); 
    }
}
