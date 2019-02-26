using System;
using System.Collections.Generic;
using System.Text;

namespace AniRPG.Application.Game.MapSystem.UseCases.Exceptions
{
    public class MapSystemException : Exception
    {
        public MapSystemException(string message)
            : base(message)
        {

        }
    }
}
