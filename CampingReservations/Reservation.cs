using System;

namespace CampingReservations
{
    public class Reservation
    {
        public int campsiteId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        /// <summary>
        /// Create a readable string for a reservation
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return campsiteId + ": " + startDate.ToShortDateString() + " - " + endDate.ToShortDateString();
        }
    }

}
