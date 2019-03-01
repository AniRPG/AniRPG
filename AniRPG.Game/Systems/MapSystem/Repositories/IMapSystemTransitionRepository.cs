using System.Collections.Generic;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Game.Systems.MapSystem.Repositories
{
    public interface IMapSystemTransitionRepository
    {
        Task<Transition> GetTransition(int transitionId);
    }
}