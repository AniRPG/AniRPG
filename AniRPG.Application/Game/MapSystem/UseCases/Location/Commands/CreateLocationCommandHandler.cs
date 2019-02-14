using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;
using AniRPG.Application.Game.MapSystem.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace AniRPG.Application.Game.MapSystem.UseCases.Location.Commands
{
    public class CreateLocationCommandHandler : AsyncRequestHandler<CreateLocationCommand>
    {
        private readonly IMapSystemLocationRepository _locationRepository;

        public CreateLocationCommandHandler(IMapSystemLocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        protected override async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Domain.Entities.Location()
            {
                Name = request.Name
            };
            await _locationRepository.CreateLocation(location);
        }
    }
}
