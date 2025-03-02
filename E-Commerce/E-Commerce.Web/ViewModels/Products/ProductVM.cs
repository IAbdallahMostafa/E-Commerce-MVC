using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Web.ViewModels.Products
{
    public class ProductVM
    {
        [Required]
        [MinLength(3, ErrorMessage = "MINIMUM LENGTH OF NAME IS 3 CHARACTERS!")]
        public string Name { get; set; } = string.Empty;
        [MinLength(5, ErrorMessage = "MINIMUM LENGTH OF DISCRIPTION IS 3 CHARACTERS!")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
