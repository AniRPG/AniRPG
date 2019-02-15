using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Queries.GetLocation
{
    public class GetLocationQuery : IRequest<Location>
    {
        public int LocationId { get; set; }
    }
}
