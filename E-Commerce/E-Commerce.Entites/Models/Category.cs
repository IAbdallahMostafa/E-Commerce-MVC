using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace E_Commerce.Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [MinLength(3, ErrorMessage = "MINIMUM LENGTH OF NAME IS 3 CHARACTERS!")]
        public string Name { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "MINIMUM LENGTH OF DESCRIPTION IS 5 CHARACTERS!")]
        public string Description { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public List<Product> Products { get; set; } = new();
    }
}
