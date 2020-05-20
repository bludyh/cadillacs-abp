using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Class> Classes { get; set; }
    }
}
