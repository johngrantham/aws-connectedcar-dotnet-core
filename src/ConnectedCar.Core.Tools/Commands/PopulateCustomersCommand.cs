using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Extensions;
using ConnectedCar.Core.Tools.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace ConnectedCar.Core.Tools.Commands
{
    public class PopulateCustomersCommand : BaseCommand
    {
        public void Run()
        {
            var customerFiles = Directory.GetFiles(CustomersFilePath);

            foreach (string file in customerFiles)
            {
                PopulateCustomers(file);
            }
        }

        private void PopulateCustomers(string file)
        {
            List<CustomerData> results = ReadCustomerData(file);

            List<List<CustomerData>> chunks = results.ChunkBy(20);

            chunks.AsParallel().ForAll(c => {
                List<Customer> customers = new List<Customer>();
                List<Vehicle> vehicles = new List<Vehicle>();
                List<Registration> registrations = new List<Registration>();

                foreach (CustomerData result in c)
                {
                    customers.Add(result.GetCustomer());
                    vehicles.Add(result.GetVehicle());
                    registrations.Add(result.GetRegistration());
                }

                GetCustomerService().BatchUpdate(customers);
                GetVehicleService().BatchUpdated(vehicles);
                GetRegistrationService().BatchUpdate(registrations);

                Console.WriteLine("Batch updates performed");
            });
        }
    }
}