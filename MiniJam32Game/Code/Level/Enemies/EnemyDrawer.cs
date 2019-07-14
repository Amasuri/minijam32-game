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

        //For Enemy eyes only
        private const int bat_oneFrameMs = 50;
        private const int bat_frameAmount = 5;
        static private int bat_oneStep => (int)TileData.ScaledTileSize.X / bat_frameAmount;
        static private int bat_maxAnimTime => bat_oneFrameMs * bat_frameAmount;

        public static void LoadAssets(Minijam32 game)
        {
            batAnim = new Animation(game, "res/mob/bat_right", 16, Minijam32.Scale, 50);
            batAnim.EnableDrawing();
        }

        public static void DrawThisTypeAt(SpriteBatch batch, EnemyAI enemy)
        {
            var type = enemy.type;
            var tilePos = enemy.currentPos;
            var effect = enemy.lastMove.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            var animTime = enemy.TimeSinceLastMove() > bat_maxAnimTime ? bat_maxAnimTime : enemy.TimeSinceLastMove();
            var lastMove = enemy.lastMove;

            Vector2 offset = GenerateOffset(animTime, lastMove);

            if (type == EnemyAI.Type.CommonBat)
                batAnim.Draw(batch, effect, Minijam32.DeltaDraw, new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y) + offset, doTick: false);
            else
                batAnim.Draw(batch, effect, Minijam32.DeltaDraw, new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y) + offset, doTick: false);

            //Debug
            //batAnim.Draw(batch, effect, Minijam32.DeltaDraw, new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y), doTick: false);
        }

        public static void UpdateTicks(float delta)
        {
            batAnim.Tick(delta);
        }

        private static Vector2 GenerateOffset(float animTime, Point lastMove)
        {
            int currentStep = bat_oneStep * ((int)animTime / (int)bat_oneFrameMs);

            return (lastMove.ToVector2() * currentStep) - lastMove.ToVector2() * bat_frameAmount * bat_oneStep;
        }
    }
}
