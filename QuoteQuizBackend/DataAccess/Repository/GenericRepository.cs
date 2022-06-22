using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QuoteQuizBackend.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly QuoteQuizDbContext _context;
        private readonly DbSet<T> _set;

        public GenericRepository(QuoteQuizDbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public  async Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelector)
        {
            var query = IncludeDetails(_set.Where(predicate), propertySelector);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _set.Where(predicate).ToListAsync();
        }
        public async Task DeleteAsync(int id, params Expression<Func<T, object>>[] propertySelectors)
        {
            var entity = await _set.FindAsync(id);
            if (entity is not null)
            {
                _set.Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] propertySelectors)
        {
            var query = IncludeDetails(_set, propertySelectors);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<T> SaveAsync(T entity)
        {
            await _set.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _set.Attach(entity);
            var updatedEntity = _set.Update(entity).Entity;
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
        private static IQueryable<T> IncludeDetails(IQueryable<T> query, Expression<Func<T, object>>[] propertySelectors)
        {
            if (propertySelectors != null)
                foreach(var item in propertySelectors)
                    query = query.Include(item);

            return query;
        }
    }
}
