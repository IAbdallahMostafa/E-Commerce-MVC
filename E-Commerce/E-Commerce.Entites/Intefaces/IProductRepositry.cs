using E_Commerce.Entites.Interfaces;
using E_Commerce.Entities.Models;

namespace E_Commerce.Entites.Intefaces
{
    public interface IProductRepositry : IGenericRepositry<Product>, IFile
    {
    }
}
