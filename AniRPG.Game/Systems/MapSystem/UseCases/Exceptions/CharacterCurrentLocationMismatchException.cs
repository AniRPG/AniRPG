namespace AniRPG.Game.Systems.MapSystem.UseCases.Exceptions
{
    public class CharacterCurrentLocationMismatchException : MapSystemException
    {
        public CharacterCurrentLocationMismatchException(int characterId, int transitionId)
            : base($"Character {characterId} current location does not equal to transition {transitionId} from value.")
        {

        }
    }
}
