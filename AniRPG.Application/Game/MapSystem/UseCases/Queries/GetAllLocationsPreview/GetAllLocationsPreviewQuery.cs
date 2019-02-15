using System.Collections.Generic;
using AniRPG.Application.Game.MapSystem.UseCases.Models;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Queries.GetAllLocationsPreview
{
    public class GetAllLocationsPreviewQuery : IRequest<IEnumerable<LocationPreviewModel>>
    {

    }
}
