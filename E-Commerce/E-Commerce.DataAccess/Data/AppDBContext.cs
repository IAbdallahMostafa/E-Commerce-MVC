using E_Commerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDBContext).Assembly);
        }
    }
}
