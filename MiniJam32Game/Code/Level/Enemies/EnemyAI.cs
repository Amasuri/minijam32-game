using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Level.Tile;
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

        private const int defaultWaitTime = 500;
        private float currentWaitTime;

        public Type type { get; private set; }
        public int currentHp { get; private set; }
        public Point currentPos { get; private set; }
        public bool isDead => currentHp <= 0;

        public EnemyAI(Type type, Point startingPos)
        {
            this.type = type;
            this.currentHp = 2;
            this.currentPos = startingPos;
            this.currentWaitTime = defaultWaitTime;
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

                if(!TileData.IsSolid( game.levelData.tileGrid[newPos.X, newPos.Y].type) && !game.levelData.IsBombAtThisPosition(newPos))
                {
                    hasGeneratedPos = true;
                    this.currentPos += move;
                }
            }
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
    }
}
