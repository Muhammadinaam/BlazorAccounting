using Microsoft.AspNetCore.Identity;

namespace BlazorAccounting.Data
{
    public class MultiTenantBase
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
