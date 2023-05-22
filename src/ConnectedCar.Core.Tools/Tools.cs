using System;
using System.Diagnostics;
using ConnectedCar.Core.Tools.Commands;

namespace ConnectedCar.Core.Tools
{
    public class Tools
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Type the command number and press enter");
                Console.WriteLine("  Type 1 to populate dealer data");
                Console.WriteLine("  Type 2 to populate customer data");
                Console.WriteLine("  Type 3 to populate appointment data");

                var correctInput = false;
                var timer = new Stopwatch();

                while (!correctInput)
                {
                    correctInput = true;

                    string input = Console.ReadLine();
                    int command;

                    if (int.TryParse(input, out command))
                    {
                        timer.Start();

                        switch (command)
                        {
                            case 1:
                                PopulateDealersCommand populateDealersCommand = new PopulateDealersCommand();
                                populateDealersCommand.Run();
                                break;
                            case 2:
                                PopulateCustomersCommand populateCustomersCommand = new PopulateCustomersCommand();
                                populateCustomersCommand.Run();
                                break;
                            case 3:
                                PopulateAppointmentsCommand populateAppointmentsCommand = new PopulateAppointmentsCommand();
                                populateAppointmentsCommand.Run();
                                break;
                        }

                        timer.Stop();
                    }
                    else
                    {
                        correctInput = false;
                    }
                }

                Console.WriteLine("Finished: " + timer.Elapsed);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
