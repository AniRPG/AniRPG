using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.DeleteTransition
{
    public class DeleteTransitionCommand : IRequest
    {
        public int TransitionId { get; set; }
    }
}