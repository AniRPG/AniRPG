using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest
    {
        public string Name { get; set; }
    }
}
