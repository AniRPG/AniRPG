using System;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.Common.Exceptions;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.UseCases.Commands.MoveCharacterByTransition;
using AniRPG.Application.Game.MapSystem.UseCases.Exceptions;
using AniRPG.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AniRPG.ApplicationTests.Game.MapSystem.UseCases.Commands
{
    [TestClass]
    public class MoveCharacterByTransitionTests
    {
        private IMapSystemTransitionRepository _transitionRepository;
        private IMapSystemCharacterRepository _characterRepository;

        private Character _updatedCharacter;

        [TestInitialize]
        public void SetupMocks()
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

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public async Task CharacterDoesNotExist_Exception()
        {
            await new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository)
                .Handle(new MoveCharacterByTransitionCommand {CharacterId = 2, TransitionId = 1},
                    CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public async Task TransitionDoesNotExist_Exception()
        {
            await new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository)
                .Handle(new MoveCharacterByTransitionCommand { CharacterId = 1, TransitionId = 5 },
                    CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(CharacterCurrentLocationMismatchException))]
        public async Task CurrentLocationAndFromMismatch_Exception()
        {
            await new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository)
                .Handle(new MoveCharacterByTransitionCommand { CharacterId = 1, TransitionId = 2 },
                    CancellationToken.None);
        }

        [TestMethod]
        public async Task Ok()
        {
            await new MoveCharacterByTransitionCommandHandler(_transitionRepository, _characterRepository)
                .Handle(new MoveCharacterByTransitionCommand { CharacterId = 1, TransitionId = 1 },
                    CancellationToken.None);

            Assert.IsNotNull(_updatedCharacter);
            Assert.AreEqual(_updatedCharacter.Id, 1);
            Assert.AreEqual(_updatedCharacter.CurrentLocation.Id, 2);
        }
    }
}
