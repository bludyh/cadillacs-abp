using System;

namespace Identity.Api.Dtos
{
    public class StudentReadDto
    {

        public int Id { get; set; }
        public string Pcn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Picture

        public string AccountStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ProgramReadDto Program { get; set; }

    }
}
