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
        private IMapSystemLocationRepository _locationRepository;
        private IMapSystemTransitionRepository _transitionRepository;
        private Transition _lastAddedTransition;

        [TestInitialize]
        public void SetupMocks()
        {
            var location1 = new Location {Id = 1};
            var location2 = new Location {Id = 2};
            var location3 = new Location {Id = 3};

            var locationRepositoryMock = new Mock<IMapSystemLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((int x) =>
                {
                    switch (x)
                    {
                        case 1:
                            return location1;
                        case 2:
                            return location2;
                        case 3:
                            return location3;
                        default:
                            return null;
                    }
                });
            _locationRepository = locationRepositoryMock.Object;

            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.AddTransition(It.IsAny<Transition>()))
                .Returns(Task.CompletedTask)
                .Callback((Transition t) => { _lastAddedTransition = t; });
            transitionRepositoryMock
                .Setup(a => a.ExistTransitionBetween(1, 2))
                .ReturnsAsync(true);
            transitionRepositoryMock
                .Setup(a => a.ExistTransitionBetween(1, 3))
                .ReturnsAsync(false);
            _transitionRepository = transitionRepositoryMock.Object;
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public async Task FromDoesNotExist_Exception()
        {
            await new CreateTransitionCommandHandler(_locationRepository, _transitionRepository)
                    .Handle(
                        new CreateTransitionCommand {FromLocationId = 4, ToLocationId = 2},
                        CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public async Task ToDoesNotExist_Exception()
        {
            await new CreateTransitionCommandHandler(_locationRepository, _transitionRepository)
                .Handle(
                    new CreateTransitionCommand { FromLocationId = 2, ToLocationId = 4 },
                    CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public async Task TransitionAlreadyExistExist_Exception()
        {
            await new CreateTransitionCommandHandler(_locationRepository, _transitionRepository)
                .Handle(
                    new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 2 },
                    CancellationToken.None);
        }

        [TestMethod]
        public async Task Ok()
        {
            await new CreateTransitionCommandHandler(_locationRepository, _transitionRepository)
                .Handle(
                    new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 3 },
                    CancellationToken.None);

            Assert.IsNotNull(_lastAddedTransition);
            Assert.AreEqual(_lastAddedTransition.From.Id, 1);
            Assert.AreEqual(_lastAddedTransition.To.Id, 3);
        }
    }
}
