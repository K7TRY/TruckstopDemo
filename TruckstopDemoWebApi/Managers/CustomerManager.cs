using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TruckstopDemoWebApi.Models;

namespace TruckstopDemoWebApi.Managers
{
    public class CustomerManager
    {
        private string CustomerJsonFile { get; set; }
         
        private static readonly string[] AreaCodes = new[]
            {"201","202","203","205","206","207","208","209","210","212","213","214","215","216","217","218","219","224","225","228","229","231","234","239","240","248","251","252","253","254","256","260","262","267","269","270","276","281","301","302","303","304","305","307","308","309","310","312","313","314","315","316","317","318","319","320","321","323","325","330","334","336","337","339","347","351","352","360","361","386","401","402","404","405","406","407","408","409","410","412","413","414","415","417","419","423","425","430","432","434","435","440","443","469","478","479","480","484","501","502","503","504","505","507","508","509","510","512","513","515","516","517","518","520","530","540","541","551","559","561","562","563","567","570","571","573","574","575","580","585","586","601","602","603","605","606","607","608","609","610","612","614","615","616","617","618","619","620","623","626","630","631","636","641","646","650","651","660","661","662","678","682","701","702","703","704","706","707","708","712","713","714","715","716","717","718","719","720","724","727","731","732","734","740","754","757","760","763","765","770","772","773","774","775","781","785","786","801","802","803","804","805","806","808","810","812","813","814","815","816","817","818","828","830","831","832","843","845","847","848","850","856","857","858","859","860","862","863","864","865","866","870","901","903","904","906","907","908","909","910","912","913","914","915","916","917","918","919","920","925","928","931","936","937","940","941","947","949","951","952","954","956","970","971","972","973","978","979","980","985","989"
        };

        public CustomerManager()
        {
            string dataDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            CustomerJsonFile = Path.Combine(dataDir, "Customers.json");
        }

        private static string RandomPhoneNumber()
        {
            var rng = new Random();

            string areaCode = AreaCodes[rng.Next(AreaCodes.Length)];
            string exchange = rng.Next(100, 999).ToString();
            string station = rng.Next(1000, 9999).ToString();

            return $"({areaCode}) {exchange}-{station}";
        }

        private static string GetStateAbreviationByName(string name)
        {
            switch (name.ToUpperInvariant())
            {
                case "ALABAMA":
                    return "AL";

                case "ALASKA":
                    return "AK";

                case "AMERICAN SAMOA":
                    return "AS";

                case "ARIZONA":
                    return "AZ";

                case "ARKANSAS":
                    return "AR";

                case "CALIFORNIA":
                    return "CA";

                case "COLORADO":
                    return "CO";

                case "CONNECTICUT":
                    return "CT";

                case "DELAWARE":
                    return "DE";

                case "DISTRICT OF COLUMBIA":
                    return "DC";

                case "FEDERATED STATES OF MICRONESIA":
                    return "FM";

                case "FLORIDA":
                    return "FL";

                case "GEORGIA":
                    return "GA";

                case "GUAM":
                    return "GU";

                case "HAWAII":
                    return "HI";

                case "IDAHO":
                    return "ID";

                case "ILLINOIS":
                    return "IL";

                case "INDIANA":
                    return "IN";

                case "IOWA":
                    return "IA";

                case "KANSAS":
                    return "KS";

                case "KENTUCKY":
                    return "KY";

                case "LOUISIANA":
                    return "LA";

                case "MAINE":
                    return "ME";

                case "MARSHALL ISLANDS":
                    return "MH";

                case "MARYLAND":
                    return "MD";

                case "MASSACHUSETTS":
                    return "MA";

                case "MICHIGAN":
                    return "MI";

                case "MINNESOTA":
                    return "MN";

                case "MISSISSIPPI":
                    return "MS";

                case "MISSOURI":
                    return "MO";

                case "MONTANA":
                    return "MT";

                case "NEBRASKA":
                    return "NE";

                case "NEVADA":
                    return "NV";

                case "NEW HAMPSHIRE":
                    return "NH";

                case "NEW JERSEY":
                    return "NJ";

                case "NEW MEXICO":
                    return "NM";

                case "NEW YORK":
                    return "NY";

                case "NORTH CAROLINA":
                    return "NC";

                case "NORTH DAKOTA":
                    return "ND";

                case "NORTHERN MARIANA ISLANDS":
                    return "MP";

                case "OHIO":
                    return "OH";

                case "OKLAHOMA":
                    return "OK";

                case "OREGON":
                    return "OR";

                case "PALAU":
                    return "PW";

                case "PENNSYLVANIA":
                    return "PA";

                case "PUERTO RICO":
                    return "PR";

                case "RHODE ISLAND":
                    return "RI";

                case "SOUTH CAROLINA":
                    return "SC";

                case "SOUTH DAKOTA":
                    return "SD";

                case "TENNESSEE":
                    return "TN";

                case "TEXAS":
                    return "TX";

                case "UTAH":
                    return "UT";

                case "VERMONT":
                    return "VT";

                case "VIRGIN ISLANDS":
                    return "VI";

                case "VIRGINIA":
                    return "VA";

                case "WASHINGTON":
                    return "WA";

                case "WEST VIRGINIA":
                    return "WV";

                case "WISCONSIN":
                    return "WI";

                case "WYOMING":
                    return "WY";
            }

            return name.ToUpperInvariant().Substring(0, 2);
        }

