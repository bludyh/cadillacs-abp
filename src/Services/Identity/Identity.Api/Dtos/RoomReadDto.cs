using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class RoomReadDto {

        public string Id { get; set; }

        public BuildingReadDto Building { get; set; }

    }
}
