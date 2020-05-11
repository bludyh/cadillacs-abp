using System;
using System.Collections.Generic;

namespace Identity.EventHandler.Models
{
    public partial class School
    {
        public School()
        {
            Programs = new HashSet<Program>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
    }
}
