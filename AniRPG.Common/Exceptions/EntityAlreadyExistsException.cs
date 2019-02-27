using System;

namespace AniRPG.Common.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) is already exists.")
        {

        }
    }
}