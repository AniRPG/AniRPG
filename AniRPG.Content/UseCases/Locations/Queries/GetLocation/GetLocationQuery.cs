using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Queries.GetLocation
{
    public class GetLocationQuery : IRequest<Location>
    {
        public int LocationId { get; set; }
    }
}
