using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Locations.Commands.DeleteLocation;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Locations.Commands
{
    public class DeleteLocationTests
    {
        [Fact]
        public async Task Ok()
        {
            var deletedLocationId = -1;
            var locationRepositoryMock = new Mock<IContentLocationRepository>();
            locationRepositoryMock
                .Setup(a => a.DeleteEntity(1))
                .Returns(Task.CompletedTask)
                .Callback((int locId) => deletedLocationId = locId);

            var handler = new DeleteLocationCommandHandler(locationRepositoryMock.Object);
            var command = new DeleteLocationCommand {LocationId = 1};

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.True(res);
            Assert.Equal(1, deletedLocationId);
        }
    }
}