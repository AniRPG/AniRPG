using AniRPG.Game.Systems.CharacterSystem.Repositories;
using AniRPG.Game.Systems.MapSystem.Repositories;

namespace AniRPG.Game.Repositories
{
    public interface ICharacterRepository
        : ICharacterSystemCharacterRepository, IMapSystemCharacterRepository
    {

    }
}
