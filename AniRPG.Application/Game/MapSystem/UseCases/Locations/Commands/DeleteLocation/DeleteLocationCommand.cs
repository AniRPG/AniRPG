using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Commands.DeleteLocation
{
    public class DeleteLocationCommand : IRequest
    {
        public int LocationId { get; set; }
    }
}
