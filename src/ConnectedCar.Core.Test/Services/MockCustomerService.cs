using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Updates;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockCustomerService : ICustomerService
    {
        private Dictionary<string,Customer> customers = new Dictionary<string,Customer>();

        public Task CreateCustomer(Customer customer)
        {
            if (customer == null || !customer.Validate())
                throw new InvalidOperationException();

            if (customers.ContainsKey(customer.Username))
                throw new InvalidOperationException();

            customer.CreateDateTime = DateTime.Now;
            customer.UpdateDateTime = DateTime.Now;

            customers.Add(customer.Username, customer);

            return Task.CompletedTask;
        }

        public Task UpdateCustomer(CustomerPatch patch)
        {
            if (patch == null || !patch.Validate())
                throw new InvalidOperationException();

            if (customers.ContainsKey(patch.Username))
            {
                Customer customer = customers[patch.Username];

                customer.PhoneNumber = patch.PhoneNumber;
                customer.UpdateDateTime = DateTime.Now;
            }

            return Task.CompletedTask;
        }

        public Task DeleteCustomer(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException();
                
            customers.Remove(username);

            return Task.CompletedTask;
        }

        public Task<Customer> GetCustomer(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException();

            if (customers.ContainsKey(username))
            {
                return Task.FromResult(customers[username]);
            }                

            return Task.FromResult<Customer>(null);
        }

        public Task<List<Customer>> GetCustomers(string lastname)
        {
            if (string.IsNullOrEmpty(lastname))
                throw new InvalidOperationException();

            var results = customers
                .Where(p => p.Value.Lastname.ToLower().StartsWith(lastname.ToLower()))
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results); 
        }

        public Task BatchUpdate(List<Customer> customers)
        {
            if (customers == null)
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }
    }
}