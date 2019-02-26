using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.UseCases.Commands.DeleteTransition;
using AniRPG.Application.Game.MapSystem.UseCases.Queries.GetTransitionsFromLocation;
using AniRPG.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AniRPG.ApplicationTests.Game.MapSystem.UseCases.Queries
{
    [TestClass]
    public class GetTransitionsFromLocationTests
    {
        [TestMethod]
        public async Task Ok()
        {
            var transitionsFromLocation1 = new List<Transition>
            {
                new Transition {Id = 1},
                new Transition {Id = 2}
            };
            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.GetTransitionsFromLocation(1))
                .ReturnsAsync(transitionsFromLocation1);

            var result = await new GetTransitionsFromLocationQueryHandler(transitionRepositoryMock.Object)
                .Handle(new GetTransitionsFromLocationQuery {LocationId = 1}, CancellationToken.None);

            Assert.AreEqual(result, transitionsFromLocation1);
        }
    }
}
