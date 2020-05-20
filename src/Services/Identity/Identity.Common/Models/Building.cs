using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class Building
    {

        public string Id { get; set; }

        public List<Room> Rooms { get; set; }
        public List<SchoolBuilding> SchoolBuildings { get; set; }

    }
}
