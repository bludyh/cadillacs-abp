using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models {
    public class Building {

        public string Id { get; set; }

        public List<Room> Rooms { get; set; }
        public List<SchoolBuilding> SchoolBuildings { get; set; }

    }
}
