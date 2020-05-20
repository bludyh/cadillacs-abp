using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class RoomDeleted : Event
    {
        public string Id { get; set; }
        public string BuildingId { get; set; }
    }
}
