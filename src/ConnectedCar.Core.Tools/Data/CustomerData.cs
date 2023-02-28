using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Enums;
using System;

namespace ConnectedCar.Core.Tools.Data
{
    public class CustomerData
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Vin { get; set; }
        public string VehiclePin { get; set; }

        public Customer GetCustomer()
        {
            return new Customer
            {
                Username = Username,
                Firstname = Firstname,
                Lastname = Lastname,
                PhoneNumber = PhoneNumber
            };
        }

        public Vehicle GetVehicle()
        {
            return new Vehicle
            {
                Vin = Vin,
                Colors = new Colors
                {
                    Interior = "Black",
                    Exterior = "Silver"
                },
                VehicleCode = VehicleCodeEnum.Turbo,
                VehiclePin = VehiclePin
            };
        }

        public Registration GetRegistration()
        {
            return new Registration
            {
                Username = Username,
                Vin = Vin,
                StatusCode = StatusCodeEnum.Active
            };
        }
    }
}