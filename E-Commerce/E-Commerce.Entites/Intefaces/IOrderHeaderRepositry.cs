using E_Commerce.Entites.Interfaces;
using E_Commerce.Entites.Models;

namespace E_Commerce.Entites.Intefaces
{
    public interface IOrderHeaderRepositry : IGenericRepositry<OrderHeader>
    {
        void UpdateOrderStatus(int orderId, string orderStatus, string paymentStatus);

    } 
}
