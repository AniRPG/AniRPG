using System.Collections.Generic;
using AniRPG.Content.UseCases.Locations.Models;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Queries.GetAllLocationsPreview
{
    public class GetAllLocationsPreviewQuery : IRequest<IEnumerable<LocationPreviewModel>>
    {

    }
}
