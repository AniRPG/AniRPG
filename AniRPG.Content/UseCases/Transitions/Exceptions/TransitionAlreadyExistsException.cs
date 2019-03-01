using System;

namespace AniRPG.Content.UseCases.Transitions.Exceptions
{
    public class TransitionAlreadyExistsException : Exception
    {
        public int FromLocationId { get; }
        public int ToLocationId { get; }

        public TransitionAlreadyExistsException(int fromLocationId, int toLocationId)
            : base($"Transition between locations ({fromLocationId}) and ({toLocationId}) already exists.")
        {
            FromLocationId = fromLocationId;
            ToLocationId = toLocationId;
        }
    }
}