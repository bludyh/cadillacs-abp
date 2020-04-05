using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models {
    public class Student : User {

        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ProgramId { get; set; }
        public Program Program { get; set; }

        public List<Mentor> Mentors { get; set; }

    }
}
