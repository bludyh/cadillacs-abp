using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class Employee : User
    {

        public string SchoolId { get; set; }
        public School School { get; set; }
        public string RoomId { get; set; }
        public string BuildingId { get; set; }
        public Room Room { get; set; }

        public List<EmployeeProgram> EmployeePrograms { get; set; }

    }
}
