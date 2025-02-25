using E_Commerce.DataAccess.Data;
using E_Commerce.Entites.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DataAccess.Repositries
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDBContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepositry(AppDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string[]? includeWord = null)
        {
           IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeWord != null)
                foreach (var word in includeWord)
                    query = query.Include(word);

            return query.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string[]? includeWord = null)
        {

            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeWord != null)
                foreach (var word in includeWord)
                    query = query.Include(word);

            return await query.ToListAsync();
        }

        public T GetOne(Expression<Func<T, bool>>? filter = null, string[]? includeWords = null)
        {
            return GetAll(filter, includeWords).FirstOrDefault();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>>? filter = null, string[]? includeWords = null)
        {
            return (await GetAllAsync(filter, includeWords)).FirstOrDefault();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
        }
        public void Update(T item)
        {
            _dbSet.Update(item);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }
    }
}
