using DeliveryManService.Application.Commands;
using DeliveryManService.Application.Queries;
using DeliveryManService.Core.Repositories;
using DeliveryManService.Core.Specs;
using MediatR;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace DeliveryManService.Application.Handlers
{
    public class UpdateDeliveryManCommandHandler : IRequestHandler<UpdateDeliveryManCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public UpdateDeliveryManCommandHandler(
            IMediator mediator,
            IDeliveryManRepository deliveryManRepository,
            IAppLogger logger,
            IServiceContext serviceContext)
        {
            _mediator = mediator;
            _deliveryManRepository = deliveryManRepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<bool> Handle(UpdateDeliveryManCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return false;

                var relativePath = GetChnImageRelativePath(request);

                if (!await UpdateCnhImageToLocalStorageAsync(request.CnhImage, relativePath))
                    return false;

                var deliveryMan = await _deliveryManRepository.GetAsync(request.Id);
                deliveryMan.UpdateCnhImage(relativePath);
                await _deliveryManRepository.UpdateAsync(deliveryMan);
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao atualizar o entregador Id {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(UpdateDeliveryManCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                _serviceContext.AddNotification("O campo id é obrigatório");
            else if (!await ExistsIdAsync(request.Id))
                _serviceContext.AddNotification($"O entregador id {request.Id} não existe");

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

        private string GetChnImageRelativePath(UpdateDeliveryManCommand request)
        {
            var extension = GetCnhImageExtension(request.CnhImage);
            return $"cnh_image/{request.Id}/cnh{extension}";
        }

        private async Task<bool> UpdateCnhImageToLocalStorageAsync(string cnhImage, string relativePath)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(basePath, relativePath);
                var directory = Path.GetDirectoryName(filePath);

                if (Directory.Exists(directory))
                    Directory.Delete(directory, true);

                Directory.CreateDirectory(directory);

                byte[] imageBytes = Convert.FromBase64String(cnhImage);
                await File.WriteAllBytesAsync(filePath, imageBytes);
                _logger.LogInformation($"Imagem CNH atualizada em {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao atualizar a imagem CNH");
                return false;
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
            return (await _mediator.Send(query)).Any();
        }

        private async Task<bool> ExistsCnhNumberAsync(string cnhNumber)
        {
            var specParams = new GetAllDeliveryMenSpecParams(cnhNumber: cnhNumber);
            var query = new GetAllDeliveryMenQuery(specParams);
            return (await _mediator.Send(query)).Any();
        }

        private async Task<bool> ExistsCnpjAsync(string cnpj)
        {
            var specParams = new GetAllDeliveryMenSpecParams(cnpj: cnpj);
            var query = new GetAllDeliveryMenQuery(specParams);
            return (await _mediator.Send(query)).Any();
        }
    }
}
