using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<Location>
    {
        public string Name { get; set; }
    }
}
