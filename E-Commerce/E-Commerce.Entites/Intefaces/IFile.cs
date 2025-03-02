using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace E_Commerce.Entites.Intefaces
{
    public interface IFile
    {
        string SaveFile(string rootPath, string imagesPath, IFormFile file)
        {
            if (file != null)
            {

                string fileName = Guid.NewGuid().ToString(); 
                var extension = Path.GetExtension(file.FileName); 
                var fullPath = @$"{rootPath}{imagesPath}\{fileName}{extension}";
                
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return $@"{fileName}{extension}";
            }
            return null!;

        }

        void DeleteFile(string pathToDelete)
        {
            System.IO.File.Delete(pathToDelete);
        }

    }
}
