using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Game.Systems.CharacterSystem.UseCases.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest<Character>
    {
        public string Name { get; set; }
    }
}
