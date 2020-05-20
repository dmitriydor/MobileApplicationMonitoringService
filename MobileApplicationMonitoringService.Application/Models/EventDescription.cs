using MongoDB.Bson.Serialization.Attributes;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class EventDescription
    {
        [BsonId]
        public string EventName { get; set; }
        public string Description { get; set; }
    }
}
