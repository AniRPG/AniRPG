using System.Threading;
using System.Threading.Tasks;
using AniRPG.Common.Exceptions;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Locations.Queries.GetLocation;
using AniRPG.Domain.Entities;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Locations.Queries
{
    public class GetLocationTests
    {
        private readonly IContentLocationRepository _locationRepository;
        private readonly GetLocationQueryHandler _handler;
        private readonly Location _location;

        public GetLocationTests()
        {
            _location = new Location {Id = 1, Name = "TestName", Description = "TestDescription"};
            var locationRepositoryMock = new Mock<IContentLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.GetEntity(It.IsAny<int>()))
                .ReturnsAsync((int id) => id == 1 ? _location : null);
            _locationRepository = locationRepositoryMock.Object;

            _handler = new GetLocationQueryHandler(_locationRepository);
        }

        [Fact]
        public async Task Ok()
        {
            var query = new GetLocationQuery {LocationId = 1};

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.StrictEqual(_location, result);
        }

        [Fact]
        public async Task LocationNotExist_Exception()
        {
            var query = new GetLocationQuery { LocationId = 2 };
            Task Act() => _handler.Handle(query, CancellationToken.None);

            var ex = await Record.ExceptionAsync(Act) as EntityNotFoundException<Location>;

            Assert.NotNull(ex);
            Assert.Equal(2, ex.EntityId);
        }
    }
}