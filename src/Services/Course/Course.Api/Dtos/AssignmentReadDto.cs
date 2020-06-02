using Course.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class AssignmentReadDto
    {
        public int Id { get; set; }

        public ClassReadDto Class { get; set; }

        public string Name { get; set; }
        public AssignmentType Type { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public int Weight { get; set; }
    }
}
