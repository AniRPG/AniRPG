using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Queries.GetLocation
{
    public class GetLocationQuery : IRequest<Location>
    {
        public int LocationId { get; set; }
    }
}
