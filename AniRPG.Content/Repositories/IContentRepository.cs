using System.Threading.Tasks;
using AniRPG.Domain.Common;

namespace AniRPG.Content.Repositories
{
    public interface IContentRepository<T> where T : IEntity
    {
        Task AddEntity(T entity);
        Task DeleteEntity(int entityId);
        Task UpdateEntity(T entity);
        Task<T> GetEntity(int entityId);
    }
}