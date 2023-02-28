using Microsoft.Extensions.DependencyInjection;
using ConnectedCar.Core.Services;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Tools.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace ConnectedCar.Core.Tools.Commands
{
    public abstract class BaseCommand
    {
        protected const string DealersFilePath = @"Data/Dealers";
        protected const string CustomersFilePath = @"Data/Customers";
        protected const string CredentialsFilePath = @"Data/Credentials";
        
        protected const string DateFormat = "yyyy-MM-dd";

        private readonly ServiceProvider serviceProvider;
        private static Random random = new Random();

        public BaseCommand()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IServiceContext, LocalServiceContext>();
            services.AddSingleton<ITranslator, Translator>();
            services.AddSingleton<IAppointmentService, AppointmentService>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IDealerService, DealerService>();
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<ITimeslotService, TimeslotService>();
            services.AddSingleton<IVehicleService, VehicleService>();
            services.AddSingleton<IUserService, UserService>();

            serviceProvider = services.BuildServiceProvider();
        }

        protected IAppointmentService GetAppointmentService()
        {
            return serviceProvider.GetRequiredService<IAppointmentService>();
        }

        protected ICustomerService GetCustomerService()
        {
            return serviceProvider.GetRequiredService<ICustomerService>();
        }

        protected IDealerService GetDealerService()
        {
            return serviceProvider.GetRequiredService<IDealerService>();
        }

        protected IRegistrationService GetRegistrationService()
        {
            return serviceProvider.GetRequiredService<IRegistrationService>();
        }

        protected ITimeslotService GetTimeslotService()
        {
            return serviceProvider.GetRequiredService<ITimeslotService>();
        }

        protected IVehicleService GetVehicleService()
        {
            return serviceProvider.GetRequiredService<IVehicleService>();
        }

        protected IUserService GetUserService()
        {
            return serviceProvider.GetRequiredService<IUserService>();
        }

        protected static StateCodeEnum GetRandomStateCode()
        {
            var values = Enum.GetValues(typeof(StateCodeEnum));
            return (StateCodeEnum)values.GetValue(random.Next(values.Length));
        }   

        protected static T GetRandomItem<T>(List<T> items)
        {
            if (items.Count > 0)
            {
                return items[random.Next(items.Count)];
            }

            return default(T);
        } 

        protected static List<DealerData> ReadDealerData(string file)
        {
            CsvParser<DealerData> parser = new CsvParser<DealerData>(
                new CsvParserOptions(true, ';'), 
                new DealerMapping());

            var results = parser.ReadFromFile(file, Encoding.ASCII).ToList();

            return results.Select(r => r.Result).ToList();
        }

        protected static List<CustomerData> ReadCustomerData(string file)
        {
            CsvParser<CustomerData> parser = new CsvParser<CustomerData>(
                new CsvParserOptions(true, ';'), 
                new CustomerMapping());

            var results = parser.ReadFromFile(file, Encoding.ASCII).ToList();

            return results.Select(r => r.Result).ToList();
        }
    }


}