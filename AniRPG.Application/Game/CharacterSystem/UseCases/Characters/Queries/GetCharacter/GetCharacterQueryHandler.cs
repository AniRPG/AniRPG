﻿using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;
using AniRPG.Application.Game.CharacterSystem.Repositories;
using AniRPG.Application.Game.Common.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Characters.Queries.GetCharacter
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
                throw new EntityNotFoundException("Character", request.Id);

            return character;
        }

    }
}
