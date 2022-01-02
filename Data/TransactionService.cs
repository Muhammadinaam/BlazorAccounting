using Microsoft.EntityFrameworkCore.ChangeTracking;
using MudBlazor;
using System.Linq.Expressions;

namespace BlazorAccounting.Data
{
    public class DebitCreditNotEqualException : Exception
    {

    }

    public class EmptyException : Exception
    {

    }

    public class TransactionService : EntityServiceBase<Transaction>
    {
        public TransactionService(IGenericRepository<Transaction> genericRepository, ApplicationDbContext dbContext)
            :base(dbContext)
        {
            this.repository = genericRepository;
        }

        public override Expression<Func<Transaction, bool>>? GetSearchPredicate(
            string searchTerm)
        {
            Expression <Func<Transaction, bool>>? predicate = null;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                predicate = x => x.VoucherNumber.ToLower().Contains(searchTerm.ToLower());
            }
            return predicate;
        }

        public override Func<IQueryable<Transaction>, IQueryable<Transaction>>? GetSortFunction(string sortBy, SortDirection sortDirection)
        {
            Func<IQueryable<Transaction>, IQueryable<Transaction>>? sortFunction = null;
            if (sortBy != null)
            {
                Expression<Func<Transaction, string>>? keySelector = null;
                keySelector = sortBy switch
                {
                    "datetime" => q => q.DateTime.ToString(),
                    "vouhcernumber" => q => q.VoucherNumber,
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

        public override ValueTask<EntityEntry<Transaction>> InsertAsync(Transaction t)
        {
            CheckDebitCredit(t);
            return base.InsertAsync(t);
        }

        private static void CheckDebitCredit(Transaction t)
        {
            if (t.TransactionLines.Count == 0)
            {
                throw new EmptyException();
            }

            var totalDebit = t.TransactionLines.Sum(tl => tl.Debit);
            var totalCredit = t.TransactionLines.Sum(tl => tl.Credit);
            if (totalDebit != totalCredit)
            {
                throw new DebitCreditNotEqualException();
            }
        }

        public override void Update(Transaction t)
        {
            CheckDebitCredit(t);
            base.Update(t);
        }
    }
}
