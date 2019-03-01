using System.Collections.Generic;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Content.Repositories
{
    public interface IContentLocationRepository : IContentRepository<Location>
    {
        Task<IEnumerable<Location>> GetAllLocations();
    }
}