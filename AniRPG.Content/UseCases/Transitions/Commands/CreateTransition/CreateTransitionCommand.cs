using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Transitions.Commands.CreateTransition
{
    public class CreateTransitionCommand : IRequest<Transition>
    {
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
    }
}