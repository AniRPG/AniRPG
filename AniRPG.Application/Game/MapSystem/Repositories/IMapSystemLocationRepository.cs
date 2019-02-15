using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.Repositories
{
    public interface IMapSystemLocationRepository
    {
        Task AddLocation(Location location);
        Task DeleteLocation(int locationId);
        Task<Location> GetLocation(int locationId);
        Task<IEnumerable<Location>> GetAllLocations();
        Task UpdateLocation(Location location);
    }
}
