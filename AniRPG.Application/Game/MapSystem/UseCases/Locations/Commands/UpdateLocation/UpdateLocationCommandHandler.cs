using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommandHandler : AsyncRequestHandler<UpdateLocationCommand>
    {
        private readonly IMapSystemLocationRepository _locationRepository;

        public UpdateLocationCommandHandler(IMapSystemLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        protected override async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocation(request.LocationId);

            if (location == null)
                throw new EntityNotFoundException("Location", request.LocationId);

            location.Name = request.Name;
            location.Description = request.Description;

            location.Transitions.Clear();
            foreach (var transition in request.Transitions)
                location.Transitions.Add(transition);
        }
    }
}
