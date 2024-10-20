using MediatR;

namespace MotorcycleService.Application.Commands
{
    public class DeleteMotorcycleCommand : IRequest<bool>
    {
        public string Id { get; internal set; }

        public DeleteMotorcycleCommand(
            string id)
        {
            Id = id;
        }
    }
}
