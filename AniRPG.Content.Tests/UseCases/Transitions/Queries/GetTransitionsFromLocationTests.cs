using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Transitions.Queries.GetTransitionsFromLocation;
using AniRPG.Domain.Entities;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Transitions.Queries
{
    public class GetTransitionsFromLocationTests
    {
        [Fact]
        public async Task Ok()
        {
            var transitionsFromLocation1 = new List<Transition>
            {
                new Transition {Id = 1},
                new Transition {Id = 2}
            };
            var transitionRepositoryMock = new Mock<IContentTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.GetTransitionsFromLocation(1))
                .ReturnsAsync(transitionsFromLocation1);

            var handler = new GetTransitionsFromLocationQueryHandler(transitionRepositoryMock.Object);
            var command = new GetTransitionsFromLocationQuery {LocationId = 1};

            var res = await handler.Handle(command, CancellationToken.None);
            
            Assert.NotNull(res);
            Assert.StrictEqual(transitionsFromLocation1, res);
        }
    }
}
