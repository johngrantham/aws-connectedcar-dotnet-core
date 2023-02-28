using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ConnectedCar.Core.Services
{
    public class EventService : BaseService,IEventService
    {
        public EventService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task CreateEvent(Event evnt)
        {
            if (evnt == null || !evnt.Validate())
                throw new InvalidOperationException();

            EventItem item = GetTranslator().translate(evnt);

            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            var dbContext = GetServiceContext().GetDynamoDbContext();
            await dbContext.SaveAsync(item);
        }

        public async Task DeleteEvent(string vin, long timestamp)
        {
            if (string.IsNullOrEmpty(vin) || timestamp == 0)
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            EventItem item = await dbContext.LoadAsync<EventItem>(vin, timestamp);

            if (item != null)
            {
                await dbContext.DeleteAsync(item);
            }
        }

        public async Task<Event> GetEvent(string vin, long timestamp)
        {
            if (string.IsNullOrEmpty(vin) || timestamp == 0)
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            EventItem item = await dbContext.LoadAsync<EventItem>(vin, timestamp);

            return GetTranslator().translate(item);
        }

        public async Task<List<Event>> GetEvents(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();

            List<EventItem> items = await dbContext.QueryAsync<EventItem>(vin).GetRemainingAsync();

            return items.Select(p => GetTranslator().translate(p)).ToList();
        }
    }
}
