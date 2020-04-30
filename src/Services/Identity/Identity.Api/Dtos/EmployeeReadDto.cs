using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class EmployeeReadDto {

        public int Id { get; set; }
        public string Pcn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Picture

        public string AccountStatus { get; set; }

        public SchoolCreateReadDto School { get; set; }
        public RoomDto Room { get; set; }
    }
}
