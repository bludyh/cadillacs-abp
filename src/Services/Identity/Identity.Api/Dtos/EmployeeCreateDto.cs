using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class EmployeeCreateDto {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Picture

        public string SchoolId { get; set; }
        public string RoomId { get; set; }
        public string BuildingId { get; set; }

    }
}
