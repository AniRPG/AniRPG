using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.DeleteTransition
{
    public class DeleteTransitionCommandHandler : IRequestHandler<DeleteTransitionCommand, Unit>
    {
        private readonly IMapSystemTransitionRepository _transitionRepository;

        public DeleteTransitionCommandHandler(IMapSystemTransitionRepository transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        public async Task<Unit> Handle(DeleteTransitionCommand request, CancellationToken cancellationToken)
        {
            await _transitionRepository.DeleteTransition(request.TransitionId);
            return Unit.Value;
        }
    }
}