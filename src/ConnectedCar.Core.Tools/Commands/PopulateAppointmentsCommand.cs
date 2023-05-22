using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Shared.Extensions;
using ConnectedCar.Core.Tools.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System;
using TinyCsvParser.Mapping;
using TinyCsvParser;

namespace ConnectedCar.Core.Tools.Commands
{
    public class PopulateAppointmentsCommand : BaseCommand
    {
        public void Run()
        {
            var customerFiles = Directory.GetFiles(CustomersFilePath);

            foreach (string file in customerFiles)
            {
                PopulateAppointments(file);
            }
        }

        private void PopulateAppointments(string file)
        {
            List<CustomerData> results = ReadCustomerData(file);
            List<CustomerData> filtered = results.GetRange(0, results.Count / 10);

            List<List<CustomerData>> chunks = filtered.ChunkBy(20);

            chunks.AsParallel().ForAll(c => {
                StateCodeEnum stateCode = GetRandomStateCode();
                string startDate = DateTime.Now.ToString(DateFormat);
                string endDate = DateTime.Now.AddDays(7).ToString(DateFormat);

                List<Dealer> dealers = GetDealerService().GetDealers(stateCode).GetAwaiter().GetResult();
                List<Appointment> appointments = new List<Appointment>();

                foreach (CustomerData result in c)
                {
                    Dealer dealer = GetRandomItem(dealers);

                    if (dealer != null)
                    {
                        List<Timeslot> timeslots = GetTimeslotService().GetTimeslots(dealer.DealerId, startDate, endDate).GetAwaiter().GetResult();
                        Timeslot timeslot = GetRandomItem(timeslots);

                        if (timeslot != null)
                        {
                            appointments.Add(new Appointment
                            {
                                TimeslotKey = new TimeslotKey 
                                {
                                    DealerId = dealer.DealerId,
                                    ServiceDateHour = timeslot.ServiceDateHour
                                },
                                RegistrationKey = new RegistrationKey
                                {
                                    Username = result.Username,
                                    Vin = result.Vin
                                }
                            });
                        }

                    }
                }

                GetAppointmentService().BatchUpdate(appointments);
            });
        }
    }
}