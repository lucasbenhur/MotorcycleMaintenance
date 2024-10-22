using MongoDB.Bson.Serialization.Attributes;
using RentService.Core.Enums;

namespace RentService.Core.Entities
{
    public class Rent
    {
        public Rent(
            string deliveryManId,
            string motorcycleId,
            DateTime startDate,
            DateTime endDate,
            DateTime estimatedEndDate,
            RentPlan plan)
        {
            DeliveryManId = deliveryManId;
            MotorcycleId = motorcycleId;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedEndDate = estimatedEndDate;
            Plan = plan;
            Id = null;
            DailyValue = null;
            ReturnDate = null;
        }

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public int? DailyValue { get; set; }
        public string DeliveryManId { get; internal set; }
        public string MotorcycleId { get; internal set; }
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public DateTime EstimatedEndDate { get; internal set; }
        public RentPlan Plan { get; internal set; }
        public DateTime? ReturnDate { get; internal set; }
    }
}
