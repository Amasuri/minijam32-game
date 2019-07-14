using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Level.Tile;
using BPO.Minijam32.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Level.Enemies
{
    /// <summary>
    /// The only thing in difference between enemies is their looks, right?
    /// So, for right now one class is enough.
    /// In case of something, this can become an abstract class - the base for different AIs and such.
    /// </summary>
    public class EnemyAI
    {
        public enum Type
        {
            CommonBat,
            SomeOtherMook,
        }

        private const int defaultWaitTime = 1000;
        private const int hastedWaitTime = 500;
        private float currentWaitTime;

        public Type type { get; private set; }
        public int currentHp { get; private set; }
        private const int maxEnemyHp = 1;
        public Point currentPos { get; private set; }
        public bool isDead => currentHp <= 0;

        /// <summary>
        /// Internal because for "Enemy" assembly only: only the drawer needs to know this
        /// </summary>
        internal Point lastMove;

        public EnemyAI(Type type, Point startingPos)
        {
            this.type = type;
            this.currentHp = maxEnemyHp;
            this.currentPos = startingPos;
            this.currentWaitTime = defaultWaitTime;
            this.lastMove = new Point(0, 0);
        }

        public void Update(Minijam32 game)
        {
            currentWaitTime -= Minijam32.DeltaUpdate;

            if(currentWaitTime <= 0)
            {
                currentWaitTime = defaultWaitTime;
                this.Move(game);
            }
        }

        private void Move(Minijam32 game)
        {
            //The idea is that a straight line difference is always integer, but the non-straight line will always have some weirdass numbers
            bool playerInStraightLine = (this.currentPos - PlayerDataManager.tilePosition).ToVector2().Length() % 1 == 0;

            bool success = false;
            if (playerInStraightLine)
            {
                success = TryMoveInStraightLine(game);
            }

            //For every other case including finding a straight line but meeting a wall, use random movements
            if(!success)
                ControlRandomMovement(game);
        }

        private bool TryMoveInStraightLine(Minijam32 game)
        {
            //Obtain the direction and, since we're tile-based, make only one step at a time
            Point move = PlayerDataManager.tilePosition - this.currentPos;
            move = new Point(Math.Sign(move.X), Math.Sign(move.Y));

            //Remember new position for later calculations
            Point newPos = this.currentPos + move;

            //Try to see if the tiles on straight line don't contain any bad ones like solid or bombs
            int tilesInLine = (int)(PlayerDataManager.tilePosition - this.currentPos).ToVector2().Length();
            for (int i = 1; i < tilesInLine; i++)
            {
                bool normalTile = IsThisNewPosOkay(game, (move.ToVector2() * i * Math.Sign(move.X + move.Y)).ToPoint() + this.currentPos);
                if (!normalTile)
                    return false;
            }

            //Try to see if there aren't any tiles like bombs and solid tiles, and move
            if (IsThisNewPosOkay(game, newPos))
            {
                this.currentPos += move;
                this.lastMove = move;

                return true;
            }

            //We've failed every check up to that
            return false;
        }

        private void ControlRandomMovement(Minijam32 game)
        {
            bool hasGeneratedPos = false;
            int i = 0;

            //Later there will be a check "if 4 sides are blocked then no check"

            while (!hasGeneratedPos && i < 10)
            {
                i++;

                Point move = this.GenerateNewDirectionalMove();
                Point newPos = this.currentPos + move;

                if (newPos.X <= 0 || newPos.Y <= 0 || newPos.X >= game.levelData.tileGrid.GetLength(0) || newPos.Y >= game.levelData.tileGrid.GetLength(1))
                    continue;

                if (IsThisNewPosOkay(game, newPos))
                {
                    hasGeneratedPos = true;
                    this.currentPos += move;
                    this.lastMove = move;
                }
            }
        }

        private static bool IsThisNewPosOkay(Minijam32 game, Point newPos)
        {
            return !TileData.IsSolid(game.levelData.tileGrid[newPos.X, newPos.Y].type) && !game.levelData.IsBombAtThisPosition(newPos) && !game.levelData.IsEnemyThere(newPos);
        }

        private Point GenerateNewDirectionalMove()
        {
            bool xOrY = Minijam32.Rand.Next(100) >= 49;
            int move = Minijam32.Rand.Next(3) - 1;

            return xOrY == true ? new Point(move, 0) : new Point(0, move);
        }

        public void Damage()
        {
            this.currentHp -= 1;
        }

        /// <summary>
        /// Internal because for "Enemy" assembly only: only the drawer needs to know this
        /// </summary>
        internal float TimeSinceLastMove()
        {
            return defaultWaitTime - this.currentWaitTime;
        }
    }
}
