using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos {
    public class ProgramDto {

        public string Id { get; set; }
        public string Name { get; set; }

        public SchoolDto School { get; set; }

    }
}
