namespace RentService.Application.Entities
{
    public class Motorcycle
    {
        public string Id { get; set; } = null!;
        public int Year { get; set; }
        public string Model { get; set; } = null!;
        public string Plate { get; set; } = null!;
    }
}
