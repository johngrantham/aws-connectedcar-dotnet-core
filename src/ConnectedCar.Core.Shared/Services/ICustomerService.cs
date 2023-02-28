using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Updates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface ICustomerService
    {
        Task CreateCustomer(Customer customer);

        Task UpdateCustomer(CustomerPatch patch);

        Task DeleteCustomer(string username);

        Task<Customer> GetCustomer(string username);

        Task<List<Customer>> GetCustomers(string lastname);

        Task BatchUpdate(List<Customer> customers);

    }
}
