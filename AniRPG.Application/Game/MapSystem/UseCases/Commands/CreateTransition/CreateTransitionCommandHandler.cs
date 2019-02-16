using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateTransition
{
    public class CreateTransitionCommandHandler : AsyncRequestHandler<CreateTransitionCommand>
    {
        private readonly IMapSystemLocationRepository _locationRepository;
        private readonly IMapSystemTransitionRepository _transitionRepository;

        public CreateTransitionCommandHandler(
            IMapSystemLocationRepository locationRepository,
            IMapSystemTransitionRepository transitionRepository)
        {
            _locationRepository = locationRepository;
            _transitionRepository = transitionRepository;
        }

        protected override async Task Handle(CreateTransitionCommand request, CancellationToken cancellationToken)
        {
            var locationFrom = await _locationRepository.GetLocation(request.FromLocationId);
            if (locationFrom == null)
                throw new EntityNotFoundException("Location", request.FromLocationId);

            var locationTo = await _locationRepository.GetLocation(request.ToLocationId);
            if (locationTo == null)
                throw new EntityNotFoundException("Location", request.ToLocationId);

            var transition = new Transition
            {
                From = locationFrom,
                To = locationTo
            };

            await _transitionRepository.AddTransition(transition);
        }
    }
}