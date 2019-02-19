using System;
using System.Threading;
using System.Threading.Tasks;
using AniRPG.Application.Game.MapSystem.Repositories;
using AniRPG.Application.Game.MapSystem.UseCases.Commands.DeleteTransition;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AniRPG.ApplicationTests.Game.MapSystem.UseCases.Commands
{
    [TestClass]
    public class DeleteTransitionTests
    {
        [TestMethod]
        public async Task Ok()
        {
            int deletedTransitionId = -1;
            var transitionRepositoryMock = new Mock<IMapSystemTransitionRepository>();
            transitionRepositoryMock
                .Setup(a => a.DeleteTransition(1))
                .Returns(Task.CompletedTask)
                .Callback((int tId) => deletedTransitionId = tId);

            await new DeleteTransitionCommandHandler(transitionRepositoryMock.Object)
                .Handle(new DeleteTransitionCommand {TransitionId = 1}, CancellationToken.None);

            Assert.AreEqual(deletedTransitionId, 1);
        }
    }
}
