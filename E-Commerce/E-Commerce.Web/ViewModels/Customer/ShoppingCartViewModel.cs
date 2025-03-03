using E_Commerce.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Web.ViewModels.Customer
{
    public class ShoppingCartViewModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public IEnumerable<Product> RelatedProducts { get; set; }
    }
}
