using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Services
{
    public class DealerService : BaseService,IDealerService
    {
        public DealerService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task<string> CreateDealer(Dealer dealer)
        {
            if (dealer == null || !dealer.Validate())
                throw new InvalidOperationException();

            DealerItem item = GetTranslator().translate(dealer);

            item.DealerId = Guid.NewGuid().ToString();
            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var operationConfig = new DynamoDBOperationConfig() { ConsistentRead = true };

            await dbContext.SaveAsync(item);
            await dbContext.LoadAsync<DealerItem>(item.DealerId, operationConfig);

            return item.DealerId;
        }

        public async Task DeleteDealer(string dealerId)
        {
            if (string.IsNullOrEmpty(dealerId))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            DealerItem item = await dbContext.LoadAsync<DealerItem>(dealerId);

            if (item != null)
            {
                await dbContext.DeleteAsync(item);
            }
        }

        public async Task<Dealer> GetDealer(string dealerId)
        {
            if (string.IsNullOrEmpty(dealerId))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            DealerItem item = await dbContext.LoadAsync<DealerItem>(dealerId);

            return GetTranslator().translate(item);
        }

        public async Task<List<Dealer>> GetDealers(StateCodeEnum stateCode)
        {
            var dbContext = GetServiceContext().GetDynamoDbContext();
            
            // Note that in this case the propertyName used in the conditions should be the mapped Dealer.cs property name NOT the serialized attribute name
            var conditions = new List<ScanCondition>() { new ScanCondition("StateCode", ScanOperator.Equal, new object[] { stateCode }) };

            List<DealerItem> items = await dbContext.ScanAsync<DealerItem>(conditions).GetRemainingAsync();
            
            return items.Select(p => GetTranslator().translate(p)).ToList();
        }
    }
}
