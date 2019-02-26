using MediatR;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest
    {
        public string Name { get; set; }
    }
}
