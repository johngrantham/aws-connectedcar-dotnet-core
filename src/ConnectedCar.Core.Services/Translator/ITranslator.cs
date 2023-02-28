using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Services.Data.Items;

namespace ConnectedCar.Core.Services.Translator
{
    public interface ITranslator
    {
        Appointment translate(AppointmentItem item);
        AppointmentItem translate(Appointment entity);

        Customer translate(CustomerItem item);
        CustomerItem translate(Customer entity);

        Dealer translate(DealerItem item);
        DealerItem translate(Dealer entity);

        Event translate(EventItem item);
        EventItem translate(Event entity);

        Registration translate(RegistrationItem item);
        RegistrationItem translate(Registration entity);

        Timeslot translate(TimeslotItem item);
        TimeslotItem translate(Timeslot entity);

        Vehicle translate(VehicleItem item);
        VehicleItem translate(Vehicle entity);
    }
}