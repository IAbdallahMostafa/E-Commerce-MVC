using System.Linq.Expressions;

namespace E_Commerce.Entites.Repositries
{
    public interface IGenericRepositry<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string[]? includeWord = null);
        T GetOne(Expression<Func<T, bool>>? filter = null, string[]? includeWords = null);
        void Add(T item);
        void Update(int id);
        void Delete(int id);
    }
}
