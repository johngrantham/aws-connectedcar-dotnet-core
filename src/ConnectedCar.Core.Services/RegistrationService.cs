using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Updates;
using ConnectedCar.Core.Shared.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Services
{
    public class RegistrationService : BaseService,IRegistrationService
    {
        public RegistrationService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task CreateRegistration(Registration registration)
        {
            if (registration == null || !registration.Validate())
                throw new InvalidOperationException();

            RegistrationItem item = GetTranslator().translate(registration);

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var readConfig = new DynamoDBOperationConfig() { ConsistentRead = true };

            RegistrationItem existing = await dbContext.LoadAsync<RegistrationItem>(item.Username, item.Vin, readConfig);

            if (existing != null)
                throw new InvalidOperationException();

            item.StatusCode = StatusCodeEnum.Active;
            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            var batch = dbContext.CreateBatchWrite<RegistrationItem>();

            var queryConfig = new DynamoDBOperationConfig { IndexName = "VehicleRegistrationIndex" };

            List<RegistrationItem> previousItems = await dbContext.QueryAsync<RegistrationItem>(item.Vin, queryConfig).GetRemainingAsync();

            foreach (RegistrationItem previousItem in previousItems)
            {
                previousItem.StatusCode = StatusCodeEnum.Inactive;
                batch.AddPutItem(previousItem);
            }

            batch.AddPutItem(item);

            await batch.ExecuteAsync();
            await dbContext.LoadAsync<RegistrationItem>(item.Username, item.Vin, readConfig);
        }

        public async Task UpdateRegistration(RegistrationPatch patch)
        {
            if (patch == null || !patch.Validate())
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var operationConfig = new DynamoDBOperationConfig() { ConsistentRead = true };

            RegistrationItem item = await dbContext.LoadAsync<RegistrationItem>(patch.Username, patch.Vin);

            if (item != null)
            {
                item.StatusCode = patch.StatusCode;
                item.UpdateDateTime = DateTime.Now;

                await dbContext.SaveAsync(item);
                await dbContext.LoadAsync<RegistrationItem>(patch.Username, patch.Vin, operationConfig);
            }
        }

        public async Task DeleteRegistration(string username, string vin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            RegistrationItem item = await dbContext.LoadAsync<RegistrationItem>(username, vin);

            if (item != null)
            {
                await dbContext.DeleteAsync(item);
            }
        }

        public async Task<Registration> GetRegistration(string username, string vin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            RegistrationItem item = await dbContext.LoadAsync<RegistrationItem>(username, vin);
            return GetTranslator().translate(item);
        }

        public async Task<List<Registration>> GetCustomerRegistrations(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();

            List<RegistrationItem> items = await dbContext.QueryAsync<RegistrationItem>(username).GetRemainingAsync();

            return items
                .Where(p => p.StatusCode == StatusCodeEnum.Active)
                .Select(p => GetTranslator().translate(p))
                .ToList();
        }

        public async Task<List<Registration>> GetVehicleRegistrations(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            var config = new DynamoDBOperationConfig { IndexName = "VehicleRegistrationIndex" };

            List<RegistrationItem> items = await dbContext.QueryAsync<RegistrationItem>(vin, config).GetRemainingAsync();

            return items
                .Where(p => p.StatusCode == StatusCodeEnum.Active)
                .Select(p => GetTranslator().translate(p))
                .ToList();
        }

        public async Task BatchUpdate(List<Registration> registrations)
        {
            if (registrations == null)
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var batch = dbContext.CreateBatchWrite<RegistrationItem>();

            foreach (Registration registration in registrations)
            {
                if (registration.Validate())
                {
                    batch.AddPutItem(GetTranslator().translate(registration));
                }
            }

            await batch.ExecuteAsync();
        }
    }
}
