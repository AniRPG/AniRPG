using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.Repositories
{
    public interface IMapSystemCharacterRepository
    {
        Task CreateCharacter(string name);
        Task<Character> GetCharacter(int id);
        Task<bool> CharacterExistWithName(string name);
    }
}
