using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace AniRPG.Application.Game.MapSystem.UseCases.Characters.Queries.GetCharacter
{
    public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, Character>
    {
        private readonly IMapSystemCharacterRepository _characterRepository;

        public GetCharacterQueryHandler(IMapSystemCharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Character> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetCharacter(request.Id);

            if (character == null)
                throw new EntityNotFoundException("Character", request.Id);

            return character;
        }

    }
}
