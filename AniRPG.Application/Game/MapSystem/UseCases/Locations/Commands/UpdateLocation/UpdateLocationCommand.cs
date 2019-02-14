using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AniRPG.Domain.Entities;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Commands.UpdateLocation
{
    public class UpdateLocationCommand : IRequest
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Location> Transitions { get; private set; }
        UpdateLocationCommand()
        {
            Transitions = new HashSet<Location>();
        }
    }
}
