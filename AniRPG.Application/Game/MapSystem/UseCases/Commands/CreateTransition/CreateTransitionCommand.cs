using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateTransition
{
    public class CreateTransitionCommand : IRequest<Transition>
    {
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
    }
}