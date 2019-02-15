using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Characters.Queries.GetCharacter
{
    public class GetCharacterQuery : IRequest<Character>
    {
        public int Id { get; set; }
    }
}
