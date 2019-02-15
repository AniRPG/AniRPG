using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.Exceptions;

namespace AniRPG.Application.Game.MapSystem.UseCases.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandHandler : AsyncRequestHandler<CreateCharacterCommand>
    {
        private readonly IMapSystemCharacterRepository _characterRepository;

        public CreateCharacterCommandHandler(IMapSystemCharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        protected override async Task Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            if (await _characterRepository.CharacterExistWithName(request.Name))
                throw new EntityAlreadyExistsException("Character", request.Name);

            await _characterRepository.CreateCharacter(request.Name);
        }
    }
}
