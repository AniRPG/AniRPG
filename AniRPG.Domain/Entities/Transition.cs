using AniRPG.Domain.Common;

namespace AniRPG.Domain.Entities
{
    public class Transition : IEntity
    {
        public int Id { get; set; }
        public Location From { get; set; }
        public Location To { get; set; }
    }
}