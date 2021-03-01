using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Core.GameModels
{
    public enum GameType
    {
        Bullet = 1,
        Blitz,
        Rapid,
        OnlyPawnsMode,
        OnlyKnightsMode,
        OnlyQueensMode
    }
}
