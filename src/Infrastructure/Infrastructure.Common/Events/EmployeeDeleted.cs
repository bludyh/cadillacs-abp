using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class EmployeeDeleted : Event
    {
        public int Id { get; set; }
    }
}
