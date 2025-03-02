namespace E_Commerce.Web.Settings
{
    public static class ConstantsFile
    {
        public const string ProductsPath = "/Images/Product";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const long MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
