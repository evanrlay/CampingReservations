using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingReservations
{
    public class Campsite
    {
        public int ID { get; set; }
        public string name { get; set; }

        public int GetID()
        {
            return ID;
        }

        public override string ToString()
        {
            return name; 
        }
    }
}
