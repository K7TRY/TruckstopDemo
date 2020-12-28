using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace TruckstopDemoWebApi.Models
{
    [Serializable]
    public class Account
    {
        [Key]
        [ReadOnly(true)]
        [Display(Name = "Account ID", Description = "Unique account ID")]
        public Guid AccountId { get; set; } = Guid.NewGuid();

        [Display(Name = "Customer ID", Description = "Unique customer ID")]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [Display(Name = "Last Accessed", Description = "Date and time the customer last accessed this account.")]
        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Display(Name = "Account Type", Description = "Customer Account Type")]
        public AccountType AccountType { get; set; } = AccountType.Savings;

        [Display(Name = "Collection of transactions", Description = "A list of transactions for this account.")]
        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();

        public IEnumerable<Transaction> OrderedTransactions()
        {
            return SortDescending(Transactions.ToList());
        }

        [ReadOnly(true)]
        public decimal AccountBalance => calculateAccountBalance();

        private decimal calculateAccountBalance()
        {
            if (Transactions == null)
            {
                return 0.0m;
            }

            List<Transaction> transactionList = new List<Transaction>();
            transactionList.AddRange(Transactions);
            //transactionList = SortDescending(transactionList);
            decimal balance = transactionList.Sum(x => x.Amount);
            return balance;
        }

        private static List<Transaction> SortDescending(List<Transaction> list)
        {
            list.Sort((a, b) => b.TransactionDateTime.CompareTo(a.TransactionDateTime));
            return list;
        }
    }
}
