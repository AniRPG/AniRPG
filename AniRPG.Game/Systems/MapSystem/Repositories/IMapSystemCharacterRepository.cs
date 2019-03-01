using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Game.Systems.MapSystem.Repositories
{
    public interface IMapSystemCharacterRepository
    {
        Task<Character> GetCharacter(int characterId);
        Task UpdateCharacter(Character character);
    }
}