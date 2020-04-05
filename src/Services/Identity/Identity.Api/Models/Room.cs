using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models {
    public class Room {

        public string Id { get; set; }

        public string BuildingId { get; set; }
        public Building Building { get; set; }

        public List<Employee> Employees { get; set; }

    }
}
