using System;
using System.Collections.Generic;

#nullable disable

namespace Db_CodeFirst.DAL
{
    public partial class Passenger
    {
        public Passenger()
        {
            PassInTrips = new HashSet<PassInTrip>();
        }

        public int PassId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PassInTrip> PassInTrips { get; set; }
    }
}
