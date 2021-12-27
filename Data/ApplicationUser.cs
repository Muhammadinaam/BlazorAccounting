using Microsoft.AspNetCore.Identity;

namespace BlazorAccounting.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
