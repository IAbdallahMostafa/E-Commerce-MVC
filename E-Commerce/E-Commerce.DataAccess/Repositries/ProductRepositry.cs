using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.DataAccess.Repositries
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepositry
    {

        public ProductRepositry(AppDBContext context) : base(context)
        {
        }
        
    }
}
