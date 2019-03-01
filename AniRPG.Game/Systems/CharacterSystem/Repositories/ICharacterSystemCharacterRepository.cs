using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Game.Systems.CharacterSystem.Repositories
{
    public interface ICharacterSystemCharacterRepository
    {
        Task AddCharacter(Character character);
        Task<Character> GetCharacter(int id);
        Task<bool> CharacterExistsWithName(string name);
    }
}
