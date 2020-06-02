using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class GroupReadDto
    {
        public int Id { get; set; }

        public ClassReadDto Class { get; set; }
        public string Name { get; set; }
        public int MaxSize { get; set; }
    }
}
