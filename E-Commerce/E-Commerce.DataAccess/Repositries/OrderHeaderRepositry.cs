using E_Commerce.DataAccess.Data;
using E_Commerce.Entities.Intefaces;
using E_Commerce.Entities.Models;

namespace E_Commerce.DataAccess.Repositries
{
    public class OrderHeaderRepositry : GenericRepositry<OrderHeader>, IOrderHeaderRepositry
    {
        public OrderHeaderRepositry(AppDBContext context) : base(context) { }

        public void UpdateOrderStatus(int orderId, string orderStatus, string? paymentStatus)
        {
            var order = GetOne(e => e.Id ==  orderId);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
                if (paymentStatus != null)
                    order.PaymentStatus = paymentStatus;
            }
            
        }
    }
}
