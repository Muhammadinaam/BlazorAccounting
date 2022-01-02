using System.ComponentModel.DataAnnotations;

namespace BlazorAccounting.Data
{
    public class Transaction : MultiTenantBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [MaxLength(100)]
        public string VoucherNumber { get; set; }

        [ValidateComplexType]
        public virtual List<TransactionLine> TransactionLines { get; set; } = new List<TransactionLine>();
    }
}
