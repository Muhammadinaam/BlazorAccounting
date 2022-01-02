using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlazorAccounting.Data
{
    public enum AccountType
    {
        Asset,
        Liability,
        Income,
        Expense
    }


    public class Account : MultiTenantBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public AccountType AccountType { get; set; }
    }
}
