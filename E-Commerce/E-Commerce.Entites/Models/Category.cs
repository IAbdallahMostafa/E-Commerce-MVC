using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [MinLength(3, ErrorMessage = "MINIMUM LENGTH OF NAME IS 3 CHARACTERS!")]
        [MaxLength(30, ErrorMessage = "MAX LENGTH OF NAME IS 30 CHARACTERS!")]
        public string Name { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "MINIMUM LENGTH OF DESCRIPTION IS 5 CHARACTERS!")]
        [MaxLength(150, ErrorMessage = "MAX LENGTH OF DESCRIPTION IS 150 CHARACTERS!")]
        public string Description { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
