using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.Repositories
{
    public interface IMapSystemCharacterRepository
    {
        Task<Character> GetCharacter(int characterId);
        Task UpdateCharacter(Character character);
    }
}