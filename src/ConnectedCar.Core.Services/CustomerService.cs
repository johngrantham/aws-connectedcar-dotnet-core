using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Updates;
using ConnectedCar.Core.Shared.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Services
{
    public class CustomerService : BaseService,ICustomerService
    {
        public CustomerService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task CreateCustomer(Customer customer)
        {
            if (customer == null || !customer.Validate())
                throw new InvalidOperationException();

            CustomerItem item = GetTranslator().translate(customer);

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var operationConfig = new DynamoDBOperationConfig() { ConsistentRead = true };

            CustomerItem existing = await dbContext.LoadAsync<CustomerItem>(item.Username, operationConfig);

            if (existing != null)
                throw new InvalidOperationException();

            item.LastnameLower = customer.Lastname.ToLower();
            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            await dbContext.SaveAsync(item);
            await dbContext.LoadAsync<CustomerItem>(item.Username, operationConfig);
        }

        public async Task UpdateCustomer(CustomerPatch patch)
        {
            if (patch == null || !patch.Validate())
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            CustomerItem item = await dbContext.LoadAsync<CustomerItem>(patch.Username);

            if (item != null)
            {
                item.PhoneNumber = patch.PhoneNumber;
                item.UpdateDateTime = DateTime.Now;

                await dbContext.SaveAsync(item);
            }
        }

        public async Task DeleteCustomer(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            CustomerItem item = await dbContext.LoadAsync<CustomerItem>(username);

            if (item != null)
            {
                await dbContext.DeleteAsync(username);
            }
        }

        public async Task<Customer> GetCustomer(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            CustomerItem item = await dbContext.LoadAsync<CustomerItem>(username);

            return GetTranslator().translate(item);
        }

        public async Task<List<Customer>> GetCustomers(string lastname)
        {
            if (string.IsNullOrEmpty(lastname))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();

            // Note that in this case the propertyName used in the conditions should be the mapped DealerItem.cs property name NOT the serialized attribute name
            var conditions = new List<ScanCondition>() { new ScanCondition("LastnameLower", ScanOperator.BeginsWith, new object[] { lastname.ToLower() }) };

            List<CustomerItem> items = await dbContext.ScanAsync<CustomerItem>(conditions).GetRemainingAsync();

            return items.Select(p => GetTranslator().translate(p)).ToList();
        }

        public async Task BatchUpdate(List<Customer> customers)
        {
            if (customers == null)
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var batch = dbContext.CreateBatchWrite<CustomerItem>();

            foreach (Customer customer in customers)
            {
                if (customer.Validate())
                {
                    batch.AddPutItem(GetTranslator().translate(customer));
                }
            }

            await batch.ExecuteAsync();
        }
    }
}