        private static List<Customer> CreateNewMockCustomerList(List<Customer> customerList)
        {
            foreach (Customer customer in customerList)
            {
                customer.MobilePhone = RandomPhoneNumber();
                customer.MailingAddressState = GetStateAbreviationByName(customer.MailingAddressState);
                customer.MailingAddressCountry = "USA";

                var rnd = new Random();
                int numAccounts = rnd.Next(1, 5);
                List<Account> accounts = new List<Account>();
                Array AccountTypeValues = Enum.GetValues(typeof(AccountType));

                for (int i = 0; i < numAccounts; i++)
                {
                    Guid accountId = Guid.NewGuid();
                    decimal intialDeposit = Math.Round(Convert.ToDecimal(rnd.Next(2000, 5000)) + Convert.ToDecimal(rnd.NextDouble()), 2);

                    int yearsAgo = rnd.Next(-15, -1);
                    int monthsAgo = rnd.Next(-11, -1);
                    int daysAgo = rnd.Next(-29, -1);
                    DateTime transactionDate = DateTime.UtcNow.AddYears(yearsAgo).AddMonths(monthsAgo).AddDays(daysAgo);

                    List<Transaction> transactions = new List<Transaction>();
                    transactions.Add(new Transaction() { AccountId = accountId, Amount = intialDeposit, Cleared = true, TransactionDateTime = transactionDate, TransactionId = Guid.NewGuid() });

                    int numTransactions = rnd.Next(5, 100);

                    for (int j = 0; j < numTransactions; j++)
                    {
                        decimal transactionAmount = Math.Round(Convert.ToDecimal(rnd.Next(-300, 100)) + Convert.ToDecimal(rnd.NextDouble()), 2);
                        yearsAgo = rnd.Next(-2, 0);
                        monthsAgo = rnd.Next(-11, 0);
                        daysAgo = rnd.Next(-29, 0);
                        transactionDate = DateTime.UtcNow.AddYears(yearsAgo).AddMonths(monthsAgo).AddDays(daysAgo);
                        // bool cleared = rnd.Next() > (Int32.MaxValue / 2);

                        transactions.Add(new Transaction() { AccountId = accountId, Amount = transactionAmount, Cleared = true, TransactionDateTime = transactionDate, TransactionId = Guid.NewGuid() });
                    }

                    AccountType accountType = (AccountType)AccountTypeValues.GetValue(rnd.Next(AccountTypeValues.Length));
                    accounts.Add(new Account() { AccountId = accountId, AccountType = accountType, CustomerId = customer.CustomerId, LastAccessed = DateTime.UtcNow, Transactions = transactions });
                }

                customer.Accounts = accounts;
            }

            return customerList;
        }

        public IList<Customer> GetAllCustomers()
        {
            using FileStream fs = File.OpenRead(CustomerJsonFile);
            var customerList = JsonSerializer.DeserializeAsync<IList<Customer>>(fs).Result;
            return customerList;
        }

        public Customer GetCustomer(Guid CustomerId)
        {
            if (CustomerId == null || CustomerId.Equals(Guid.Empty))
            {
                throw new ArgumentNullException(nameof(CustomerId));
            }

            using FileStream fs = File.OpenRead(CustomerJsonFile);
            var customerList = JsonSerializer.DeserializeAsync<IList<Customer>>(fs).Result;

            var customer = customerList.Where(c => c.CustomerId.Equals(CustomerId)).FirstOrDefault();

            if (customer == null)
            {
                throw new NullReferenceException("The customer was not found.");
            }

            return customer;
        }

        public bool DeleteCustomer(Guid CustomerId)
        {
            if (CustomerId == null || CustomerId.Equals(Guid.Empty))
            {
                throw new ArgumentNullException(nameof(CustomerId));
            }

            var customerList = GetAllCustomers();
            var customerToDelete = customerList.Where(c => c.CustomerId.Equals(CustomerId)).FirstOrDefault();
            var successfulRemove = customerList.Remove(customerToDelete);

            if (successfulRemove)
            {
                SaveCustomers(customerList);
            }

            return successfulRemove;
        }

        public Account CreateAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }
            if (account.CustomerId == null)
            {
                throw new ArgumentNullException(nameof(account.CustomerId));
            }

            var selectedCustomer = GetCustomer(account.CustomerId);
            selectedCustomer.Accounts.ToList().Add(account);

            SaveNewCustomer(selectedCustomer);

