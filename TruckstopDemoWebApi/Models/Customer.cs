using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TruckstopDemoWebApi.Models
{
    [Serializable]
    public class Customer
    {
        [Key]
        [ReadOnly(true)]
        [Display(Name = "Customer ID", Description = "Unique customer ID")]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [ReadOnly(true)]
        [Display(Name = "User Name", Prompt = "Please select a username.", Description = "The unique username for this customer.")]
        [StringLength(255, ErrorMessage = "The username needs to be between 2 and 255 letters long.", MinimumLength = 2)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name", Prompt = "Please enter the first name of the customer.", Description = "Customer first name.")]
        [StringLength(255, ErrorMessage = "The first name needs to be between 2 and 255 letters long.", MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name", Prompt = "Please enter the last name of the customer.", Description = "Customer last name.")]
        [StringLength(255, ErrorMessage = "The last name needs to be between 2 and 255 letters long.", MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [ReadOnly(true)]
        [Display(Name = "Customer's name", Description = "Customer's full name.")]
        public string FullName => $"{FirstName} {LastName}";

        [ReadOnly(true)]
        [Display(Name = "Customer's name", Description = "Customer's full name. Last name, first name.")]
        public string FullNameLastNameFirst => $"{LastName}, {FirstName}";

        // The error message could be a globalized resource.
        [Required]
        [StringLength(255, ErrorMessage = "The mailing address needs to be between 2 and 255 letters long.", MinimumLength = 2)]
        [Display(Name = "Street Mailing Address", Prompt = "Please enter the street address.", Description = "The mailing address street.")]
        public string MailingAddressStreet { get; set; } = string.Empty;

        [Required]
        [StringLength(255, ErrorMessage = "The mailing city is too long.")]
        [Display(Name = "Street Mailing City", Prompt = "Please enter the city name.", Description = "The mailing address city.")]
        public string MailingAddressCity { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "The mailing state is too long.")]
        [Display(Name = "State Abbreviation", Prompt = "Please enter the state name.", Description = "The mailing address state abbreviation.")]
        public string MailingAddressState { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "The mailing zip code needs to be between 5 and 10 characters long.", MinimumLength = 5)]
        [Display(Name = "Street Mailing State", Prompt = "Please enter the state name.", Description = "The mailing address state.")]
        public string MailingAddressZip { get; set; } = string.Empty;

        public string MailingAddressCountry { get; set; } = "USA";

        [ReadOnly(true)]
        public string MailingBlock => $"{FullName}\r\n{MailingAddressStreet}\r\n{MailingAddressCity}, {MailingAddressState} {MailingAddressZip}\r\n{MailingAddressCountry}";

        [ReadOnly(true)]
        public string MailingBlockHtml => $"{FullName}<br />{MailingAddressStreet}<br />{MailingAddressCity}, {MailingAddressState} {MailingAddressZip}<br />{MailingAddressCountry}";

        [Phone]
        public string MobilePhone { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime MembershipDate { get; set; } = DateTime.UtcNow;

        [ReadOnly(true)]
        public ushort YearsMember => (ushort)Math.Round(DateTime.UtcNow.Subtract(MembershipDate).TotalDays / 365.2425, MidpointRounding.ToZero);

        [StringLength(255, ErrorMessage = "The email address is too long.")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public IEnumerable<Account> Accounts { get; set; } = new List<Account>();
    }
}
