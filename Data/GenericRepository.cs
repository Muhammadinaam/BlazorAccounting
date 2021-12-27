using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace BlazorAccounting.Data
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {

        private ApplicationDbContext context;
        private bool disposed = false;

        public ApplicationDbContext Context => this.context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<List<T>> GetAll()
        {
            return context.Set<T>().ToListAsync();
        }

        public async Task<(int count, List<T> data)> GetPaginatedAsync(
            int perPage,
            int pageNumber,
            Expression<Func<T, bool>>? searchPredicate,
            Func<IQueryable<T>, IQueryable<T>>? sortFunction)
        {
            var query = context.Set<T>().Where(x => true);
            if (searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }
            if (sortFunction != null)
            {
                query = sortFunction(query);
            }
            var count = await query.CountAsync();
            var data = await query.Skip((pageNumber) * perPage).Take(perPage).ToListAsync();
            return (count, data);
        }

        public ValueTask<T?> GetByIDAsync(int id)
        {
            return context.Set<T>().FindAsync(id);
        }

        public ValueTask<EntityEntry<T>> InsertAsync(T t)
        {
            return context.Set<T>().AddAsync(t);
        }

        public async Task<int> DeleteAsync(int id)
        {
            T? t = await GetByIDAsync(id);
            if (t != null)
            {
                context.Set<T>().Remove(t);
                return await SaveChangesAsync();
            }
            return 0;
        }

        public void Update(T t)
        {
            context.Set<T>().Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
