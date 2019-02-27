using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;

namespace AniRPG.Content.UseCases.Transitions.Commands.DeleteTransition
{
    public class DeleteTransitionCommandHandler : IRequestHandler<DeleteTransitionCommand, bool>
    {
        private readonly IContentTransitionRepository _transitionRepository;

        public DeleteTransitionCommandHandler(IContentTransitionRepository transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        public async Task<bool> Handle(DeleteTransitionCommand request, CancellationToken cancellationToken)
        {
            await _transitionRepository.DeleteEntity(request.TransitionId);
            return true;
        }
    }
}