using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Content.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Queries.GetLocation
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Location>
    {
        private readonly IContentLocationRepository _locationRepository;

        public GetLocationQueryHandler(IContentLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetEntity(request.LocationId);

            if (location == null)
                throw new EntityNotFoundException<Location>(request.LocationId);

            return location;
        }
    }
}
