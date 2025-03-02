using E_Commerce.Web.Settings;
using E_Commerce.Web.Settings.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Web.ViewModels.Products
{
    public class AddProductViewModel : ProductVM
    {
        [Required(ErrorMessage = "Must Upload an Image!")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(ConstantsFile.AllowedExtensions)]
        [MaxFileSize(ConstantsFile.MaxFileSizeInBytes)]
        public IFormFile Image { get; set; }
    }
}
