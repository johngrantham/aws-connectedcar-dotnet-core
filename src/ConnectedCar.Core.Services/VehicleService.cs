using Amazon.DynamoDBv2.DataModel;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConnectedCar.Core.Services
{
    public class VehicleService : BaseService,IVehicleService
    {
        public VehicleService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            if (vehicle == null || !vehicle.Validate())
                throw new InvalidOperationException();

            VehicleItem item = GetTranslator().translate(vehicle);

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var operationConfig = new DynamoDBOperationConfig() { ConsistentRead = true };

            VehicleItem existing = await dbContext.LoadAsync<VehicleItem>(item.Vin, operationConfig);

            if (existing != null)
                throw new InvalidOperationException();

            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            await dbContext.SaveAsync(item);
            await dbContext.LoadAsync<VehicleItem>(item.Vin, operationConfig);
        }

        public async Task DeleteVehicle(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            VehicleItem item = await dbContext.LoadAsync<VehicleItem>(vin);

            if (item != null)
            {
                await dbContext.DeleteAsync(item);
            }
        }

        public async Task<Vehicle> GetVehicle(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            VehicleItem item = await dbContext.LoadAsync<VehicleItem>(vin);
            
            return GetTranslator().translate(item);
        }

        public async Task<bool> ValidatePin(string vin, string vehiclePin)
        {
            if (string.IsNullOrEmpty(vin) || string.IsNullOrEmpty(vehiclePin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            VehicleItem item = await dbContext.LoadAsync<VehicleItem>(vin);
            
            return item != null && item.VehiclePin.Equals(vehiclePin);
        }

        public async Task BatchUpdated(List<Vehicle> vehicles)
        {
            if (vehicles == null)
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var batch = dbContext.CreateBatchWrite<VehicleItem>();

            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.Validate())
                {
                    batch.AddPutItem(GetTranslator().translate(vehicle));
                }
            }

            await batch.ExecuteAsync();
        }
    }
}
