using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TruckstopDemoWebApi.Models
{
    [Serializable]
    public enum AccountType
    {
        [Display(Name = "Savings", Description = "Savings account")]
        Savings,

        [Display(Name = "Checking", Description = "Checking account")]
        Checking,

        [Display(Name = "Christmas Club", Description = "Christmas Club savings account")]
        ChristmasClub,

        [Display(Name = "Credit", Description = "Credit account")]
        Credit,

        [Display(Name = "Money Market", Description = "Money Market account")]
        MoneyMarket,

        [Display(Name = "Certificate of Deposit", Description = "Certificate of Deposit")]
        CertificateOfDeposit,

        [Display(Name = "Retirement Savings", Description = "Retirement savings account")]
        Retirement
    }
}
