using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Commands.DeleteLocation
{
    public class DeleteLocationCommand : IRequest
    {
        public int LocationId { get; set; }
    }
}
