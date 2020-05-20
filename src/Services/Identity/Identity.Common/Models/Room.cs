using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class Room
    {

        public string Id { get; set; }

        public string BuildingId { get; set; }
        public Building Building { get; set; }

        public List<Employee> Employees { get; set; }

    }
}
