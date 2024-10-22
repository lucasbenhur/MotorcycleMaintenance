namespace RentService.Application.Entities
{
    public class DeliveryMan
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; } = null!;
        public string CnhType { get; set; } = null!;
        public string CnhImage { get; set; } = null!;
    }
}
