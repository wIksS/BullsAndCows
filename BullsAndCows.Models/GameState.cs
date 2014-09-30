using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public enum GameState
    {
        WaitingForOpponent,
        RedInTurn,
        BlueInTurn,
        Finished
    }
}
