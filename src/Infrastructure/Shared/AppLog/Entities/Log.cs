using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;

namespace Shared.AppLog.Entities
{
    public class Log
    {
        public Log(
            string level,
            string message,
            Exception? ex = null)
        {
            Id = ObjectId.GenerateNewId();
            DateTime = DateTime.Now;
            Level = level;
            Message = message;
            StackTrace = ex?.StackTrace;
            ExceptionMessage = ex?.Message;

#if DEBUG
            HostName = Dns.GetHostName();
#else
            HostName = Environment.GetEnvironmentVariable("HOSTNAME") ?? Dns.GetHostName();
#endif
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; internal set; }
        public string Level { get; internal set; }
        public DateTime DateTime { get; internal set; }
        public string HostName { get; set; }
        public string Message { get; internal set; }
        public string? ExceptionMessage { get; internal set; }
        public string? StackTrace { get; internal set; }

    }
}
