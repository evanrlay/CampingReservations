using System;
using System.Collections.Generic;

namespace CampingReservations
{
    public class Search
    {
        private int gap;
        private DateTime searchStartDate;
        private DateTime searchEndDate;
        private List<Campsite> campsites;
        private List<Reservation> reservations;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dayGap">Length of time allowed between reservations</param>
        /// <param name="start">The start date search parameter</param>
        /// <param name="end">The end date search parameter</param>
        /// <param name="sites">List of available campsites</param>
        /// <param name="reserved">List of current reservations</param>
        public Search(int dayGap, DateTime start, DateTime end, List<Campsite> sites, List<Reservation> reserved)
        {
            gap = dayGap;
            searchStartDate = start;
            searchEndDate = end;
            campsites = sites;
            reservations = reserved;            
        }

        /// <summary>
        /// Search function to determine which sites meet all requirements for reservation
        /// </summary>
        /// <returns>a list of available campsites</returns>
        public List<Campsite> SearchForReservations()
        {
            ValidateInfo();

            foreach (var reservation in reservations)
            {
                var siteID = reservation.campsiteId;
                var reservedCampsite = campsites.Find(x => x.GetID() == siteID);
                var invalid = false;

                //ensure campsite has not already been removed
                if (reservedCampsite != null)
                {
                    //check for reservations that begin before the reservation search start date
                    if (reservation.startDate < searchStartDate)
                    {
                        //invalid if reservation ends after the search start date
                        if (reservation.endDate >= searchStartDate)
                        {
                            invalid = true;
                        }
                        //invalid if the reservation ends and leaves a gap equal to the gap rule before the reservation start date
                        else if (reservation.endDate >= searchStartDate.AddDays(-1 * (1 + gap)) && reservation.endDate < searchStartDate.AddDays(-1))
                        {
                            invalid = true;
                        }

                    }
                    //check for reservations that begin in the middle of the search dates
                    else if(reservation.startDate >= searchStartDate && reservation.startDate <= searchEndDate)
                    {
                        invalid = true;
                    }
                    //check reservations beginning after the search parameters
                    else
                    {
                        //invalid if reservation does not start the day after the search or more than the gap length before the search
                        if (reservation.startDate != searchEndDate.AddDays(1))
                        {
                            if (reservation.startDate <= searchEndDate.AddDays(1 + gap) && reservation.startDate > searchEndDate.AddDays(1))
                            {
                                invalid = true;
                            }
                        }
                    }

                    if (invalid)
                    {
                        campsites.Remove(reservedCampsite);
                    }
                }
            }

            return campsites;
        }

        /// <summary>
        /// Method to ensure passed in parameters are valid before trying to search for a reservation
        /// </summary>
        private void ValidateInfo()
        {
            if(searchStartDate == null || searchEndDate == null)
            {
                throw new Exception("Search parameters are invalid.");
            }

            if(campsites.Count == 0)
            {
                throw new Exception("Empty list of campsites given.");
            }
        }
    }
}
