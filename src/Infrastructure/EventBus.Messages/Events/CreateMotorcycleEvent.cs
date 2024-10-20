namespace EventBus.Messages.Events
{
    public class CreateMotorcycleEvent : BaseIntegrationEvent
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
