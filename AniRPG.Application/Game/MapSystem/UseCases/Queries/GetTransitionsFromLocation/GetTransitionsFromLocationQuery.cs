using System.Collections.Generic;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Queries.GetTransitionsFromLocation
{
    public class GetTransitionsFromLocationQuery : IRequest<IEnumerable<Transition>>
    {
        public int LocationId { get; set; }
    }
}