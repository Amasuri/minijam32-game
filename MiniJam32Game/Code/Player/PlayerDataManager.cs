using System;
using BPO.Minijam32.Level;
using BPO.Minijam32.Level.Tile;
using BPO.Minijam32.Music;
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

        private static float currentMoveCoolDown;
        private const float maxMoveCoolDown = 400f;

        static public int coins { get; private set; }

        static public Point tilePosition { get; private set; }
        static public Point lastMove { get; private set; }

        static public void InitData(LevelData level)
        {
            tilePosition = level.currentPlayerDefaultLocation;
            currentHP = maxHP;
            currentInvis = 0f;
            coins = 00;
        }

        static public void Damage()
        {
            if (currentInvis >= 0f)
                return;

            currentHP -= 1;
            currentInvis = maxInvis;
        }

        static public void GetCoin()
        {
            coins++;
        }

        static public void Update()
        {
            currentInvis -= Minijam32.DeltaUpdate;

            if (currentMoveCoolDown >= 0)
            {
                currentMoveCoolDown -= Minijam32.DeltaUpdate;
            }
        }

        /// <summary>
        /// Is internal because only PlayerController can do this.
        /// </summary>
        static internal void TryMove(LevelData level, Point move)
        {
            //if(currentMoveCoolDown >= 0)
            //{
            //    return;
            //}

            tilePosition += move;
            lastMove = move;

            if (TileData.IsSolid(level.tileGrid[tilePosition.X, tilePosition.Y].type) || level.IsBombAtThisPosition(tilePosition))
            {
                tilePosition -= move;
                return;
            }

            currentMoveCoolDown = maxMoveCoolDown;
            PlayerDrawer.NotifyAboutSuccessfulMoving();
        }

        static internal void ResetBeforeNewLevel(Point newPlayerStartLocation)
        {
            currentMoveCoolDown = 0f;
            tilePosition = newPlayerStartLocation;
            currentInvis = 0f;
        }

        static public void Heal()
        {
            SoundPlayer.PlaySound(SoundPlayer.Type.HealPlayer);
            currentHP++;
        }
    }
}
