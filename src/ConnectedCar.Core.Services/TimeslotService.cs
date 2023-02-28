using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;

namespace ConnectedCar.Core.Services
{
    public class TimeslotService : BaseService,ITimeslotService
    {
        public TimeslotService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task CreateTimeslot(Timeslot timeslot)
        {
            if (timeslot == null || !timeslot.Validate())
                throw new InvalidOperationException();

            TimeslotItem item = GetTranslator().translate(timeslot);

            var dbContext = GetServiceContext().GetDynamoDbContext();

            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            await dbContext.SaveAsync(item);
        }

        public async Task DeleteTimeslot(string dealerId, string serviceDateHour)
        {
            if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(serviceDateHour))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            TimeslotItem item = await dbContext.LoadAsync<TimeslotItem>(dealerId, serviceDateHour);

            if (item != null)
            {
                await dbContext.DeleteAsync(item);
            }
        }

        public async Task<Timeslot> GetTimeslot(string dealerId, string serviceDateHour)
        {
            if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(serviceDateHour))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            TimeslotItem item = await dbContext.LoadAsync<TimeslotItem>(dealerId, serviceDateHour);

            return GetTranslator().translate(item);
        }

        public async Task<List<Timeslot>> GetTimeslots(string dealerId, string startDate, string endDate)
        {
             if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                throw new InvalidOperationException();
                
           var dbContext = GetServiceContext().GetDynamoDbContext();
            var values = new Dictionary<string, DynamoDBEntry>();
            values.Add(":dealerId", dealerId);
            values.Add(":startDate", startDate);
            values.Add(":endDate", endDate);

            // Note that for this query the expression statement should use the serialized attribute names
            QueryOperationConfig query = new QueryOperationConfig
            {
                KeyExpression = new Expression{
                    ExpressionStatement = "dealerId = :dealerId AND serviceDateHour BETWEEN :startDate AND :endDate",
                    ExpressionAttributeValues = values
                }
            };

            List<TimeslotItem> items = await dbContext.FromQueryAsync<TimeslotItem>(query).GetRemainingAsync();

            return items.Select(p => GetTranslator().translate(p)).ToList();
        }

        public async Task BatchUpdate(List<Timeslot> timeslots)
        {
            if (timeslots == null)
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var batch = dbContext.CreateBatchWrite<TimeslotItem>();

            foreach (Timeslot timeslot in timeslots)
            {
                if (timeslot.Validate())
                {
                    batch.AddPutItem(GetTranslator().translate(timeslot));
                }
            }

            await batch.ExecuteAsync();
        }
    }
}
