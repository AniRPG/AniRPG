using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Commands.CreateLocation
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
    {
        private readonly IContentLocationRepository _locationRepository;

        public CreateLocationCommandHandler(IContentLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Location()
            {
                Name = request.Name
            };
            await _locationRepository.AddEntity(location);

            return location;
        }
    }
}
