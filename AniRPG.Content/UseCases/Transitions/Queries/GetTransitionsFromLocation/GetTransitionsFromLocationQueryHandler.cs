using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Domain.Entities;
using MediatR;

namespace AniRPG.Content.UseCases.Transitions.Queries.GetTransitionsFromLocation
{
    public class GetTransitionsFromLocationQueryHandler: IRequestHandler<GetTransitionsFromLocationQuery, IEnumerable<Transition>>
    {
        private readonly IContentTransitionRepository _transitionRepository;

        public GetTransitionsFromLocationQueryHandler(IContentTransitionRepository transitionRepository)
        {
            _transitionRepository = transitionRepository;
        }

        public async Task<IEnumerable<Transition>> Handle(GetTransitionsFromLocationQuery request, CancellationToken cancellationToken)
        {
            return await _transitionRepository.GetTransitionsFromLocation(request.LocationId);
        }
    }
}