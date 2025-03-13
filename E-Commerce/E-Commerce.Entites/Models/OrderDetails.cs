using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace E_Commerce.Entites.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
