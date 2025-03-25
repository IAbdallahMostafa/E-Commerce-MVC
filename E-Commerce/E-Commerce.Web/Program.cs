using E_Commerce.DataAccess.Data;
using E_Commerce.DataAccess.Repositries;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Models;
using E_Commerce.Web.Settings.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utilities;
using Stripe;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Runtime compile    
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            // Register DBContex
            builder.Services.AddDbContext<AppDBContext>(options => 
                             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConstr")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>
                 (options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1))
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders(); 

            
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            // Register UnitOfWork
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Register Mapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Stripe
            builder.Services.Configure<StripeData>(builder.Configuration.GetSection("Stripe")); // Properties in clss must have the same name of keys in appsettings.json

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Stripe 
            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();


            app.UseAuthorization();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "area",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            
            
            app.Run();
        }
    }
}
