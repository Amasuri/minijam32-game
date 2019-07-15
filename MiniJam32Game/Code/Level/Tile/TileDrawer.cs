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
                { TileData.Type.FloorDirtLight, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 0) },
                { TileData.Type.FloorDirtMediumPlain, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 0) },
                { TileData.Type.FloorDirtDark, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 0) },

                { TileData.Type.BombOne, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 1) },
                { TileData.Type.BombTwo, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 1) },

                { TileData.Type.WallBricksContourFinished, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksLeftFinished, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksMiddleFinished, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksRightFinished, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopNarrow, new Vector2(TileData.TileSize.X * 4, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopLeft, new Vector2(TileData.TileSize.X * 5, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopCenter, new Vector2(TileData.TileSize.X * 6, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopRight, new Vector2(TileData.TileSize.X * 7, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopLowCornerNarrow, new Vector2(TileData.TileSize.X * 8, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopLowCornerLeft, new Vector2(TileData.TileSize.X * 9, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopLowCornerRight, new Vector2(TileData.TileSize.X * 10, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopHighCornerNarrow, new Vector2(TileData.TileSize.X * 11, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopHighCornerLeft, new Vector2(TileData.TileSize.X * 12, TileData.TileSize.Y * 3) },
                { TileData.Type.WallBricksTopHighCornerRight, new Vector2(TileData.TileSize.X * 13, TileData.TileSize.Y * 3) },

                { TileData.Type.FloorWaterStillSimple, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeWallNarrow, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeWallLeft, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeWallCenter, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeWallRight, new Vector2(TileData.TileSize.X * 4, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeNarrow, new Vector2(TileData.TileSize.X * 5, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeLeft, new Vector2(TileData.TileSize.X * 6, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeRight, new Vector2(TileData.TileSize.X * 7, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeCornerNarrow, new Vector2(TileData.TileSize.X * 8, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeCornerLeft, new Vector2(TileData.TileSize.X * 9, TileData.TileSize.Y * 4) },
                { TileData.Type.FloorWaterEdgeCornerRight, new Vector2(TileData.TileSize.X * 10, TileData.TileSize.Y * 4) },

                { TileData.Type.RockBasic, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 5) },
                { TileData.Type.RockFunky, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 5) },
                { TileData.Type.RockBlues, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 5) },
                { TileData.Type.RockProg, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 5) },
                { TileData.Type.RockHard, new Vector2(TileData.TileSize.X * 4, TileData.TileSize.Y * 5) },
                { TileData.Type.RockBread, new Vector2(TileData.TileSize.X * 5, TileData.TileSize.Y * 5) },
                { TileData.Type.RockGrave, new Vector2(TileData.TileSize.X * 6, TileData.TileSize.Y * 5) },
                { TileData.Type.RockTurtle, new Vector2(TileData.TileSize.X * 7, TileData.TileSize.Y * 5) },

                { TileData.Type.RockBasicGold, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 6) },
                { TileData.Type.RockFunkyGold, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 6) },
                { TileData.Type.RockBluesGold, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 6) },
                { TileData.Type.RockProgGold, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 6) },
                { TileData.Type.RockHardGold, new Vector2(TileData.TileSize.X * 4, TileData.TileSize.Y * 6) },

                { TileData.Type.GoldBasic, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 7) },
                { TileData.Type.GoldFunky, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 7) },
                { TileData.Type.GoldBlues, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 7) },
                { TileData.Type.GoldProg, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 7) },
                { TileData.Type.GoldHard, new Vector2(TileData.TileSize.X * 4, TileData.TileSize.Y * 7) },

                { TileData.Type.ButtonRed, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 8) },
                { TileData.Type.ButtonYellow, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 8) },
                { TileData.Type.ButtonBlue, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 8) },

                { TileData.Type.ButtonRedPressed, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 9) },
                { TileData.Type.ButtonYellowPressed, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 9) },
                { TileData.Type.ButtonBluePressed, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 9) },

                { TileData.Type.ColorWallRed, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 11) },
                { TileData.Type.ColorWallYellow, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 11) },
                { TileData.Type.ColorWallBlue, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 11) },

                { TileData.Type.Ice, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 12) },
                { TileData.Type.NewLevelHole, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 13) },

                { TileData.Type.PassableWallRoofComplete, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 2) },
                { TileData.Type.PassableWallRoofLeft, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 2) },
                { TileData.Type.PassableWallRoofMiddle, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 2) },
                { TileData.Type.PassableWallRoofRight, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 2) },

                { TileData.Type.ColorWallTopRed, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 10) },
                { TileData.Type.ColorWallTopYellow, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 10) },
                { TileData.Type.ColorWallTopBlue, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 10) },

                { TileData.Type.ColorWallRemainderRed, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 14) },
                { TileData.Type.ColorWallRemainderYellow, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 14) },
                { TileData.Type.ColorWallRemainderBlue, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 14) },

                { TileData.Type.NewWallTopToBeRemoved, new Vector2(TileData.TileSize.X * 15, TileData.TileSize.Y * 3) },

                //Those are unreachable in editor
                { TileData.Type.CharacterDeathDown, new Vector2(TileData.TileSize.X * 0, TileData.TileSize.Y * 15) },
                { TileData.Type.CharacterDeathRight, new Vector2(TileData.TileSize.X * 1, TileData.TileSize.Y * 15) },
                { TileData.Type.CharacterDeathUp, new Vector2(TileData.TileSize.X * 2, TileData.TileSize.Y * 15) },
                { TileData.Type.CharacterDeathLeft, new Vector2(TileData.TileSize.X * 3, TileData.TileSize.Y * 15) },
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
            if (!TileData.IsUpperRoof(type))
                return;

            batch.Draw
            (
                tileSheet,
                new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y),
                new Rectangle(typeSourceRect[type].ToPoint(), TileData.TileSize.ToPoint()),
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
