using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Level.Tile
{
    static public class TileDrawer
    {
        static private Texture2D tileSheet;
        static private Dictionary<TileData.Type, Vector2> typeSourceRect;

        static public void InitAssets(Minijam32 game)
        {
            tileSheet = game.Content.Load<Texture2D>("res/tile/general");

            typeSourceRect = new Dictionary<TileData.Type, Vector2>
            {
                { TileData.Type.FloorDirtMediumPlain, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 0) },
                { TileData.Type.FloorWaterStillSimple, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 4) },
                { TileData.Type.WallBricksContourFinished, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 3) },
            };
        }

        static public void DrawTileAt(SpriteBatch batch, TileData.Type type, Point tilePos)
        {
            batch.Draw
            (
                tileSheet,
                new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y),
                new Rectangle( typeSourceRect[type].ToPoint(), TileData.TileSize.ToPoint()),
                Color.White,
                0.0f,
                Vector2.Zero,
                Minijam32.Scale,
                SpriteEffects.None,
                0.0f
            );
        }

        static public void DrawTileRoofingAt(SpriteBatch batch, TileData.Type type, Point tilePos)
        {
            if (!TileData.HasBlockAboveIt(type))
                return;

            batch.Draw
            (
                tileSheet,
                new Vector2(tilePos.X * TileData.ScaledTileSize.X, (tilePos.Y - 1) * TileData.ScaledTileSize.Y),
                new Rectangle(typeSourceRect[type].ToPoint() - new Point( 0, (int)TileData.TileSize.Y), TileData.TileSize.ToPoint()),
                Color.White,
                0.0f,
                Vector2.Zero,
                Minijam32.Scale,
                SpriteEffects.None,
                0.0f
            );
        }
    }
}
