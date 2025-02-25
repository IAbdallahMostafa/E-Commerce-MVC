using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Intefaces;

namespace E_Commerce.DataAccess.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        public ICategoryRepositry Category { get; private set; }

        public UnitOfWork(AppDBContext context) 
        {
            _context = context;
            Category = new CategoryRepositry(context);
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
