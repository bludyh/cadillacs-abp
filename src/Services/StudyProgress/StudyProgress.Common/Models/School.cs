using System.Collections.Generic;

namespace StudyProgress.Common.Models
{
    public class School
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Program> Programs { get; set; }
    }
}
