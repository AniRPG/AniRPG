using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Content.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Location>
    {
        private readonly IContentLocationRepository _locationRepository;

        public UpdateLocationCommandHandler(IContentLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetEntity(request.LocationId);

            if (location == null)
                throw new EntityNotFoundException<Location>(request.LocationId);

            location.Name = request.Name;
            location.Description = request.Description;

            await _locationRepository.UpdateEntity(location);

            return location;
        }
    }
}
