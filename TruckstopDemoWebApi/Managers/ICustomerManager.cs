using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckstopDemoWebApi.Models;

namespace TruckstopDemoWebApi.Managers
{
    public interface ICustomerManager
    {
        IList<Customer> GetAllCustomers();

        Customer GetCustomer(Guid CustomerId);

        Account CreateAccount(Account account);

        Transaction CreateTransaction(Guid customerId, Transaction transaction);

        async Task SaveCustomerAsync(Customer customer) { }

        async Task SaveCustomersAsync(IList<Customer> customers) { }
    }
}
