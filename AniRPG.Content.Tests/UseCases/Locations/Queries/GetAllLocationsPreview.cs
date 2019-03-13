using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Locations.Queries.GetAllLocationsPreview;
using AniRPG.Domain.Entities;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Locations.Queries
{
    public class GetAllLocationsPreview
    {
        private readonly IContentLocationRepository _locationRepository;
        private readonly GetAllLocationsPreviewQueryHandler _handler;
        private readonly IEnumerable<Location> _locations;

        public GetAllLocationsPreview()
        {
            _locations = new List<Location>
            {
                new Location {Id = 1, Name = "Name1", Description = "Description1"},
                new Location {Id = 2, Name = "Name2", Description = "Description2"},
                new Location {Id = 3, Name = "Name3", Description = "Description3"}
            };

            var locationRepositoryMock = new Mock<IContentLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.GetAllLocations())
                .ReturnsAsync(_locations);
            _locationRepository = locationRepositoryMock.Object;

            _handler = new GetAllLocationsPreviewQueryHandler(_locationRepository);
        }

        [Fact]
        public async Task Ok()
        {
            var query = new GetAllLocationsPreviewQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            var resultAsList = result.ToList();
            Assert.Equal(_locations.Count(), resultAsList.Count);
            foreach (var locationPreviewModel in resultAsList)
            {
                var location = _locations.FirstOrDefault(x => x.Id == locationPreviewModel.Id);
                Assert.NotNull(location);
                Assert.Equal(location.Name, locationPreviewModel.Name);
            }
        }
    }
}