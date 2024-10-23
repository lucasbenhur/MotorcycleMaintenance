namespace EventBus.Messages.Events
{
    public class CreateMotorcycleEvent : BaseIntegrationEvent
    {
        public string Id { get; set; } = null!;
        public int Year { get; set; }
    }
}
