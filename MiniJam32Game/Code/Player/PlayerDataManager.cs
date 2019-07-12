using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Player
{
    /// <summary>
    /// Your standard data manager: saving, remembering what have been done, etc.
    ///
    /// Is __internal__ because data managment belongs to Player assembly in general, not to other entities.
    /// PlayerController invoked many of it's calls and manages interfacing via other stuff.
    /// This all is to simplify debugging.
    /// </summary>
    static internal class PlayerDataManager
    {
        static private Point tilePosition;

        static internal void Move(Point move)
        {
            tilePosition += move;
        }
    }
}
