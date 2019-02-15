using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.DeleteLocation
{
    public class DeleteLocationCommandHandler : AsyncRequestHandler<DeleteLocationCommand>
    {
        private readonly IMapSystemLocationRepository _locationRepository;

        public DeleteLocationCommandHandler(IMapSystemLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        protected override async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            await _locationRepository.DeleteLocation(request.LocationId);
        }
    }
}
