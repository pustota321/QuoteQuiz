using System.Linq.Expressions;

namespace QuoteQuizBackend.DataAccess.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id, params Expression<Func<T, object>>[] propertySelectors);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] propertySelectors);
    }
}
