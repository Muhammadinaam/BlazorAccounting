using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BlazorAccounting.Data
{
    public interface IGenericRepository<T> where T : class
    {
        ApplicationDbContext Context { get; }
        Task<int> DeleteAsync(int id);
        void Dispose();
        Task<List<T>> GetAll();
        ValueTask<T?> GetByIDAsync(int id);
        Task<(int count, List<T> data)> GetPaginatedAsync(
            int perPage,
            int pageNumber,
            Expression<Func<T, bool>>? searchPredicate,
            Func<IQueryable<T>, IQueryable<T>>? sortFunction);
        ValueTask<EntityEntry<T>> InsertAsync(T t);
        Task<int> SaveChangesAsync();
        void Update(T t);
    }
}