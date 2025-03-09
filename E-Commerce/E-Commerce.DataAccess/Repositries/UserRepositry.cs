using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Models;

namespace E_Commerce.DataAccess.Repositries
{
    public class UserRepositry : GenericRepositry<ApplicationUser>, IUserRepositry
    {
        public UserRepositry(AppDBContext context) : base(context)
        {
        }
    }
}
