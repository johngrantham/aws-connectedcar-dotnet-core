using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Services.Data.Items;
using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Services;

namespace ConnectedCar.Core.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        public AppointmentService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task<string> CreateAppointment(Appointment appointment)
        {
            if (appointment == null || !appointment.Validate())
                throw new InvalidOperationException();

            AppointmentItem item = GetTranslator().translate(appointment);

            item.AppointmentId = Guid.NewGuid().ToString();
            item.CreateDateTime = DateTime.Now;
            item.UpdateDateTime = DateTime.Now;

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var operationConfig = new DynamoDBOperationConfig() { ConsistentRead = true };

            await dbContext.SaveAsync(item);
            await dbContext.LoadAsync<AppointmentItem>(item.AppointmentId, operationConfig);

            return item.AppointmentId;
        }

        public async Task DeleteAppointment(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            AppointmentItem item = await dbContext.LoadAsync<AppointmentItem>(appointmentId);

            if (item != null)
            {
                await dbContext.DeleteAsync(item);
            }
        }

        public async Task<Appointment> GetAppointment(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();
            AppointmentItem item = await dbContext.LoadAsync<AppointmentItem>(appointmentId);

            return GetTranslator().translate(item);
        }

        public async Task<List<Appointment>> GetRegistrationAppointments(string username, string vin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();

            var registrationKey = new RegistrationKey { Username = username, Vin = vin };
            var config = new DynamoDBOperationConfig { IndexName = "RegistrationAppointmentIndex" };

            List<AppointmentItem> items = await dbContext.QueryAsync<AppointmentItem>(registrationKey, config).GetRemainingAsync();
            
            return items.Select(item => GetTranslator().translate(item)).ToList();
        }

        public async Task<List<Appointment>> GetTimeslotAppointments(string dealerId, string serviceDateHour)
        {
            if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(serviceDateHour))
                throw new InvalidOperationException();
                
            var dbContext = GetServiceContext().GetDynamoDbContext();

            var timeslotKey = new TimeslotKey { DealerId = dealerId, ServiceDateHour = serviceDateHour };
            var config = new DynamoDBOperationConfig { IndexName = "TimeslotAppointmentIndex" };

            List<AppointmentItem> items = await dbContext.QueryAsync<AppointmentItem>(timeslotKey, config).GetRemainingAsync();

            return items.Select(p => GetTranslator().translate(p)).ToList();
        }

        public async Task BatchUpdate(List<Appointment> appointments)
        {
            if (appointments == null)
                throw new InvalidOperationException();

            var dbContext = GetServiceContext().GetDynamoDbContext();
            var batch = dbContext.CreateBatchWrite<AppointmentItem>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Validate())
                {
                    appointment.AppointmentId = Guid.NewGuid().ToString();
                    batch.AddPutItem(GetTranslator().translate(appointment));
                }
            }

            await batch.ExecuteAsync();
        }
    }
}
