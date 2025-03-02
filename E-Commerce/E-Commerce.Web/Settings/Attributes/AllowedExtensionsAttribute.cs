using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Web.Settings.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) 
                return null;

            var file = value as IFormFile;
            string fileExtension = Path.GetExtension(file!.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
                return new ValidationResult($"File Must Be {_allowedExtensions} Extensions");

            return ValidationResult.Success;
        }
    }
}
