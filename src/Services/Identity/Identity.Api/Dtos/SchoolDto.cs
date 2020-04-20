using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class SchoolDto {

        public string Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
