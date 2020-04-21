using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Program> Programs { get; set; }
    }
}
