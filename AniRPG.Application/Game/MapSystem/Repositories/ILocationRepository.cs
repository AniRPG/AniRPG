using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.Repositories
{
    public interface ILocationRepository
    {
        Task CreateLocation(Location location);
    }
}
