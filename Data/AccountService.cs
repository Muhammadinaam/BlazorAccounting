using MudBlazor;
using System.Linq.Expressions;

namespace BlazorAccounting.Data
{
    public class AccountService : EntityServiceBase<Account>
    {
        public AccountService(IGenericRepository<Account> genericRepository, ApplicationDbContext dbContext)
            :base(dbContext)
        {
            this.repository = genericRepository;
        }

        public override Expression<Func<Account, bool>>? GetSearchPredicate(
            string searchTerm)
        {
            Expression <Func<Account, bool>>? predicate = null;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                predicate = x => x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    x.Code.ToLower().Contains(searchTerm.ToLower());
            }
            return predicate;
        }

        public override Func<IQueryable<Account>, IQueryable<Account>>? GetSortFunction(string sortBy, SortDirection sortDirection)
        {
            Func<IQueryable<Account>, IQueryable<Account>>? sortFunction = null;
            if (sortBy != null)
            {
                Expression<Func<Account, string>>? keySelector = null;
                keySelector = sortBy switch
                {
                    "code" => q => q.Code,
                    "name" => q => q.Name,
                    _ => throw new Exception($"Sort by {sortBy} not supported"),
                };

                switch (sortDirection)
                {
                    case SortDirection.Ascending:
                        sortFunction = q => q.OrderBy(keySelector);
                        break;
                    case SortDirection.Descending:
                        sortFunction = q => q.OrderByDescending(keySelector);
                        break;
                    case SortDirection.None:
                        break;
                    default:
                        break;
                }
            }
            return sortFunction;
        }
    }
}
