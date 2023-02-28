using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Updates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IRegistrationService
    {
        Task CreateRegistration(Registration registration);

        Task UpdateRegistration(RegistrationPatch patch);

        Task DeleteRegistration(string username, string vin);

        Task<Registration> GetRegistration(string username, string vin);

        Task<List<Registration>> GetCustomerRegistrations(string username);

        Task<List<Registration>> GetVehicleRegistrations(string vin);

        Task BatchUpdate(List<Registration> registrations);
    }
}
