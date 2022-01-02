using System;
using System.Collections.Generic;

#nullable disable

namespace Db_CodeFirst.DAL
{
    public partial class Trip
    {
        public Trip()
        {
            PassInTrips = new HashSet<PassInTrip>();
        }

        public int TripNo { get; set; }
        public int CompId { get; set; }
        public string Plane { get; set; }
        public string TownFrom { get; set; }
        public string TownTo { get; set; }
        public DateTime TimeOut { get; set; }
        public DateTime TimeIn { get; set; }

        public virtual Company Comp { get; set; }
        public virtual ICollection<PassInTrip> PassInTrips { get; set; }
    }
}
