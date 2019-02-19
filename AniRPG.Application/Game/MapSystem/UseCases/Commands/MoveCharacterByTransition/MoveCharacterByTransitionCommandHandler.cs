using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.UseCases.Exceptions;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.MoveCharacterByTransition
{
    public class MoveCharacterByTransitionCommandHandler : IRequestHandler<MoveCharacterByTransitionCommand, Unit>
    {
        private readonly IMapSystemTransitionRepository _transitionRepository;
        private readonly IMapSystemCharacterRepository _characterRepository;

        public MoveCharacterByTransitionCommandHandler(
            IMapSystemTransitionRepository transitionRepository,
            IMapSystemCharacterRepository characterRepository)
        {
            _transitionRepository = transitionRepository;
            _characterRepository = characterRepository;
        }

        public async Task<Unit> Handle(MoveCharacterByTransitionCommand request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetCharacter(request.CharacterId);
            if (character == null)
                throw new EntityNotFoundException("Character", request.CharacterId);

            var transition = await _transitionRepository.GetTransition(request.TransitionId);
            if (transition == null)
                throw new EntityNotFoundException("Transition", request.TransitionId);

            if (character.CurrentLocation != transition.From)
                throw new CharacterCurrentLocationMismatchException(request.CharacterId, request.TransitionId);

            character.CurrentLocation = transition.To;
            await _characterRepository.UpdateCharacter(character);

            return Unit.Value;
        }
    }
}