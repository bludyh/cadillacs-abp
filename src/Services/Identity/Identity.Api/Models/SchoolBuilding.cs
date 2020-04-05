using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models {
    public class SchoolBuilding {

        public string SchoolId { get; set; }
        public School School { get; set; }

        public string BuildingId { get; set; }
        public Building Building { get; set; }

    }
}
