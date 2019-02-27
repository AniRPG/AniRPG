using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Game.Systems.CharacterSystem.UseCases.Queries.GetCharacter
{
    public class GetCharacterQuery : IRequest<Character>
    {
        public int Id { get; set; }
    }
}
