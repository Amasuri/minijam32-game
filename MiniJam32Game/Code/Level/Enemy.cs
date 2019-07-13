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
            this.currentHp = 1;
            this.currentPos = startingPos;
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
    }
}
