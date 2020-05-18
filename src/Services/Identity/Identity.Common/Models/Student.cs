using System;
using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class Student : User
    {

        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ProgramId { get; set; }
        public Program Program { get; set; }

        public List<Mentor> Mentors { get; set; }

    }
}
