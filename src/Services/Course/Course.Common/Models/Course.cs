using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Course
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Credit { get; set; }

        public List<Class> Classes { get; set; }
    }
}
