using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Queries.GetLocation
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
