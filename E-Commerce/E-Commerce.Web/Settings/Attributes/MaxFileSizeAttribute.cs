using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Web.Settings.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;
        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            var file = value as IFormFile;

            if (file!.Length > _maxFileSize)
                return new ValidationResult($"Max size is {ConstantsFile.MaxFileSizeInMB}MB");

            return ValidationResult.Success;

        }
    }
}
