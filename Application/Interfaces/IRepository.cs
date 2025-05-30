using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
        Task<List<T>> FindAllWithIncludesAsync(Expression<Func<T, bool>> predicate, string[] includes, CancellationToken cancellationToken = default);


        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);

        IQueryable<T> Query();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
