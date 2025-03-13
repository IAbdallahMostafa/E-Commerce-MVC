using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entites.Models;

namespace E_Commerce.DataAccess.Repositries
{
    public class OrderHeaderRepositry : GenericRepositry<OrderHeader>, IOrderHeaderRepositry
    {
        public OrderHeaderRepositry(AppDBContext context) : base(context) { }
    }
}
