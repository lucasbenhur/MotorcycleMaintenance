using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Notifications.Entities
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
            Id = ObjectId.GenerateNewId();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; internal set; }

        public string Message { get; internal set; }
    }
}
