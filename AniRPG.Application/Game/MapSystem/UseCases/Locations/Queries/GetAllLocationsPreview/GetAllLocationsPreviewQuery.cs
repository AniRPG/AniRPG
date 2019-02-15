using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Application.Game.MapSystem.UseCases.Locations.Models;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Queries.GetAllLocationsPreview
{
    public class GetAllLocationsPreviewQuery : IRequest<IEnumerable<LocationPreviewModel>>
    {

    }
}
