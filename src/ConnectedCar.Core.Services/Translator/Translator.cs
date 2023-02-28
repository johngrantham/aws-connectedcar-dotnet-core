using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Services.Data.Items;

namespace ConnectedCar.Core.Services.Translator
{
    public class Translator : ITranslator
    {
        public Appointment translate(AppointmentItem item) 
        {
            if (item != null)
            {
                return new Appointment
                {
                    AppointmentId = item.AppointmentId,
                    TimeslotKey = item.TimeslotKey,
                    RegistrationKey = item.RegistrationKey,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public AppointmentItem translate(Appointment entity) 
        {
            if (entity != null)
            {
                return new AppointmentItem
                {
                    AppointmentId = entity.AppointmentId,
                    TimeslotKey = entity.TimeslotKey,
                    RegistrationKey = entity.RegistrationKey,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }

        public Customer translate(CustomerItem item)
        {
            if (item != null)
            {
                return new Customer
                {
                    Username = item.Username,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    PhoneNumber = item.PhoneNumber,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public CustomerItem translate(Customer entity) 
        {
            if (entity != null)
            {
                return new CustomerItem
                {
                    Username = entity.Username,
                    Firstname = entity.Firstname,
                    Lastname = entity.Lastname,
                    LastnameLower = entity.Lastname != null ? entity.Lastname : null,
                    PhoneNumber = entity.PhoneNumber,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }

        public Dealer translate(DealerItem item)
        {
            if (item != null)
            {
                return new Dealer
                {
                    DealerId = item.DealerId,
                    Name = item.Name,
                    Address = item.Address,
                    StateCode = item.StateCode,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public DealerItem translate(Dealer entity) 
        {
            if (entity != null)
            {
                return new DealerItem
                {
                    DealerId = entity.DealerId,
                    Name = entity.Name,
                    Address = entity.Address,
                    StateCode = entity.StateCode,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }

        public Event translate(EventItem item)
        {
            if (item != null)
            {
                return new Event
                {
                    Vin = item.Vin,
                    Timestamp = item.Timestamp,
                    EventCode = item.EventCode,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public EventItem translate(Event entity) 
        {
            if (entity != null)
            {
                return new EventItem
                {
                    Vin = entity.Vin,
                    Timestamp = entity.Timestamp,
                    EventCode = entity.EventCode,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }

        public Registration translate(RegistrationItem item)
        {
            if (item != null)
            {
                return new Registration
                {
                    Username = item.Username,
                    Vin = item.Vin,
                    StatusCode = item.StatusCode,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public RegistrationItem translate(Registration entity) 
        {
            if (entity != null)
            {
                return new RegistrationItem
                {
                    Username = entity.Username,
                    Vin = entity.Vin,
                    StatusCode = entity.StatusCode,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }

        public Timeslot translate(TimeslotItem item)
        {
            if (item != null)
            {
                return new Timeslot
                {
                    DealerId = item.DealerId,
                    ServiceDateHour = item.ServiceDateHour,
                    ServiceBayCount = item.ServiceBayCount,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public TimeslotItem translate(Timeslot entity) 
        {
            if (entity != null)
            {
                return new TimeslotItem
                {
                    DealerId = entity.DealerId,
                    ServiceDateHour = entity.ServiceDateHour,
                    ServiceBayCount = entity.ServiceBayCount,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }

        public Vehicle translate(VehicleItem item)
        {
            if (item != null)
            {
                return new Vehicle
                {
                    Vin = item.Vin,
                    Colors = item.Colors,
                    VehiclePin = item.VehiclePin,
                    VehicleCode = item.VehicleCode,
                    CreateDateTime = item.CreateDateTime,
                    UpdateDateTime = item.UpdateDateTime
                };
            }

            return null;
        }

        public VehicleItem translate(Vehicle entity) 
        {
            if (entity != null)
            {
                return new VehicleItem
                {
                    Vin = entity.Vin,
                    Colors = entity.Colors,
                    VehiclePin = entity.VehiclePin,
                    VehicleCode = entity.VehicleCode,
                    CreateDateTime = entity.CreateDateTime,
                    UpdateDateTime = entity.UpdateDateTime
                };
            }

            return null;
        }        
    }
}