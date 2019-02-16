using System.Collections.Generic;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.Repositories
{
    public interface IMapSystemTransitionRepository
    {
        Task<int> AddTransition(Transition transition);
        Task DeleteTransition(int transitionId);
        Task<Transition> GetTransition(int transitionId);
        Task<IEnumerable<Transition>> GetTransitionsFromLocation(int locationId);
    }
}