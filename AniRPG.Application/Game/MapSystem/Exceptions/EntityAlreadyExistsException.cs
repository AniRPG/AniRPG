using System;
using System.Collections.Generic;
using System.Text;

namespace AniRPG.Application.Game.MapSystem.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) is already exists.")
        {

        }
    }
}
