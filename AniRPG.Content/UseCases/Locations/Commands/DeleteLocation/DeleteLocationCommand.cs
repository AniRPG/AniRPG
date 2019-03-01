using MediatR;

namespace AniRPG.Content.UseCases.Locations.Commands.DeleteLocation
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public int LocationId { get; set; }
    }
}
