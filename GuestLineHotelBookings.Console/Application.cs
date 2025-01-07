using GuestLineHotelBookings.Domain.DomainServices.Availability;

namespace GuestLineHotelBookings.Console
{
    public class Application
    {
        private const string DateTimeFormat = "yyyyMMdd";

        private readonly IAvailabilityService _availabilityService;

        public Application(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        public void Run()
        {
            System.Console.WriteLine("Welcome");
            System.Console.WriteLine("Please provide valid availability or search command.");
            System.Console.WriteLine("Example commands:");
            System.Console.WriteLine("Availability(H1, 20240901-20240903, DBL)");
            System.Console.WriteLine("Search(H1, 365, SGL)");

            while (true)
            {
                var command = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(command))
                {
                    System.Console.WriteLine("Exiting");
                    break;
                }

                if (command.Contains("Availability"))
                {
                    ProcessAvailabilityCommand(command);
                }
                else if (command.Contains("Search"))
                {
                    ProcessSearchCommand(command);
                }
                else
                {
                    System.Console.WriteLine("Invalid command.");
                }
            }
        }
        
        private void ProcessSearchCommand(string command)
        {
            string[] parameters;
            int daysAhead;

            try
            {
                parameters = new string(command.Substring(command.IndexOf('(') + 1).TrimEnd(')').Where(c => !char.IsWhiteSpace(c)).ToArray()).Split(',');

                daysAhead = int.Parse(parameters[1]);
            }
            catch
            {
                System.Console.WriteLine("Invalid command.");
                return;
            }

            try
            {

                var availabilities = _availabilityService.SearchAvailability(
                    parameters[0],
                    daysAhead,
                    parameters[2]);

                if (availabilities.Count == 0)
                {
                    System.Console.WriteLine();
                }
                else
                {
                    for (int i = 0; i < availabilities.Count; i++)
                    {
                        if (i == availabilities.Count - 1)
                        {
                            System.Console.WriteLine(availabilities[i].ToString());
                        }
                        else
                        {
                            System.Console.WriteLine(availabilities[i].ToString() + ",");
                        }
                    }
                }
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is ArgumentException)
            {
                System.Console.WriteLine("Invalid command.");
                return;
            }
            catch (Exception)
            {
                System.Console.WriteLine("Internal error. Please try again.");
                return;
            }
        }

        private void ProcessAvailabilityCommand(string command)
        {
            DateTime checkIn;
            DateTime? checkOut = null;
            string[] parameters;

            try
            {
                parameters = new string(command.Substring(command.IndexOf('(') + 1).TrimEnd(')').Where(c => !char.IsWhiteSpace(c)).ToArray()).Split(',');

                if (parameters[1].Contains('-'))
                {
                    var dates = parameters[1].Split('-');

                    checkIn = DateTime.ParseExact(dates[0], DateTimeFormat, null);
                    checkOut = DateTime.ParseExact(dates[1], DateTimeFormat, null);
                }
                else
                {
                    checkIn = DateTime.ParseExact(parameters[1], DateTimeFormat, null);
                }
            }
            catch
            {
                System.Console.WriteLine("Invalid command.");
                return;
            }

            try
            {
                var availability = _availabilityService.GetAvailability(
                    parameters[0],
                    parameters[2],
                    checkIn,
                    checkOut);

                System.Console.WriteLine(availability.ToString());
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is ArgumentException)
            {
                System.Console.WriteLine("Invalid command.");
                return;
            }
            catch (Exception)
            {
                System.Console.WriteLine("Internal error. Please try again.");
                return;
            }
        }
    }
}
