using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Extensions;
using ConnectedCar.Core.Tools.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace ConnectedCar.Core.Tools.Commands
{
    public class PopulateDealersCommand : BaseCommand
    {
        public void Run()
        {
            var files = Directory.GetFiles(DealersFilePath);

            foreach (string file in files)
            {
                PopulateDealers(file);
            }
        }

        private void PopulateDealers(string file)
        {
            List<DealerData> results = ReadDealerData(file);

            List<List<DealerData>> chunks = results.ChunkBy(20);

            chunks.AsParallel().ForAll(d => {
                foreach (DealerData dealer in d)
                {
                    string dealerId = GetDealerService().CreateDealer(dealer.GetDealer()).GetAwaiter().GetResult();

                    DateTime today = DateTime.Today;

                    for (int dt = 0; dt < 7; dt++)
                    {
                        string serviceDate = today.AddDays(dt).ToString(DateFormat);
                        List<Timeslot> timeslots = new List<Timeslot>();

                        for (int h = 8; h <= 16; h++)
                        {
                            string serviceDateHour = serviceDate + "-" + h.ToString().PadLeft(2,'0');

                            timeslots.Add(new Timeslot
                            {
                                DealerId = dealerId,
                                ServiceDateHour = serviceDateHour,
                                ServiceBayCount = 10
                            });
                        }

                        GetTimeslotService().BatchUpdate(timeslots);
                    }
                }
            });
        }
    }
}