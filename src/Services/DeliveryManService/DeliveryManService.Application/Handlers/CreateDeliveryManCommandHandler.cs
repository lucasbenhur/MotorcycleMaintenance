using DeliveryManService.Application.Commands;
using DeliveryManService.Application.Mappers;
using DeliveryManService.Core.Entities;
using DeliveryManService.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
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
                var msg = $"Ocorreu um erro ao cadastrar o entregador Id {request.Id}. Detalhes: {ex.Message}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(CreateDeliveryManCommand request)
        {
            if (string.IsNullOrEmpty(request.CnhImage))
                _serviceContext.AddNotification("Informe a imagem da CNH.");
            else if (!IsValidCnhImageExtension(request.CnhImage))
                _serviceContext.AddNotification("Extensão da imagem da CNH é inválida. São permitidas apenas .png e .bmp");

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
                _logger.LogError(ex, "Ocorreu um erro ao armazenar a imagem CNH. Detalhes: {Message}", ex.Message);
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
    }
}
