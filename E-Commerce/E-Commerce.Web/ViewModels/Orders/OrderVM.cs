using E_Commerce.Entities.Models;

namespace E_Commerce.Web.ViewModels.Orders
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
