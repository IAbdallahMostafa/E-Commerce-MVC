using E_Commerce.DataAccess.Data;
using E_Commerce.Entities.Intefaces;
using E_Commerce.Entities.Models;

namespace E_Commerce.DataAccess.Repositries
{
    public class OrderDetailsRepositry : GenericRepositry<OrderDetails>, IOrderDetailsRepositry
    {
        public OrderDetailsRepositry(AppDBContext context) : base(context) { }
    }
}
