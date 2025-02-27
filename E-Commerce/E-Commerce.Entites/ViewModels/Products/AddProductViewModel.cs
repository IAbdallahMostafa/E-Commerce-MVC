using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entites.ViewModels.Products
{
    public class AddProductViewModel
    {
        public int Id { get; set; }
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

        [Required(ErrorMessage = "Must Upload an Image!")]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
