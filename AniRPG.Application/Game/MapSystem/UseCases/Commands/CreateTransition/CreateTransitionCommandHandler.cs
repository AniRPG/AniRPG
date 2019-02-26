using System;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateTransition
{
    public class CreateTransitionCommandHandler : IRequestHandler<CreateTransitionCommand, Transition>
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

        public async Task<Transition> Handle(CreateTransitionCommand request, CancellationToken cancellationToken)
        {
            var locationFrom = await _locationRepository.GetLocation(request.FromLocationId);
            if (locationFrom == null)
                throw new EntityNotFoundException("Location", request.FromLocationId);

            var locationTo = await _locationRepository.GetLocation(request.ToLocationId);
            if (locationTo == null)
                throw new EntityNotFoundException("Location", request.ToLocationId);

            var transitionExist =
                await _transitionRepository.ExistTransitionBetween(locationFrom.Id, locationTo.Id);
            if (transitionExist)
                throw new ApplicationException("Such transition already exist");

            var transition = new Transition
            {
                From = locationFrom,
                To = locationTo
            };

            await _transitionRepository.AddTransition(transition);

            return transition;
        }
    }
}