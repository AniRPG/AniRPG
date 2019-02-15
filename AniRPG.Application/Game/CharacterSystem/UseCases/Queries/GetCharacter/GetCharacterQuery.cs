using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.CharacterSystem.UseCases.Queries.GetCharacter
{
    public class GetCharacterQuery : IRequest<Character>
    {
        public int Id { get; set; }
    }
}
