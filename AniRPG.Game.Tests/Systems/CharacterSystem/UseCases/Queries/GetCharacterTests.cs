using System.Threading;
using AniRPG.Domain.Entities;
using AniRPG.Game.Systems.CharacterSystem.Repositories;
using AniRPG.Game.Systems.CharacterSystem.UseCases.Queries.GetCharacter;
using Moq;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using Xunit;

namespace AniRPG.Game.Tests.Systems.CharacterSystem.UseCases.Queries
{
    public class GetCharacterTests
    {
        private readonly ICharacterSystemCharacterRepository _characterRepository;
        private readonly GetCharacterQueryHandler _handler;

        private readonly Character _character;

        public GetCharacterTests()
        {
            _character = new Character {Id = 1, Name = "Alex"};

            var characterRepositoryMock = new Mock<ICharacterSystemCharacterRepository>();
            characterRepositoryMock
                .Setup(a => a.GetCharacter(It.IsAny<int>()))
                .ReturnsAsync((int id) => id == _character.Id ? _character : null);
            _characterRepository = characterRepositoryMock.Object;

            _handler = new GetCharacterQueryHandler(_characterRepository);
        }

        [Fact]
        public async Task Ok()
        {
            var query = new GetCharacterQuery {Id = _character.Id};

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.StrictEqual(_character, result);
        }

        [Fact]
        public async Task CharacterNotExist_Exception()
        {
            const int missingId = 2;
            var query = new GetCharacterQuery {Id = missingId};
            Task Act() => _handler.Handle(query, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException<Character>;

            Assert.NotNull(ex);
            Assert.Equal(missingId, ex.EntityId);
        }
    }
}