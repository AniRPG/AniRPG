using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Locations;
using AniRPG.Content.UseCases.Locations.Commands.UpdateLocation;
using AniRPG.Domain.Entities;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Locations.Commands
{
    public class UpdateLocationTests
    {
        private readonly IContentLocationRepository _locationRepository;
        private readonly UpdateLocationCommandHandler _handler;
        private Location _location;

        public UpdateLocationTests()
        {
            _location = new Location {Id = 1, Name = "Location", Description = "Description"};
            var locationRepositoryMock = new Mock<IContentLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.GetEntity(It.IsAny<int>()))
                .ReturnsAsync((int id) => id == 1 ? _location : null);
            locationRepositoryMock
                .Setup(a => a.UpdateEntity(It.IsAny<Location>()))
                .Returns(Task.CompletedTask)
                .Callback((Location location) => _location = location);
            _locationRepository = locationRepositoryMock.Object;

            _handler = new UpdateLocationCommandHandler(_locationRepository);
        }

        [Fact]
        public async Task Ok()
        {
            var newName = "testName";
            var newDescription = "testDescription";
            var command = new UpdateLocationCommand {LocationId = 1, Name = newName, Description = newDescription};

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.StrictEqual(_location, result);
            Assert.Equal(result.Name, newName);
            Assert.Equal(result.Description, newDescription);
        }

        [Fact]
        public async Task LocationNotExist_Exception()
        {
            var command = new UpdateLocationCommand { LocationId = 2, Name = "abc", Description = "abc" };
            Task Act() => _handler.Handle(command, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException<Location>;

            Assert.NotNull(ex);
            Assert.Equal(2, ex.EntityId);
        }

        [Theory]
        [ClassData(typeof(UpdateLocationCommandGenerator))]
        public async Task InvalidCommand_Exception(int nameLength, int descriptionLength)
        {
            if (nameLength < 0 || descriptionLength < 0)
                return;
            var name = new string('x', nameLength);
            var description = new string('x', descriptionLength);
            var validator = new UpdateLocationCommandValidator();
            var command = new UpdateLocationCommand {Name = name, Description = description};

            var result = await validator.ValidateAsync(command);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }

        private class UpdateLocationCommandGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {LocationConstants.NameMinLength - 1, LocationConstants.DescriptionMinLength},
                new object[] {LocationConstants.NameMaxLength + 1, LocationConstants.DescriptionMinLength},
                new object[] {LocationConstants.NameMinLength, LocationConstants.DescriptionMinLength - 1},
                new object[] {LocationConstants.NameMinLength, LocationConstants.DescriptionMaxLength + 1}
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}