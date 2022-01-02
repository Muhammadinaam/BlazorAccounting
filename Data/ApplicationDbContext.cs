using BlazorAccounting.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorAccounting.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContext;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContext)
            : base(options)
        {
            _httpContext = httpContext;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Account>().HasQueryFilter(a => a.UserId == GetUserId());
            builder.Entity<Account>().HasIndex(a => new { a.UserId, a.Code }).IsUnique();

            builder.Entity<Transaction>()
                .HasQueryFilter(a => a.UserId == GetUserId())
                .Navigation(t => t.TransactionLines).AutoInclude();
            builder.Entity<Transaction>().HasIndex(t => new { t.UserId, t.VoucherNumber}).IsUnique();

            builder.Entity<TransactionLine>()
                .Navigation(tl => tl.Transaction).AutoInclude();

            builder.Entity<TransactionLine>()
                .Navigation(tl => tl.Account).AutoInclude();
        }

        public string? GetUserName()
        {
            return _httpContext.HttpContext.User?.Identity?.Name;
        }

        public string? GetUserId()
        {
            return Users.FirstOrDefault(u => u.UserName.ToLower() == GetUserName().ToLower())?.Id;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionLine> TransactionLines { get; set; }
    }
}