using E_Commerce.Entites.Interfaces;
using E_Commerce.Entities.Models;

namespace E_Commerce.Entities.Intefaces
{
    public interface IOrderHeaderRepositry : IGenericRepositry<OrderHeader>
    {
        void UpdateOrderStatus(int orderId, string orderStatus, string paymentStatus);

    } 
}
