using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Enums;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockVehicleService : IVehicleService
    {
        private Dictionary<string,Vehicle> vehicles = new Dictionary<string,Vehicle>();

        public Task CreateVehicle(Vehicle vehicle)
        {
            if (vehicle == null || !vehicle.Validate())
                throw new InvalidOperationException();

            if (vehicles.ContainsKey(vehicle.Vin))
                throw new InvalidOperationException();

            vehicle.CreateDateTime = DateTime.Now;
            vehicle.UpdateDateTime = DateTime.Now;

            vehicles.Add(vehicle.Vin, vehicle);

            return Task.CompletedTask;
        }

        public Task DeleteVehicle(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();

            vehicles.Remove(vin);
                
            return Task.CompletedTask;
        }

        public Task<Vehicle> GetVehicle(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            if (vehicles.ContainsKey(vin))
            {
                return Task.FromResult(vehicles[vin]);
            }                

            return Task.FromResult<Vehicle>(null);
        }

        public Task<bool> ValidatePin(string vin, string vehiclePin)
        {
            if (string.IsNullOrEmpty(vin) || string.IsNullOrEmpty(vehiclePin))
                throw new InvalidOperationException();
                
            if (vehicles.ContainsKey(vin))
            {
                return Task.FromResult(vehicles[vin].VehiclePin == vehiclePin);
            }                

            return Task.FromResult(false);
        }

        public Task BatchUpdated(List<Vehicle> vehicles)
        {
            if (vehicles == null)
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }
    }
}