using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class RoomCreated : Event
    {
        public string Id { get; set; }
        public string BuildingId { get; set; }
    }
}
