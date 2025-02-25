using System.Linq.Expressions;

namespace E_Commerce.Entites.Interfaces
{
    public interface IGenericRepositry<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string[]? includeWord = null);
        T GetOne(Expression<Func<T, bool>>? filter = null, string[]? includeWords = null);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);
    }
}
