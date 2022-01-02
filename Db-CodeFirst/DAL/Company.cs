using System;
using System.Collections.Generic;

#nullable disable

namespace Db_CodeFirst.DAL
{
    public partial class Company
    {
        public Company()
        {
            Trips = new HashSet<Trip>();
        }

        public int CompId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
