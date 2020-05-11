using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class ProgramReadDto {

        public string Id { get; set; }
        public string Name { get; set; }

        public SchoolCreateReadDto School { get; set; }

    }
}
