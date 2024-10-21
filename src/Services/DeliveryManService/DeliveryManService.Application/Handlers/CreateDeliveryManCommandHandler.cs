using DeliveryManService.Application.Commands;
using DeliveryManService.Application.Mappers;
using DeliveryManService.Application.Queries;
using DeliveryManService.Application.Responses;
using DeliveryManService.Core.Entities;
using DeliveryManService.Core.Repositories;
using DeliveryManService.Core.Specs;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Extensions;
using Shared.ServiceContext;

namespace DeliveryManService.Application.Handlers
{
    public class CreateDeliveryManCommandHandler : IRequestHandler<CreateDeliveryManCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly ILogger<CreateDeliveryManCommandHandler> _logger;
        private readonly IServiceContext _serviceContext;

        public CreateDeliveryManCommandHandler(
            IMediator mediator,
            IDeliveryManRepository deliveryManRepository,
            ILogger<CreateDeliveryManCommandHandler> logger,
            IServiceContext serviceContext)
        {
            _mediator = mediator;
            _deliveryManRepository = deliveryManRepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<bool> Handle(CreateDeliveryManCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return false;

                var cnhImage = request.CnhImage;
                var relativePath = GetChnImageRelativePath(request);
                request.CnhImage = relativePath;
                var deliveryManEntity = DeliveryManMapper.Mapper.Map<DeliveryMan>(request);
                await _deliveryManRepository.CreateAsync(deliveryManEntity);
                _logger.LogInformation("Entregador cadastrado com Id {Id}", request.Id);
                await SaveCnhImageToLocalStorageAsync(cnhImage, relativePath);
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao cadastrar o entregador Id {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(CreateDeliveryManCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                _serviceContext.AddNotification("O campo identificador é obrigatório");
            else if (await ExistsIdAsync(request.Id))
                _serviceContext.AddNotification($"O identificador {request.Id} já existe");

            if (string.IsNullOrWhiteSpace(request.Name))
                _serviceContext.AddNotification("O campo nome é obrigatório");

            if (string.IsNullOrWhiteSpace(request.Cnpj))
                _serviceContext.AddNotification("O campo cnpj é obrigatório");
            else if (!request.Cnpj.All(char.IsDigit))
                _serviceContext.AddNotification("O campo cnpj deve conter apenas números");
            else if (!request.Cnpj.IsValidCnpj())
                _serviceContext.AddNotification("O campo cnpj é inválido");
            else if (await ExistsCnpjAsync(request.Cnpj))
                _serviceContext.AddNotification($"O cnpj {request.Cnpj} já existe");

            if (request.BirthDate is null)
                _serviceContext.AddNotification("O campo data_nascimento é obrigatório");

            if (string.IsNullOrWhiteSpace(request.CnhNumber))
                _serviceContext.AddNotification("O campo numero_cnh é obrigatório");
            else if (!request.CnhNumber.All(char.IsDigit))
                _serviceContext.AddNotification("O campo numero_cnh deve conter apenas números");
            else if (await ExistsCnhNumberAsync(request.CnhNumber))
                _serviceContext.AddNotification($"O numero_cnh {request.CnhNumber} já existe");

            if (string.IsNullOrWhiteSpace(request.CnhType))
                _serviceContext.AddNotification("O campo tipo_cnh é obrigatório");
            else if (request.CnhType.ToUpper() != "A" && request.CnhType.ToUpper() != "B" && request.CnhType.ToUpper() != "A+B")
                _serviceContext.AddNotification("O campo tipo_cnh é inválido. Os valores permitidos são A, B, A+B");

            if (string.IsNullOrEmpty(request.CnhImage))
                _serviceContext.AddNotification("O campo imagem_cnh é obrigatório");
            else if (!IsValidCnhImageExtension(request.CnhImage))
                _serviceContext.AddNotification("Extensão do arquivo no campo imagem_cnh é inválido. São permitidos apenas .png e .bmp");

            return !_serviceContext.HasNotification();
        }

        private bool IsValidCnhImageExtension(string cnhImage)
        {
            var extension = GetCnhImageExtension(cnhImage);
            return extension == ".png" || extension == ".bmp";
        }

        private string GetChnImageRelativePath(CreateDeliveryManCommand request)
        {
            var extension = GetCnhImageExtension(request.CnhImage);
            return $"cnh_image/{request.Id}{extension}";
        }

        private async Task SaveCnhImageToLocalStorageAsync(string cnhImage, string relativePath)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, relativePath);
                var directory = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                byte[] imageBytes = Convert.FromBase64String(cnhImage);
                await File.WriteAllBytesAsync(filePath, imageBytes);
                _logger.LogInformation("Imagem CNH armazenada em {filePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao armazenar a imagem CNH");
            }
        }

        private string GetCnhImageExtension(string cnhImage)
        {
            if (string.IsNullOrWhiteSpace(cnhImage))
                return null;

            if (cnhImage.StartsWith("data:image/png;base64,") ||
                cnhImage.StartsWith("iVBORw0KGgo"))
                return ".png";

            if (cnhImage.StartsWith("data:image/bmp;base64,") ||
                cnhImage.StartsWith("Qk"))
                return ".bmp";

            return null;
        }

        private async Task<bool> ExistsIdAsync(string id)
        {
            var specParams = new GetAllDeliveryMenSpecParams(id: id);
            var query = new GetAllDeliveryMenQuery(specParams);
            return (await _mediator.Send<ICollection<DeliveryManResponse>>(query)).Any();
        }

        private async Task<bool> ExistsCnhNumberAsync(string cnhNumber)
        {
            var specParams = new GetAllDeliveryMenSpecParams(cnhNumber: cnhNumber);
            var query = new GetAllDeliveryMenQuery(specParams);
            return (await _mediator.Send<ICollection<DeliveryManResponse>>(query)).Any();
        }

        private async Task<bool> ExistsCnpjAsync(string cnpj)
        {
            var specParams = new GetAllDeliveryMenSpecParams(cnpj: cnpj);
            var query = new GetAllDeliveryMenQuery(specParams);
            return (await _mediator.Send<ICollection<DeliveryManResponse>>(query)).Any();
        }
    }
}
