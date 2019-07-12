using BPO.Minijam32.Level;
using BPO.Minijam32.Level.Tile;
using Microsoft.Xna.Framework;

namespace BPO.Minijam32.Player
{
    /// <summary>
    /// Your standard data manager: saving, remembering what have been done, etc.
    /// </summary>
    public static class PlayerDataManager
    {
        static public Point tilePosition { get; private set; }
        static public Point lastMove { get; private set; }

        static public void InitData()
        {
            tilePosition = new Point(0, 0);
        }

        /// <summary>
        /// Is internal because only PlayerController can do this.
        /// </summary>
        static internal void Move(LevelData level, Point move)
        {
            tilePosition += move;
            lastMove = move;

            if (TileData.IsSolid(level.tileGrid[tilePosition.X, tilePosition.Y].type))
                tilePosition -= move;
        }
    }
}
