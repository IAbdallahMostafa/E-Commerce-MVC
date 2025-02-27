using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace E_Commerce.Entites.Intefaces
{
    public interface IFile
    {
        string SaveFile(string rootPath, string folderName, IFormFile file)
        {
            if (file != null)
            {
                var fullfolder = Path.Combine(rootPath, folderName); //WWWroot/images/products
                string fileName = Guid.NewGuid().ToString(); //random name (123456)
                var extension = Path.GetExtension(file.FileName); //.jpg
                var fullPath = Path.Combine(fullfolder, fileName + extension); //WWWroot/images/products/123456.jpg                                                   

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return fullPath;
            }
            return null!;

        }


    }
}
