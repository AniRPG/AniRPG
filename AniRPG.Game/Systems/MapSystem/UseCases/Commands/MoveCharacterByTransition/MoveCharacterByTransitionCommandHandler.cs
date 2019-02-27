using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Domain.Entities;
using AniRPG.Game.Systems.MapSystem.Repositories;
using AniRPG.Game.Systems.MapSystem.UseCases.Exceptions;
using MediatR;

namespace AniRPG.Game.Systems.MapSystem.UseCases.Commands.MoveCharacterByTransition
{
    public class MoveCharacterByTransitionCommandHandler : IRequestHandler<MoveCharacterByTransitionCommand, bool>
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

        public async Task<bool> Handle(MoveCharacterByTransitionCommand request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetCharacter(request.CharacterId);
            if (character == null)
                throw new EntityNotFoundException<Character>(request.CharacterId);

            var transition = await _transitionRepository.GetTransition(request.TransitionId);
            if (transition == null)
                throw new EntityNotFoundException<Transition>(request.TransitionId);

            if (character.CurrentLocation != transition.From)
                throw new CharacterCurrentLocationMismatchException(request.CharacterId, request.TransitionId);

            character.CurrentLocation = transition.To;
            await _characterRepository.UpdateCharacter(character);

            return true;
        }
    }
}