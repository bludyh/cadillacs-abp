using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Models {
    public class Employee : User {

        public string RoomId { get; set; }
        public string BuildingId { get; set; }
        public Room Room { get; set; } 

        public List<EmployeeProgram> EmployeePrograms { get; set; }

    }
}
