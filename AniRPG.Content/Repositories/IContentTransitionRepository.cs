using System.Collections.Generic;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Content.Repositories
{
    public interface IContentTransitionRepository : IContentRepository<Transition>
    {
        Task<IEnumerable<Transition>> GetTransitionsFromLocation(int locationId);
        Task<bool> ExistTransitionBetween(int locationFromId, int locationToId);
    }
}