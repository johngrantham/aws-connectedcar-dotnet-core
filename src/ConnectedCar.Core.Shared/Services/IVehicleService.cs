using ConnectedCar.Core.Shared.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IVehicleService
    {
        Task CreateVehicle(Vehicle vehicle);

        Task DeleteVehicle(string vin);

        public Task<Vehicle> GetVehicle(string vin);

        public Task<bool> ValidatePin(string vin, string vehiclePin);

        Task BatchUpdated(List<Vehicle> vehicles);
    }
}
