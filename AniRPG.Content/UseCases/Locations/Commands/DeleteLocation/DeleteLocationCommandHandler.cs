using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using MediatR;

namespace AniRPG.Content.UseCases.Locations.Commands.DeleteLocation
{
    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly IContentLocationRepository _locationRepository;

        public DeleteLocationCommandHandler(IContentLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            await _locationRepository.DeleteEntity(request.LocationId);
            return true;
        }
    }
}
