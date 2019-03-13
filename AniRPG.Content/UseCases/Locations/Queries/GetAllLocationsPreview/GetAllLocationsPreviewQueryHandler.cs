using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Locations.Models;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Queries.GetAllLocationsPreview
{
    public class GetAllLocationsPreviewQueryHandler : IRequestHandler<GetAllLocationsPreviewQuery, IEnumerable<LocationPreviewModel>>
    {
        private readonly IContentLocationRepository _locationRepository;

        public GetAllLocationsPreviewQueryHandler(IContentLocationRepository locationRepository)
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
