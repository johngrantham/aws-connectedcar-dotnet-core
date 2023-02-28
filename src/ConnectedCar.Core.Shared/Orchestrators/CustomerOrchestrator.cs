using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConnectedCar.Core.Shared.Orchestrators
{
    public class CustomerOrchestrator : ICustomerOrchestrator
    {
        private IRegistrationService registrationService;
        private IAppointmentService appointmentService;
        private IVehicleService vehicleService;
        private IEventService eventService;

        public CustomerOrchestrator(
            IRegistrationService registrationService,
            IAppointmentService appointmentService,
            IVehicleService vehicleService,
            IEventService eventService)
        {
            this.registrationService = registrationService;
            this.appointmentService = appointmentService;
            this.vehicleService = vehicleService;
            this.eventService = eventService;
        }

        public async Task<string> CreateAppointment(string username, Appointment appointment)
        {
            Registration registration = await registrationService.GetRegistration(username, appointment.RegistrationKey.Vin);

            if (registration != null)
            {
                appointment.RegistrationKey.Username = username;

                return await appointmentService.CreateAppointment(appointment);
            }

            return null;
        }

        public async Task<Vehicle> GetVehicle(string username, string vin)
        {
            Registration registration = await registrationService.GetRegistration(username, vin);

            if (registration != null)
            {
                return await vehicleService.GetVehicle(registration.Vin);
            }

            return null;
        }

        public async Task<List<Event>> GetEvents(string username, string vin)
        {
            Registration registration = await registrationService.GetRegistration(username, vin);
            List<Event> events = new List<Event>();

            if (registration != null)
            {
                events.AddRange(await eventService.GetEvents(registration.Vin));
            }

            return events;
        }
    }
}