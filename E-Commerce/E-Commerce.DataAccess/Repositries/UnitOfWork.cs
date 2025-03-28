﻿using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;
using E_Commerce.Entities.Intefaces;

namespace E_Commerce.DataAccess.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        public ICategoryRepositry Categories { get; private set; }
        public IProductRepositry Products { get; private set; }
        public IUserRepositry Users { get; private set; }
        public IShoppingCartRepositry ShoppingCarts { get; private set; }

        public IOrderHeaderRepositry OrderHeaders { get; private set; }
        public IOrderDetailsRepositry OrderDetails { get; private set; }

        public UnitOfWork(AppDBContext context) 
        {
            _context = context;
            Categories = new CategoryRepositry(context);
            Products = new ProductRepositry(context);
            Users = new UserRepositry(context);
            ShoppingCarts = new ShoppingCartRepositry(context);
            OrderHeaders = new OrderHeaderRepositry(context);
            OrderDetails = new OrderDetailsRepositry(context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
