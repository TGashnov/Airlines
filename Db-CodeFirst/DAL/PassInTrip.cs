using System;
using System.Collections.Generic;

#nullable disable

namespace Db_CodeFirst.DAL
{
    public partial class PassInTrip
    {
        public int TripNo { get; set; }
        public DateTime Date { get; set; }
        public int PassId { get; set; }
        public string Place { get; set; }

        public virtual Passenger Pass { get; set; }
        public virtual Trip TripNoNavigation { get; set; }
    }
}
