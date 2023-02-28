using ConnectedCar.Core.Shared.Data;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Updates;
using ConnectedCar.Core.Shared.Services;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Orchestrators
{
    public class AdminOrchestrator : IAdminOrchestrator
    {
        private readonly ICustomerService customerService;
        private readonly IMessageService messageService;
        private readonly IUserService userService;

        public AdminOrchestrator(
            ICustomerService customerService,
            IMessageService messageService,
            IUserService userService)
        {
            this.customerService = customerService;
            this.messageService = messageService;
            this.userService = userService;
        }

        public async Task CreateCustomer(CustomerProvision provision) 
        {   
            Customer customer = new Customer
            {
                Username = provision.Username,
                Firstname = provision.Firstname,
                Lastname = provision.Lastname,
                PhoneNumber = provision.PhoneNumber
            };

            await customerService.CreateCustomer(customer);

            User user = new User
            {
                Username = provision.Username,
                Password = provision.Password
            };

            await userService.CreateUser(user);
        }

        public async Task CreateCustomerUsingMessage(CustomerProvision provision) 
        {
            Customer customer = new Customer
            {
                Username = provision.Username,
                Firstname = provision.Firstname,
                Lastname = provision.Lastname,
                PhoneNumber = provision.PhoneNumber
            };

            await customerService.CreateCustomer(customer);

            User user = new User
            {
                Username = provision.Username,
                Password = provision.Password
            };

            await messageService.SendCreateUser(user);
        }
    }
}