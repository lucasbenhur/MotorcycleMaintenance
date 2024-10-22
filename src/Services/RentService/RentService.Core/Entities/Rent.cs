namespace RentService.Core.Entities
{
    public class Rent : BaseEntity
    {
        public string DeliveryManId { get; set; } = null!;
        public string MotorcycleId { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        private RentPlan Plan { get; set; }
    }

    public enum RentPlan
    {
        Seven = 7,
        Fifteen = 15,
        Thirty = 30,
        FortyFive = 45,
        Fifty = 50
    }
}
