using System.ComponentModel.DataAnnotations;

namespace BlazorAccounting.Data
{
    public class TransactionLine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public Transaction Transaction { get; set; }
    }
}
