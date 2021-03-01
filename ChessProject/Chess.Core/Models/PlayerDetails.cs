using System;

namespace Chess.Core.Models
{
    public class PlayerDetails : Entity<Guid>
    {
        public int BulletRating { get; set; }

        public int BlitzRating { get; set; }

        public int RapidRating { get; set; }

        public int OnlyPawnsRating { get; set; }

        public int OnlyKnightsRating { get; set; }

        public int OnlyQueensRating { get; set; }

    }
}
