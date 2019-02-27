using MediatR;

namespace AniRPG.Game.Systems.MapSystem.UseCases.Commands.MoveCharacterByTransition
{
    public class MoveCharacterByTransitionCommand : IRequest<bool>
    {
        public int CharacterId { get; set; }
        public int TransitionId { get; set; }
    }
}