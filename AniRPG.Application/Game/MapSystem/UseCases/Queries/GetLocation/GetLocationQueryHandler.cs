using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Queries.GetLocation
{
    public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Location>
    {
        private readonly IMapSystemLocationRepository _locationRepository;

        public GetLocationQueryHandler(IMapSystemLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> Handle(GetLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocation(request.LocationId);

            if (location == null)
                throw new EntityNotFoundException("Location", request.LocationId);

            return location;
        }
    }
}
