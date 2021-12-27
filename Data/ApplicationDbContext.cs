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
    }
}