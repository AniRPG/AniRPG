using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Transitions.Commands.CreateTransition;
using AniRPG.Content.UseCases.Transitions.Exceptions;
using AniRPG.Domain.Entities;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Transitions.Commands
{
    public class CreateTransitionTests
    {
        private readonly IContentLocationRepository _locationRepository;
        private readonly IContentTransitionRepository _transitionRepository;
        private readonly CreateTransitionCommandHandler _handler;

        private Transition _lastAddedTransition;
        
        public CreateTransitionTests()
        {
            var locations = new List<Location>
            {
                new Location {Id = 1},
                new Location {Id = 2},
                new Location {Id = 3},
            };

            var locationRepositoryMock = new Mock<IContentLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.GetEntity(It.IsAny<int>()))
                .ReturnsAsync((int id) => locations.FirstOrDefault(x => x.Id == id));
            _locationRepository = locationRepositoryMock.Object;

            var transitionRepositoryMock = new Mock<IContentTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.AddEntity(It.IsAny<Transition>()))
                .Returns(Task.CompletedTask)
                .Callback((Transition t) => { _lastAddedTransition = t; });
            transitionRepositoryMock
                .Setup(a => a.ExistTransitionBetween(1, 2))
                .ReturnsAsync(true);
            transitionRepositoryMock
                .Setup(a => a.ExistTransitionBetween(1, 3))
                .ReturnsAsync(false);
            _transitionRepository = transitionRepositoryMock.Object;

            _handler = new CreateTransitionCommandHandler(_locationRepository, _transitionRepository);
        }

        [Fact]
        public async Task FromDoesNotExist_Exception()
        {
            var command = new CreateTransitionCommand { FromLocationId = 4, ToLocationId = 2 };
            Task Act() => _handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException;

            Assert.Null(_lastAddedTransition);
            Assert.NotNull(ex);
            Assert.Equal(4, ex.EntityId);
            Assert.IsType<EntityNotFoundException<Location>>(ex);
        }

        [Fact]
        public async Task ToDoesNotExist_Exception()
        {
            var command = new CreateTransitionCommand { FromLocationId = 2, ToLocationId = 4 };
            Task Act() => _handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException;

            Assert.Null(_lastAddedTransition);
            Assert.NotNull(ex);
            Assert.Equal(4, ex.EntityId);
            Assert.IsType<EntityNotFoundException<Location>>(ex);
        }

        [Fact]
        public async Task TransitionAlreadyExist_Exception()
        {
            var command = new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 2 };
            Task Act() => _handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as TransitionAlreadyExistsException;

            Assert.Null(_lastAddedTransition);
            Assert.NotNull(ex);
            Assert.Equal(1, ex.FromLocationId);
            Assert.Equal(2, ex.ToLocationId);
        }

        [Fact]
        public async Task Ok()
        {
            var command = new CreateTransitionCommand { FromLocationId = 1, ToLocationId = 3 };

            var transition = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(transition);
            Assert.StrictEqual(_lastAddedTransition, transition);

            Assert.NotNull(transition.From);
            Assert.Equal(1, transition.From.Id);

            Assert.NotNull(transition.To);
            Assert.Equal(3, transition.To.Id);
        }
    }
}
