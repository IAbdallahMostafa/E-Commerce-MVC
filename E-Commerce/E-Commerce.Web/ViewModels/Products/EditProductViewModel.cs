using E_Commerce.Web.Settings;
using E_Commerce.Web.Settings.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Web.ViewModels.Products
{
    public class EditProductViewModel : ProductVM
    {
        public int Id { get; set; }


        [DataType(DataType.Upload)]
        [AllowedExtensions(ConstantsFile.AllowedExtensions)]
        [MaxFileSize(ConstantsFile.MaxFileSizeInBytes)]
        public IFormFile? Image { get; set; }
        public string? oldImageName { get; set; } 
    }
}
