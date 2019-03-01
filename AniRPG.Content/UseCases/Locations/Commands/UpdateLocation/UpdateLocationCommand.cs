using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest<Location>
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
