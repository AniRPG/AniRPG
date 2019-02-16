using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateTransition
{
    public class CreateTransitionCommand : IRequest
    {
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
    }
}