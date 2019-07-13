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
        private const int maxHP = 3;
        static public int currentHP { get; private set; }
        static public bool isDead => currentHP <= 0;

        static private float currentInvis;
        private const float maxInvis = 1000f;

        static public Point tilePosition { get; private set; }
        static public Point lastMove { get; private set; }

        static public void InitData(LevelData level)
        {
            tilePosition = level.currentPlayerDefaultLocation;
            currentHP = maxHP;
            currentInvis = 0f;
        }

        static public void Damage()
        {
            if (currentInvis >= 0f)
                return;

            currentHP -= 1;
            currentInvis = maxInvis;
        }

        static public void Update()
        {
            currentInvis -= Minijam32.DeltaUpdate;
        }

        /// <summary>
        /// Is internal because only PlayerController can do this.
        /// </summary>
        static internal void Move(LevelData level, Point move)
        {
            tilePosition += move;
            lastMove = move;

            if (TileData.IsSolid(level.tileGrid[tilePosition.X, tilePosition.Y].type) || level.IsBombAtThisPosition(tilePosition))
                tilePosition -= move;
        }
    }
}
