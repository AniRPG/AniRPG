using System;
using System.Linq.Expressions;

namespace AniRPG.Common.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public string ConflictingPropertyName { get; }
        public object ConflictingPropertyValue { get; }

        public EntityAlreadyExistsException(
            string entityName,
            string conflictingPropertyName,
            object conflictingPropertyValue)
            : base($"Entity {entityName} with {conflictingPropertyName} \"{conflictingPropertyValue}\" already exists.")
        {
            ConflictingPropertyName = conflictingPropertyName;
            ConflictingPropertyValue = conflictingPropertyValue;
        }
    }

    public class EntityAlreadyExistsException<T> : EntityAlreadyExistsException
    {
        public EntityAlreadyExistsException(string conflictingPropertyName, object conflictingPropertyValue)
            : base(nameof(T), conflictingPropertyName, conflictingPropertyValue)
        { }
    }
}