using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "MINIMUM LENGTH OF NAME IS 3 CHARACTERS!")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image{ get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
