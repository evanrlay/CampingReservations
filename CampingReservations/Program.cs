using System;
using System.IO;

namespace CampingReservations
{
    class Program
    {
        private const int GAP = 1;

        /// <summary>
        /// Query the user for a file input, validate the file exists, and call a method to handle the data
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //query user for data
            Console.Write("Please input the path to the JSON file with the necessary information: ");
            var filePath = Console.ReadLine();

            //ensure path is to an existing file before trying to create the data from the JSON
            while (!File.Exists(filePath))
            {
                Console.Write("Not a valid file path.\nPlease input the path to the JSON file with the necessary information: ");
                filePath = Console.ReadLine();
            }
            
            //Parse the JSON into variables
            var data = new JSONData();
            var success = data.CreateData(filePath);

            //Retrieve available campsites
            if (success)
            {
                Search search = new Search(GAP, data.searchStartDate, data.searchEndDate, data.campsites, data.reservations);
                var availableSites = search.SearchForReservations();

                //Display available sites
                foreach (var site in availableSites)
                {
                    Console.WriteLine(site.ToString());
                }
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }
            else
            {
                //Leave Console open to display message from CreateData
                Console.ReadLine();
            }

        }
    }
}
