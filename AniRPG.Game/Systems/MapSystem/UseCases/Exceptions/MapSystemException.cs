using System;

namespace AniRPG.Game.Systems.MapSystem.UseCases.Exceptions
{
    public class MapSystemException : Exception
    {
        public MapSystemException(string message)
            : base(message)
        {

        }
    }
}
