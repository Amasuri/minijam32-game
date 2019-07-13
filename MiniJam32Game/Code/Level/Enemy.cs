using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Level.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Level
{
    /// <summary>
    /// The only thing in difference between enemies is their looks, right?
    /// So, for right now one class is enough.
    /// In case of something, this can become an abstract class - the base for different AIs and such.
    /// </summary>
    public class Enemy
    {
        public enum Type
        {
            SomeMook,
            SomeOtherMook,
        }

        //Split this shit to EnemyDrawer in case of post-jam
        private static Texture2D allEnemiesSheet;
        private static Dictionary<Type, Rectangle> typesPosOnSheet;
        private static Pixel placeHolderEnemyDrawer;

        private const int defaultWaitTime = 500;
        private float currentWaitTime;

        public Type type { get; private set; }
        public int currentHp { get; private set; }
        public Point currentPos { get; private set; }
        public bool isDead => currentHp <= 0;

        //Split this shit to EnemyDrawer in case of post-jam
        static public void LoadAssets(Minijam32 game)
        {
            placeHolderEnemyDrawer = new Pixel(game.GraphicsDevice);
        }

        public Enemy(Type type, Point startingPos)
        {
            this.type = type;
            this.currentHp = 2;
            this.currentPos = startingPos;
            this.currentWaitTime = defaultWaitTime;
        }

        public void DrawAt(SpriteBatch batch)
        {
            //Example source from TileDrawer:

            //batch.Draw
            //(
            //    tileSheet,
            //    new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y),
            //    new Rectangle(typeSourceRect[type].ToPoint(), TileData.TileSize.ToPoint()),
            //    Color.White,
            //    0.0f,
            //    Vector2.Zero,
            //    Minijam32.Scale,
            //    SpriteEffects.None,
            //    0.0f
            //);

            //Placeholder code:
            if (type == Type.SomeMook)
                placeHolderEnemyDrawer.Draw(batch, Color.Purple, currentPos.ToVector2() * TileData.ScaledTileSize.X, TileData.ScaledTileSize);
            else if (type == Type.SomeOtherMook)
                placeHolderEnemyDrawer.Draw(batch, Color.Red, currentPos.ToVector2() * TileData.ScaledTileSize.X, TileData.ScaledTileSize);
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
