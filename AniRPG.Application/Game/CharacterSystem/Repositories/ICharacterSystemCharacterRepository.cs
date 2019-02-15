using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.CharacterSystem.Repositories
{
    public interface ICharacterSystemCharacterRepository
    {
        Task AddCharacter(Character character);
        Task<Character> GetCharacter(int id);
        Task<bool> CharacterExistsWithName(string name);
    }
}
