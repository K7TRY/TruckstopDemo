using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TruckstopDemoWebApi.Managers;
using TruckstopDemoWebApi.Models;

namespace TruckstopDemoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerManager customerManager;

        public CustomerController(ILogger<CustomerController> logger, CustomerManager customerManager)
        {
            _logger = logger;
            this.customerManager = customerManager;
        }

        #region Customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            IEnumerable<Customer> customerList;

            try
            {
                customerList = customerManager.GetAllCustomers().ToList();
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult("Unable to open the database.") { StatusCode = 500 });
            }

            if (customerList == null)
            {
                return StatusCode(404, "The customer list is empty.");
            }

            return Ok(customerList);
        }

        [HttpGet("{CustomerId}", Name = "GetCustomer")]
        public ActionResult<Customer> GetCustomer(Guid CustomerId)
        {
            Customer customer;
            
            try
            {
                customer = customerManager.GetCustomer(CustomerId);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid customer object sent from client.");
                return BadRequest(ModelState.ValidationState);
            }

            // This should check to see if there is already a customer with the same info in the database.
            try
            {
                customerManager.SaveNewCustomer(customer);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            return CreatedAtRoute(routeName: "GetCustomer", routeValues: new { customer.CustomerId }, value: customer);
        }

        [HttpPut("{CustomerId}")]
        public IActionResult UpdateCustomer([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid customer object sent from client.");
                return BadRequest("Invalid model object");
            }

            try { 
                customerManager.UpdateCustomerAsync(customer);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            return RedirectToRoute(new
            {
                controller = "Customer",
                action = "GetCustomer",
                CustomerId = customer.CustomerId
            });
        }

        [HttpDelete("{CustomerId}")]
        public ActionResult DeleteCustomer(Guid CustomerId)
        {
            try
            {
                if (customerManager.DeleteCustomer(CustomerId))
                {
                    return NotFound(new JsonResult("The customer could not be deleted. Either it was not found, or an internal server error occurred.") { StatusCode = 404 });
                }
                else
                {
                    return Ok(new JsonResult("Customer Deleted") { StatusCode = 204 });
                }
            }
            catch(Exception ex)
            {
                return NotFound(new JsonResult("The customer could not be deleted. Either it was not found, or an internal server error occurred.") { StatusCode = 500 });
            }
            
        }
        #endregion Customers

        #region Accounts
        [HttpGet("{CustomerId}/Accounts")]
        public ActionResult<IEnumerable<Account>> GetAccounts(Guid CustomerId)
        {
            IEnumerable<Account> account;

            try
            {
                var customer = customerManager.GetCustomer(CustomerId);
                account = customer.Accounts;
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            return Ok(account);
        }

        [HttpGet("{CustomerId}/Account/{AccountId}", Name = "GetAccount")]
        public ActionResult<Account> GetAccount(Guid CustomerId, Guid AccountId)
        {
            Account selectedAccount;

            try {
                selectedAccount = customerManager.GetAccount(CustomerId, AccountId);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            return Ok(selectedAccount);
        }

        [HttpPost("{CustomerId}/Account")]
        public IActionResult CreateAccount([FromBody]Account account)
        {
            // This should check to see if there is already an account with the same info in the database.
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid customer object sent from client.");
                return BadRequest(ModelState.ValidationState);
            }

            try { 
                customerManager.SaveNewAccountAsync(account);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            return CreatedAtRoute(routeName: "GetAccount", routeValues: new { account.CustomerId, account.AccountId }, value: account);
        }

        [HttpDelete("{CustomerId}/Account/{AccountId}")]
        public ActionResult DeleteAccount(Guid CustomerId, Guid AccountId)
        {
            bool deleted;

            try { 
                deleted = customerManager.DeleteAccount(CustomerId, AccountId);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult(ex.Message) { StatusCode = 404 });
            }

            if (deleted)
            {
                return Ok();
            }

            return BadRequest("Could not delete the account.");
        }
        #endregion Accounts

        #region Transactions
        [HttpGet("{CustomerId}/Account/{AccountId}/Transactions")]
        public ActionResult<IEnumerable<Transaction>> GetTransactions(Guid CustomerId, Guid AccountId)
        {
            IEnumerable<Transaction> orderedTransactions;

            try
            {
                Account selectedAccount = customerManager.GetAccount(CustomerId, AccountId);
                orderedTransactions = selectedAccount.OrderedTransactions();
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult("Could not get sorted transactions.") { StatusCode = 404 });
            }

            return Ok(orderedTransactions);
        }

        [HttpGet("{CustomerId}/Account/{AccountId}/Transaction/{TransactionId}")]
        public ActionResult<Transaction> GetTransaction(Guid CustomerId, Guid AccountId, Guid TransactionId)
        {
            Transaction selectedTransaction;

            try
            {
                selectedTransaction = customerManager.GetTransaction(CustomerId, AccountId, TransactionId);
            }
            catch (Exception ex)
            {
                return NotFound(new JsonResult("Could not get transaction.") { StatusCode = 404 });
            }

            return Ok(selectedTransaction);
        }

        [HttpDelete("{CustomerId}/Account/{AccountId}/Transaction/{TransactionId}")]
        public ActionResult DeleteTransaction(Guid CustomerId, Guid AccountId, Guid TransactionId)
        {
            if (customerManager.DeleteTransaction(CustomerId, AccountId, TransactionId))
            {
                return Ok();
            }
            else 
            { 
                return NotFound(new JsonResult("Could not delete transaction.") { StatusCode = 404 });
            }

        }
        #endregion Transactions
    }
}
