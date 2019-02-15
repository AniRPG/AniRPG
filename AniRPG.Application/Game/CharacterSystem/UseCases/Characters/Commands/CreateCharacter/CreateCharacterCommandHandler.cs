using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.CharacterSystem.Repositories;
using AniRPG.Application.Game.Common.Exceptions;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandHandler : AsyncRequestHandler<CreateCharacterCommand>
    {
        private readonly ICharacterSystemCharacterRepository _characterRepository;

        public CreateCharacterCommandHandler(ICharacterSystemCharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        protected override async Task Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            if (await _characterRepository.CharacterExistsWithName(request.Name))
                throw new EntityAlreadyExistsException("Character", request.Name);

            var character = new Domain.Entities.Character()
            {
                Name = request.Name
            };
            await _characterRepository.AddCharacter(character);
        }
    }
}
