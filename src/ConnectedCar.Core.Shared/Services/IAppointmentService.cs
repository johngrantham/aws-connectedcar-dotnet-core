using ConnectedCar.Core.Shared.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IAppointmentService
    {
        Task<string> CreateAppointment(Appointment appointment);

        Task DeleteAppointment(string appointmentId);

        Task<Appointment> GetAppointment(string appointmentId);

        Task<List<Appointment>> GetRegistrationAppointments(string username, string vin);

        Task<List<Appointment>> GetTimeslotAppointments(string dealerId, string serviceDateHour);

        Task BatchUpdate(List<Appointment> appointments);
    }
}
