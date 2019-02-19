using System;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.UseCases.Commands.CreateTransition;
using AniRPG.Application.Repositories;
using AniRPG.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AniRPG.ApplicationTests.Game.MapSystem.UseCases.Commands
{
    [TestClass]
    public class CreateTransitionTests
    {
        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public async Task FromDoesNotExist_Exception()
        {
            Location locationFrom = null;
            var locationTo = new Location {Id = 2};
            Transition transition = null;

            var locationRepositoryMock = new Mock<IMapSystemLocationRepository>();
            locationRepositoryMock.Setup(a => a.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((int x) => x == 1 ? locationFrom : locationTo);

            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock.Setup(a => a.AddTransition(It.IsAny<Transition>()))
                .Callback((Transition t) =>
                {
                    transition = t;
                });
            transitionRepositoryMock.Setup(a => a.ExistTransitionBetween(1, 2)).ReturnsAsync(false);

            var result =
                await new CreateTransitionCommandHandler(locationRepositoryMock.Object,
                        transitionRepositoryMock.Object)
                    .Handle(new CreateTransitionCommand {FromLocationId = 1, ToLocationId = 2},
                        CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public async Task ToDoesNotExist_Exception()
        {
            var locationFrom = new Location { Id = 1 };
            Location locationTo = null;
            Transition transition = null;

            var locationRepositoryMock = new Mock<IMapSystemLocationRepository>();
            locationRepositoryMock.Setup(a => a.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((int x) => x == 1 ? locationFrom : locationTo);

            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock.Setup(a => a.AddTransition(It.IsAny<Transition>()))
                .Callback((Transition t) =>
                {
                    transition = t;
                });
            transitionRepositoryMock.Setup(a => a.ExistTransitionBetween(1, 2)).ReturnsAsync(false);

            var result =
                await new CreateTransitionCommandHandler(locationRepositoryMock.Object,
                        transitionRepositoryMock.Object)
                    .Handle(new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 2 },
                        CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public async Task TransitionAlreadyExistExist_Exception()
        {
            var locationFrom = new Location { Id = 1 };
            var locationTo = new Location { Id = 2 };
            Transition transition = null;

            var locationRepositoryMock = new Mock<IMapSystemLocationRepository>();
            locationRepositoryMock.Setup(a => a.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((int x) => x == 1 ? locationFrom : locationTo);

            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock.Setup(a => a.AddTransition(It.IsAny<Transition>()))
                .Callback((Transition t) =>
                {
                    transition = t;
                });
            transitionRepositoryMock.Setup(a => a.ExistTransitionBetween(1, 2)).ReturnsAsync(true);

            var result =
                await new CreateTransitionCommandHandler(locationRepositoryMock.Object,
                        transitionRepositoryMock.Object)
                    .Handle(new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 2 },
                        CancellationToken.None);
        }

        [TestMethod]
        public async Task Ok()
        {
            var locationFrom = new Location { Id = 1 };
            var locationTo = new Location { Id = 2 };
            Transition transition = null;

            var locationRepositoryMock = new Mock<IMapSystemLocationRepository>();
            locationRepositoryMock.Setup(a => a.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((int x) => x == 1 ? locationFrom : locationTo);

            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock.Setup(a => a.AddTransition(It.IsAny<Transition>()))
                .Returns((Transition t) =>
                {
                    transition = t;
                    return Task.FromResult(0);
                });
            transitionRepositoryMock.Setup(a => a.ExistTransitionBetween(1, 2)).ReturnsAsync(false);

            var result =
                await new CreateTransitionCommandHandler(locationRepositoryMock.Object,
                        transitionRepositoryMock.Object)
                    .Handle(new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 2 },
                        CancellationToken.None);

            Assert.IsNotNull(transition);
            Assert.AreEqual(transition.From, locationFrom);
            Assert.AreEqual(transition.To, locationTo);
        }
    }
}
