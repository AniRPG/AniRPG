using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateLocation
{
    public class CreateLocationCommandHandler : AsyncRequestHandler<CreateLocationCommand>
    {
        private readonly IMapSystemLocationRepository _locationRepository;

        public CreateLocationCommandHandler(IMapSystemLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        protected override async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Domain.Entities.Location()
            {
                Name = request.Name
            };
            await _locationRepository.AddLocation(location);
        }
    }
}
