using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruckstopDemoWebApi.Models
{
    [Serializable]
    public class Transaction
    {
        [Key]
        [ReadOnly(true)]
        [Display(Name="Transaction ID", Description = "Unique transaction ID")]
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        [Display(Name = "Account ID", Description = "Unique account ID")]
        public Guid AccountId { get; set; } = Guid.NewGuid();

        [ReadOnly(true)]
        [Display(Name = "Transaction Date and Time", Description = "Date and time the transaction took place")]
        public DateTime TransactionDateTime { get; set; } = DateTime.UtcNow;

        [Display(Name = "Funds Cleared", Description = "Have the funds been fully cleared to the account?")]
        public bool Cleared { get; set; } = false;

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Transaction Amount", Description = "Amount transfered")]
        public decimal Amount { get; set; } = 0m;
    }
}
