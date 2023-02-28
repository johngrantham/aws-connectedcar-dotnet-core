using System.Threading.Tasks;
using System.Collections.Generic;
using ConnectedCar.Core.Shared.Data.Entities;

namespace ConnectedCar.Core.Shared.Orchestrators
{
    public interface ICustomerOrchestrator
    {
        Task<string> CreateAppointment(string username, Appointment appointment);

        Task<Vehicle> GetVehicle(string username, string vin);

        Task<List<Event>> GetEvents(string username, string vin);
    }
}