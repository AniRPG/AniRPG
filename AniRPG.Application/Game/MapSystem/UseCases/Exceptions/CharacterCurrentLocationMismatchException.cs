using System;
using System.Collections.Generic;
using System.Text;

namespace AniRPG.Application.Game.MapSystem.UseCases.Exceptions
{
    public class CharacterCurrentLocationMismatchException : MapSystemException
    {
        public CharacterCurrentLocationMismatchException(int characterId, int transitionId)
            : base($"Character {characterId} current location does not equal to transition {transitionId} from value.")
        {

        }
    }
}
