using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.UseCases.Models;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Queries.GetAllLocationsPreview
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
