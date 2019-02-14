using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace AniRPG.Application.Game.MapSystem.UseCases.Locations.Commands
{
    public class CreateLocationCommand : IRequest
    {
        public string Name { get; set; }
    }
}
