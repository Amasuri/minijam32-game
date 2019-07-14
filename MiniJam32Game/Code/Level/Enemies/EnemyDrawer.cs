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
    static public class EnemyDrawer
    {
        static private Animation batAnim;

        public static void LoadAssets(Minijam32 game)
        {
            batAnim = new Animation(game, "res/mob/bat_right", 16, Minijam32.Scale, 50);
            batAnim.EnableDrawing();
        }

        public static void DrawThisTypeAt(SpriteBatch batch, Point tilePos, EnemyAI.Type type)
        {
            if (type == EnemyAI.Type.CommonBat)
                batAnim.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw, new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y), doTick: false);
            else
                batAnim.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw, new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y), doTick: false);
        }

        public static void UpdateTicks(float delta)
        {
            batAnim.Tick(delta);
        }
    }
}
