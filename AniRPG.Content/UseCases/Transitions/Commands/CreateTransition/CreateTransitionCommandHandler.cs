using System;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Transitions.Exceptions;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Transitions.Commands.CreateTransition
{
    public class CreateTransitionCommandHandler : IRequestHandler<CreateTransitionCommand, Transition>
    {
        private readonly IContentLocationRepository _locationRepository;
        private readonly IContentTransitionRepository _transitionRepository;

        public CreateTransitionCommandHandler(
            IContentLocationRepository locationRepository,
            IContentTransitionRepository transitionRepository)
        {
            _locationRepository = locationRepository;
            _transitionRepository = transitionRepository;
        }

        public async Task<Transition> Handle(CreateTransitionCommand request, CancellationToken cancellationToken)
        {
            var locationFrom = await _locationRepository.GetEntity(request.FromLocationId);
            if (locationFrom == null)
                throw new EntityNotFoundException<Location>(request.FromLocationId);

            var locationTo = await _locationRepository.GetEntity(request.ToLocationId);
            if (locationTo == null)
                throw new EntityNotFoundException<Location>(request.ToLocationId);

            var transitionExist =
                await _transitionRepository.ExistTransitionBetween(locationFrom.Id, locationTo.Id);
            if (transitionExist)
                throw new TransitionAlreadyExistsException(locationFrom.Id, locationTo.Id);

            var transition = new Transition
            {
                From = locationFrom,
                To = locationTo
            };

            await _transitionRepository.AddEntity(transition);

            return transition;
        }
    }
}