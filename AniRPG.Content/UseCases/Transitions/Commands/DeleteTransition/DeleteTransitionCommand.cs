using MediatR;

namespace AniRPG.Content.UseCases.Transitions.Commands.DeleteTransition
{
    public class DeleteTransitionCommand : IRequest<bool>
    {
        public int TransitionId { get; set; }
    }
}