using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Domain.Entities;
using AniRPG.Game.Systems.CharacterSystem.Repositories;
using MediatR;

namespace AniRPG.Game.Systems.CharacterSystem.UseCases.Commands.CreateCharacter
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, Character>
    {
        private readonly ICharacterSystemCharacterRepository _characterRepository;

        public CreateCharacterCommandHandler(ICharacterSystemCharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Character> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            if (await _characterRepository.CharacterExistsWithName(request.Name))
                throw new EntityAlreadyExistsException<Character>(nameof(request.Name), request.Name);

            var character = new Character()
            {
                Name = request.Name
            };

            await _characterRepository.AddCharacter(character);

            return character;
        }
    }
}
