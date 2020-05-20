using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class CourseDeleted : Event
    {
        public string Id { get; set; }
    }
}
