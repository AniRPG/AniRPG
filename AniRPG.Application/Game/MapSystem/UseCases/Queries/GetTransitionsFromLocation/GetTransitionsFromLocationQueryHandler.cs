using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Queries.GetTransitionsFromLocation
{
    public class GetTransitionsFromLocationQueryHandler: IRequestHandler<GetTransitionsFromLocationQuery, IEnumerable<Transition>>
    {
        private readonly IMapSystemTransitionRepository _transitionRepository;

        public GetTransitionsFromLocationQueryHandler(IMapSystemTransitionRepository transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        public async Task<IEnumerable<Transition>> Handle(GetTransitionsFromLocationQuery request, CancellationToken cancellationToken)
        {
            return await _transitionRepository.GetTransitionsFromLocation(request.LocationId);
        }
    }
}