            return account;
        }

        public Account GetAccount(Guid CustomerId, Guid AccountId)
        {
            if (CustomerId == null)
            {
                throw new ArgumentNullException(nameof(CustomerId));
            }
            if (AccountId == null)
            {
                throw new ArgumentNullException(nameof(AccountId));
            }

            var customer = GetCustomer(CustomerId);
            if (customer == null)
            {
                throw new NullReferenceException("The customer was not found.");
            }

            var selectedAccount = customer.Accounts.Where(a => a.AccountId == AccountId).FirstOrDefault();
            if (selectedAccount == null)
            {
                throw new NullReferenceException("The account was not found.");
            }

            return selectedAccount;
        }

        public void SaveNewAccountAsync(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            var customerList = GetAllCustomers();
            var customer = customerList.Where(c => c.CustomerId == account.CustomerId).FirstOrDefault();
            var accountList = customer.Accounts.ToList();
            accountList.Add(account);
            customer.Accounts = accountList;

            SaveCustomers(customerList);
        }

        public bool DeleteAccount(Guid customerId, Guid accountId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            if (accountId == null)
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            var customerList = GetAllCustomers();
            var accountToDelete = customerList.Where(c => c.CustomerId == customerId).FirstOrDefault()
                .Accounts.Where(a => a.AccountId == accountId).FirstOrDefault();
            
            if (accountToDelete == null)
            {
                throw new NullReferenceException("Unable to find the account.");
            }

            var successfulRemove = customerList.Where(c => c.CustomerId.Equals(customerId)).FirstOrDefault()
                .Accounts.ToList().Remove(accountToDelete);

            if (successfulRemove)
            {
                SaveCustomers(customerList);
            }

            return successfulRemove;
        }

        public Transaction CreateTransaction(Guid customerId, Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            if (transaction.AccountId == null)
            {
                throw new ArgumentNullException(nameof(transaction.AccountId));
            }


            var selectedCustomer = GetCustomer(customerId);
            var selectedAccount = selectedCustomer.Accounts.Where(a => a.AccountId == transaction.AccountId).FirstOrDefault();
            selectedAccount.Transactions.ToList().Add(transaction);

            SaveNewCustomer(selectedCustomer);

            return transaction;
        }

        public Transaction GetTransaction(Guid customerId, Guid accountId, Guid transactionId)
        {
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            if (accountId == null)
            {
                throw new ArgumentNullException(nameof(accountId));
            }
            if (transactionId == null)
            {
                throw new ArgumentNullException(nameof(transactionId));
            }

            var customer = GetCustomer(customerId);
            var selectedTransaction = customer.Accounts.Where(a => a.AccountId == accountId).FirstOrDefault()
                .Transactions.Where(t => t.TransactionId == transactionId).FirstOrDefault();
            return selectedTransaction;
        }

        public bool DeleteTransaction(Guid customerId, Guid accountId, Guid transactionId)
        {
            if (accountId == null)
            {
                throw new ArgumentNullException(nameof(accountId));
            }
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            if (transactionId == null)
            {
                throw new ArgumentNullException(nameof(transactionId));
            }

            var customerList = GetAllCustomers();
            var selectedTransaction = customerList.Where(c => c.CustomerId.Equals(customerId)).FirstOrDefault()
                .Accounts.Where(a => a.AccountId == accountId).FirstOrDefault()
                .Transactions.Where(t => t.TransactionId == transactionId).FirstOrDefault();

            var successfulRemove = customerList.Where(c => c.CustomerId.Equals(customerId)).FirstOrDefault()
                .Accounts.Where(a => a.AccountId == accountId).FirstOrDefault()
                .Transactions.ToList().Remove(selectedTransaction);

            if (successfulRemove)
            {
                SaveCustomers(customerList);
            }

            return successfulRemove;
        }

        public void SaveNewCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var customerList = GetAllCustomers();
            customerList.Add(customer);

            SaveCustomers(customerList);
        }

        public void UpdateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var customerList = GetAllCustomers();
            
            var oldCustomerRecord = customerList.Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();
            oldCustomerRecord.Email = customer.Email;
            oldCustomerRecord.FirstName = customer.FirstName;
            oldCustomerRecord.LastName = customer.LastName;
            oldCustomerRecord.MailingAddressCity = customer.MailingAddressCity;
            oldCustomerRecord.MailingAddressCountry = customer.MailingAddressCountry;
            oldCustomerRecord.MailingAddressState = customer.MailingAddressState;
            oldCustomerRecord.MailingAddressStreet = customer.MailingAddressStreet;
            oldCustomerRecord.MailingAddressZip = customer.MailingAddressZip;
            oldCustomerRecord.MobilePhone = customer.MobilePhone;
            
            SaveCustomers(customerList);
        }

        public void SaveCustomers(IList<Customer> customers)
        {
            if (customers == null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                var jsonString = JsonSerializer.Serialize(customers, options);
                File.WriteAllText(CustomerJsonFile, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}