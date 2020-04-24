using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class StudentCreateDto {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Picture

        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ProgramId { get; set; }

    }
}
