using System;
using System.Collections.Generic;
using System.Text;
using AniRPG.Domain.Common;

namespace AniRPG.Domain.Entities
{
    public class Character : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location CurrentLocation { get; set; }
    }
}
