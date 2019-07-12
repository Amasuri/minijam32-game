using Microsoft.Xna.Framework;

namespace BPO.Minijam32.Player
{
    /// <summary>
    /// Your standard data manager: saving, remembering what have been done, etc.
    /// </summary>
    public static class PlayerDataManager
    {
        static public Point tilePosition { get; private set; }

        static public void InitData()
        {
            tilePosition = new Point(0, 0);
        }

        /// <summary>
        /// Is internal because only PlayerController can do this.
        /// </summary>
        static internal void Move(Point move)
        {
            tilePosition += move;
        }
    }
}
