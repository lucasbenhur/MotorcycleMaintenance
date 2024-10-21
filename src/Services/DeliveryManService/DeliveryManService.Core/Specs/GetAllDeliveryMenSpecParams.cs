namespace DeliveryManService.Core.Specs
{
    public class GetAllDeliveryMenSpecParams
    {
        public GetAllDeliveryMenSpecParams()
        {
            Id = null;
            Cnpj = null;
            CnhNumber = null;
        }

        public GetAllDeliveryMenSpecParams(
            string? id = null,
            string? cnpj = null,
            string? cnhNumber = null)
        {
            Id = id;
            Cnpj = cnpj;
            CnhNumber = cnhNumber;
        }

        public string? Id { get; set; }
        public string? Cnpj { get; set; }
        public string? CnhNumber { get; set; }
    }
}
