using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Domain.Entities;
using AniRPG.Game.Systems.CharacterSystem.Repositories;
using MediatR;

namespace AniRPG.Game.Systems.CharacterSystem.UseCases.Queries.GetCharacter
{
    public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, Character>
    {
        private readonly ICharacterSystemCharacterRepository _characterRepository;

        public GetCharacterQueryHandler(ICharacterSystemCharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Character> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetCharacter(request.Id);

            if (character == null)
                throw new EntityNotFoundException<Character>(request.Id);

            return character;
        }

    }
}
