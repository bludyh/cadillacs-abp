using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class CourseUpdated : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credit { get; set; }
    }
}
