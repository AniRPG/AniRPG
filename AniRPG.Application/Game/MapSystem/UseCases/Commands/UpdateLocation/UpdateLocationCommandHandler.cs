using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.UpdateLocation
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

            await _locationRepository.UpdateLocation(location);
        }
    }
}
