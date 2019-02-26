using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.MoveCharacterByTransition
{
    public class MoveCharacterByTransitionCommand : IRequest
    {
        public int CharacterId { get; set; }
        public int TransitionId { get; set; }
    }
}