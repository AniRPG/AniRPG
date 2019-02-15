using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest
    {
        public string Name { get; set; }
    }
}
