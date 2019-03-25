using System.Threading;
using System.Threading.Tasks;
using AniRPG.Content.Repositories;
using AniRPG.Content.UseCases.Transitions.Commands.DeleteTransition;
using Moq;
using Xunit;

namespace AniRPG.Content.Tests.UseCases.Transitions.Commands
{
    public class DeleteTransitionTests
    {
        [Fact]
        public async Task Ok()
        {
            var deletedTransitionId = -1;
            var transitionRepositoryMock = new Mock<IContentTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.DeleteEntity(1))
                .Returns(Task.CompletedTask)
                .Callback((int tId) => deletedTransitionId = tId);

            var handler = new DeleteTransitionCommandHandler(transitionRepositoryMock.Object);
            var command = new DeleteTransitionCommand {TransitionId = 1};

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.True(res);
            Assert.Equal(1, deletedTransitionId);
        }
    }
}
