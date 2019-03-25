using System;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Locations;
using AniRPG.Content.UseCases.Locations.Commands.CreateLocation;
using AniRPG.Domain.Entities;
using FluentValidation;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Locations.Commands
{
    public class CreateLocationTests
    {
        private readonly IContentLocationRepository _locationRepository;
        private readonly CreateLocationCommandHandler _handler;
        private Location _addedLocation = null;

        public CreateLocationTests()
        {
            var locationRepositoryMock = new Mock<IContentLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.AddEntity(It.IsAny<Location>()))
                .Returns((Location location) => Task.FromResult(location))
                .Callback((Location location) => _addedLocation = location);
            _locationRepository = locationRepositoryMock.Object;
            
            _handler = new CreateLocationCommandHandler(_locationRepository);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("an")]
        public async Task Ok(string locationName)
        {
            var command = new CreateLocationCommand {Name = locationName};

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(locationName, result.Name);
            Assert.StrictEqual(_addedLocation, result);
        }

        [Theory]
        [InlineData(LocationConstants.NameMinLength - 1)]
        [InlineData(LocationConstants.NameMaxLength + 1)]
        public async Task InvalidName_ValidationException(int locationNameLength)
        {
            var locationName = new string('x', locationNameLength);
            var command = new CreateLocationCommand { Name = locationName };
            var validator = new CreateLocationCommandValidator();

            var result = await validator.ValidateAsync(command);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }
    }
}