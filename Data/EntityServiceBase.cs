using BlazorAccounting.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MudBlazor;
using System.Linq.Expressions;

namespace BlazorAccounting.Data
{
    public class EntityServiceBase<T> where T : class
    {
        protected IGenericRepository<T>? repository;
        private readonly ApplicationDbContext _dbContext;

        public EntityServiceBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ValueTask<T?> GetByIDAsync(int id)
        {
            return repository.GetByIDAsync(id);
        }

        public virtual Expression<Func<T, bool>>? GetSearchPredicate(
            string searchTerm)
        {
            throw new NotImplementedException("This function is to be implemented in child class");
        }

        public virtual Func<IQueryable<T>, IQueryable<T>>? GetSortFunction(
            string sortBy,
            SortDirection sortDirection)
        {
            throw new NotImplementedException("This function is to be implemented in child class");
        }

        public Task<(int count, List<T> data)> GetPaginatedAsync(
            int perPage,
            int pageNumber,
            string searchTerm,
            string sortBy,
            SortDirection sortDirection)
        {
            return repository.GetPaginatedAsync(
                perPage,
                pageNumber,
                GetSearchPredicate(searchTerm),
                GetSortFunction(sortBy, sortDirection));
        }

        public string GetCurrentUserId()
        {
            var userId = _dbContext.GetUserId();
            if (userId == null)
            {
                throw new Exception("User not logged in");
            }
            return userId;
        }

        public async ValueTask<EntityEntry<T>> InsertAsync(T t)
        {
            await SetCurrentUser(t);

            return await repository.InsertAsync(t);
        }

        private async Task SetCurrentUser(T t)
        {
            if (t != null && t.GetType().IsSubclassOf(typeof(MultiTenantBase)))
            {
                (t as MultiTenantBase).UserId = GetCurrentUserId();
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return repository.SaveChangesAsync();
        }

        public async void Update(T t)
        {
            await SetCurrentUser(t);
            repository?.Update(t);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await repository?.DeleteAsync(id);
        }
    }
}