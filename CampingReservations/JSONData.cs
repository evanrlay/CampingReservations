using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CampingReservations
{
    public class JSONData
    {
        public List<Campsite> campsites = new List<Campsite>();
        public List<Reservation> reservations = new List<Reservation>();
        public DateTime searchStartDate;
        public DateTime searchEndDate;

        public JSONData()
        {

        }

        /// <summary>
        /// Reads informationn from a JSON file and creates search parameters, a list of campsites, and a list of existing reservations
        /// </summary>
        /// <param name="filePath">String of an exisitng file path</param>
        /// <returns>true if the path is JSON and parses correctly</returns>
        public bool CreateData(string filePath)
        {
            JObject fileInformation;
            string json;

            //create a string with all the JSON information
            using (StreamReader reader = new StreamReader(filePath))
            {
                json = reader.ReadToEnd();
            }

            //try to parse the information, will return an error message and false if it fails
            try
            {
                fileInformation = JObject.Parse(json);
            }
            catch 
            {
                Console.WriteLine("There was an error parsing the JSON. Please check your file format.\nPress any key to exit.");
                return false;
            }

            //set the start and end date for the search
            try
            {
                searchStartDate = DateTime.Parse(fileInformation["search"]["startDate"].ToString());
                searchEndDate = DateTime.Parse(fileInformation["search"]["endDate"].ToString());
            }
            catch
            {
                Console.WriteLine("Missing search parameters or incorrect format.\nPress any key to exit.");
                return false;
            }

            //create a list of campsites
            try
            {
                IList<JToken> sites = fileInformation["campsites"].Children().ToList();
                foreach (var site in sites)
                {
                    Campsite campsite = site.ToObject<Campsite>();
                    campsites.Add(campsite);
                }
            }
            catch
            {
                Console.WriteLine("Missing campsite list or incorrect format.\nPress any key to exit.");
                return false;
            }

            //create a list of existing reservations
            try
            {
                IList<JToken> reservationList = fileInformation["reservations"].Children().ToList();
                foreach (var reservation in reservationList)
                {
                    Reservation reserved = reservation.ToObject<Reservation>();
                    reservations.Add(reserved);
                }
            }
            catch
            {
                Console.WriteLine("Missing reservation list or incorrect format.\nPress any key to exit.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Test to make sure JSON was parsed correctly
        /// </summary>
        private void TestCreatedData()
        {
            Console.WriteLine(searchStartDate);
            Console.WriteLine(searchEndDate);
            foreach (var reserve in reservations)
            {
                Console.WriteLine(reserve.ToString());
            }
            foreach (var site in campsites)
            {
                Console.WriteLine(site.ToString());
            }
            Console.ReadLine();
        }
    }
}
