using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Domain.Entities;
using AniRPG.Game.Systems.MapSystem.Repositories;
using AniRPG.Game.Systems.MapSystem.UseCases.Commands.MoveCharacterByTransition;
using AniRPG.Game.Systems.MapSystem.UseCases.Exceptions;
using Moq;
using Xunit;

namespace AniRPG.Game.Tests.Systems.MapSystem.UseCases.Commands
{
    public class MoveCharacterByTransitionTests
    {
        private readonly IMapSystemTransitionRepository _transitionRepository;
        private readonly IMapSystemCharacterRepository _characterRepository;

        private Character _updatedCharacter;
        
        public MoveCharacterByTransitionTests()
        {
            var locationA = new Location {Id = 1};
            var locationB = new Location {Id = 2};
            var transition1 = new Transition {Id = 1, From = locationA, To = locationB};
            var transition2 = new Transition { Id = 2, From = locationB, To = locationA };
            var character = new Character {Id = 1, CurrentLocation = locationA};

            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.GetTransition(It.IsAny<int>()))
                .ReturnsAsync((int tId) =>
                {
                    switch (tId)
                    {
                        case 1:
                            return transition1;
                        case 2:
                            return transition2;
                        default:
                            return null;
                    }
                });
            _transitionRepository = transitionRepositoryMock.Object;

            var characterRepositoryMock = new Mock<IMapSystemCharacterRepository>();
            characterRepositoryMock
                .Setup(a => a.GetCharacter(It.IsAny<int>()))
                .ReturnsAsync((int cId) => cId == 1 ? character : null);
            characterRepositoryMock
                .Setup(a => a.UpdateCharacter(It.IsAny<Character>()))
                .Returns(Task.CompletedTask)
                .Callback((Character c) => _updatedCharacter = c);
            _characterRepository = characterRepositoryMock.Object;
        }

        [Fact]
        public async Task CharacterDoesNotExist_Exception()
        {
            var handler = new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository);
            var command = new MoveCharacterByTransitionCommand {CharacterId = 2, TransitionId = 1};
            Task Act() => handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException;

            Assert.Null(_updatedCharacter);
            Assert.NotNull(ex);
            Assert.Equal(2, ex.EntityId);
            Assert.IsType<EntityNotFoundException<Character>>(ex);
        }

        [Fact]
        public async Task TransitionDoesNotExist_Exception()
        {
            var handler = new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository);
            var command = new MoveCharacterByTransitionCommand { CharacterId = 1, TransitionId = 5 };
            Task Act() => handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException;

            Assert.Null(_updatedCharacter);
            Assert.NotNull(ex);
            Assert.Equal(5, ex.EntityId);
            Assert.IsType<EntityNotFoundException<Transition>>(ex);
        }

        [Fact]
        public async Task CurrentLocationAndFromMismatch_Exception()
        {
            var handler = new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository);
            var command = new MoveCharacterByTransitionCommand { CharacterId = 1, TransitionId = 2 };
            Task Act() => handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as CharacterCurrentLocationMismatchException;

            Assert.Null(_updatedCharacter);
            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Ok()
        {
            var handler = new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository);
            var command = new MoveCharacterByTransitionCommand { CharacterId = 1, TransitionId = 1 };

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.True(res);
            Assert.NotNull(_updatedCharacter);
            Assert.StrictEqual(1, _updatedCharacter.Id);
            Assert.NotNull(_updatedCharacter.CurrentLocation);
            Assert.StrictEqual(2, _updatedCharacter.CurrentLocation.Id);
        }
    }
}
