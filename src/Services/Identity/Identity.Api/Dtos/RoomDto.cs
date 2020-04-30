using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class RoomDto {

        public string Id { get; set; }

        public BuildingDto Building { get; set; }

    }
}
