using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Application.Game.MapSystem.UseCases.Locations.Models;
using AniRPG.Application.Game.MapSystem.Repositories;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Queries.GetAllLocationsPreview
{
    class GetAllLocationsPreviewQueryHandler : IRequestHandler<GetAllLocationsPreviewQuery, IEnumerable<LocationPreviewModel>>
    {
        private readonly IMapSystemLocationRepository _locationRepository;

        public GetAllLocationsPreviewQueryHandler(IMapSystemLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<LocationPreviewModel>> Handle(GetAllLocationsPreviewQuery request, 
            CancellationToken cancellationToken)
        {
            var locationsPreviewList = (await _locationRepository.GetAllLocations()).Select(
                x => new LocationPreviewModel
                {
                    Id = x.Id,
                    Name = x.Name
                });

            return locationsPreviewList;
        }
    }
}
