using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Domain.Entities;
using AniRPG.Game.Systems.CharacterSystem.Constants;
using AniRPG.Game.Systems.CharacterSystem.Repositories;
using AniRPG.Game.Systems.CharacterSystem.UseCases.Commands.CreateCharacter;
using Moq;
using Xunit;

namespace AniRPG.Game.Tests.Systems.CharacterSystem.UseCases.Commands
{
    public class CreateCharacterTests
    {
        private readonly ICharacterSystemCharacterRepository _characterRepository;
        private readonly CreateCharacterCommandHandler _handler;
        private readonly string _alreadyExistingName = "John";
        private Character _addedCharacter;
        

        public CreateCharacterTests()
        {
            var characterRepositoryMock = new Mock<ICharacterSystemCharacterRepository>();
            characterRepositoryMock
                .Setup(a => a.CharacterExistsWithName(It.IsAny<string>()))
                .ReturnsAsync((string name) => name.Equals(_alreadyExistingName));
            characterRepositoryMock
                .Setup(a => a.AddCharacter(It.IsAny<Character>()))
                .Returns(Task.CompletedTask)
                .Callback((Character character) => _addedCharacter = character);
            _characterRepository = characterRepositoryMock.Object;
            _handler = new CreateCharacterCommandHandler(_characterRepository);
        }

        [Fact]
        public async Task Ok()
        {
            const string name = "Alex";
            var command = new CreateCharacterCommand {Name = name};

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.StrictEqual(_addedCharacter, result);
            Assert.Equal(name, result.Name);
            Assert.Null(result.CurrentLocation);
        }

        [Fact]
        public async Task WithSameNameExist_Exception()
        {
            var command = new CreateCharacterCommand{Name = _alreadyExistingName};
            Task Act() => _handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityAlreadyExistsException<Character>;

            Assert.NotNull(ex);
            Assert.Equal(nameof(command.Name), ex.ConflictingPropertyName);
            Assert.Equal(_alreadyExistingName, ex.ConflictingPropertyValue);
        }

        [Theory]
        [InlineData(CharacterConstants.NameMinLength - 1)]
        [InlineData(CharacterConstants.NameMaxLength + 1)]
        public async Task InvalidName_ValidationException(int nameLength)
        {
            var name = new string('x', nameLength);
            var command = new CreateCharacterCommand {Name = name};
            var validator = new CreateCharacterCommandValidator();

            var result = await validator.ValidateAsync(command);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }
    }
}