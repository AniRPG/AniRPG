using System;
using AniRPG.Domain.Common;

namespace AniRPG.Common.Exceptions
{
    public abstract class EntityNotFoundException : Exception
    {
        public int EntityId { get; }

        protected EntityNotFoundException(string name, int id)
            : base($"Entity {name} ({id}) was not found.")
        {
            EntityId = id;
        }
    }

    public class EntityNotFoundException<T> : EntityNotFoundException
        where T : IEntity
    {
        public EntityNotFoundException(int id)
            : base(nameof(T), id)
        {

        }
    }
}
