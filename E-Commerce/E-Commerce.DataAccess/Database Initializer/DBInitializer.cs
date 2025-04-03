using E_Commerce.DataAccess.Data;
using E_Commerce.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities;

namespace E_Commerce.DataAccess.Database_Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDBContext _context;
        public DBInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDBContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public void Initialize()
        {
            // Update Migration to databse
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            // Create Roles
            if (!_roleManager.RoleExistsAsync(Roles.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.EditorRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.CustomerRole)).GetAwaiter().GetResult();

                // Create Admin User
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Admin@myshop.com",
                    Email = "Admin@myshop.com",
                    Name = "Administrator",
                    City = "Cairo",
                    Address = "Cairo",
                    PhoneNumber = "01124338655"
                }, "P@$$w0rd").GetAwaiter().GetResult();

                ApplicationUser adminUser = _context.Users.FirstOrDefault(e => e.Email == "Admin@myshop.com");

                if (adminUser != null)
                {
                    _userManager.AddToRoleAsync(adminUser, Roles.AdminRole).GetAwaiter().GetResult();
                } 

            }

        }
    }
}

