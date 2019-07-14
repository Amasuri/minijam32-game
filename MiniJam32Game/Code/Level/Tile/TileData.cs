using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Level.Tile
{
    public class TileData
    {
        public enum Type
        {
            //I suppose it would be better to break them down & rewrite IDs into zones
            //Like, zone 70-80 is for water
            //That would make checking really easy, because water is not (insert "&& that tile" x10 times), but "(int)id > 69 && (int)id <81"

            //0-2: Dirts zone (when in doubt refer to the tile spritesheet)
            FloorDirtLight = 0,
            FloorDirtMediumPlain = 1,
            FloorDirtDark = 2,

            //3-4: Bomb tile zone
            BombOne = 3,
            BombTwo = 4,

            //5-18: Walls zone (roofings IDs are skipped, because are drawn only on demand)
            WallBricksContourFinished = 5,
            WallBricksLeftFinished = 6,
            WallBricksMiddleFinished = 7,
            WallBricksRightFinished = 8,
            WallBricksTopNarrow = 9,
            WallBricksTopLeft = 10,
            WallBricksTopCenter = 11,
            WallBricksTopRight = 12,
            WallBricksTopLowCornerNarrow = 13,
            WallBricksTopLowCornerLeft = 14,
            WallBricksTopLowCornerRight = 15,
            WallBricksTopHighCornerNarrow = 16,
            WallBricksTopHighCornerLeft = 17,
            WallBricksTopHighCornerRight = 18,

            //19-29: Water IDs
            FloorWaterStillSimple = 19,
            FloorWaterEdgeWallNarrow = 20,
            FloorWaterEdgeWallLeft = 21,
            FloorWaterEdgeWallCenter = 22,
            FloorWaterEdgeWallRight = 23,
            FloorWaterEdgeNarrow = 24,
            FloorWaterEdgeLeft = 25,
            FloorWaterEdgeRight= 26,
            FloorWaterEdgeCornerNarrow = 27,
            FloorWaterEdgeCornerLeft = 28,
            FloorWaterEdgeCornerRight = 29,

            //30-37: Rocks
            RockBasic = 30,
            RockFunky = 31,
            RockBlues = 32,
            RockProg = 33,
            RockHard = 34,
            RockBread = 35,
            RockGrave = 36,
            RockTurtle = 37,

            //38-42: Rocks with a bit of gold
            RockBasicGold = 38,
            RockFunkyGold = 39,
            RockBluesGold = 40,
            RockProgGold = 41,
            RockHardGold = 42,

            //43-47: Gold
            GoldBasic = 43,
            GoldFunky = 44,
            GoldBlues = 45,
            GoldProg = 46,
            GoldHard = 47,

            //48-50: Buttons
            ButtonRed = 48,
            ButtonYellow = 49,
            ButtonBlue = 50,

            //51-53: Pressed buttons
            ButtonRedPressed = 51,
            ButtonYellowPressed = 52,
            ButtonBluePressed = 53,

            //54-56: Color-coded walls
            ColorWallRed = 54,
            ColorWallYellow = 55,
            ColorWallBlue = 56,

            //57: Ice ice baby
            Ice = 57,

            //58: Rabbit hole
            NewLevelHole = 58,
        }

        public Type type { get; private set; }

        static public Vector2 TileSize => new Vector2(16, 16);
        static public Vector2 ScaledTileSize => TileSize * Minijam32.Scale;

        public TileData(Type type)
        {
            this.type = type;
        }

        public void Destroy()
        {
            if (!IsDestructable(type))
                return;

            if (IsHalfGolden(type))
            {
                PlayerDataManager.GetCoin();
            }
            else if (IsGolden(type))
            {
                PlayerDataManager.GetCoin();
                PlayerDataManager.GetCoin();
            }

            this.type = Type.FloorDirtMediumPlain;
        }

        /// <summary>
        /// Is this block type unpassable?
        /// </summary>
        public static bool IsSolid(Type type)
        {
            return
                (int)type >= (int)Type.BombOne && (int)type <= (int)Type.WallBricksTopHighCornerRight || IsDestructable(type) || IsWater(type);
        }

        /// <summary>
        /// Is this water?
        /// </summary>
        public static bool IsWater(Type type)
        {
            return
                (int)type >= (int)Type.FloorWaterStillSimple && (int)type <= (int)Type.FloorWaterEdgeCornerRight;
        }

        /// <summary>
        /// For tiles like walls that have a bit of a head above it.
        /// Which is drawn in another cycle above player.
        /// </summary>
        public static bool HasBlockAboveIt(Type type)
        {
            return
                ((int)type >= (int)Type.WallBricksContourFinished && (int)type <= (int)Type.WallBricksRightFinished) ||
                ((int)type >= (int)Type.ColorWallBlue && (int)type <= (int)Type.ColorWallYellow);
        }

        /// <summary>
        /// Can player destruct this tile (stone) with a bomb?
        /// </summary>
        public static bool IsDestructable(Type type)
        {
            return
                (int)type >= (int)Type.RockBasic && (int)type <= (int)Type.GoldHard;
        }

        /// <summary>
        /// Gives one coin on destruction.
        /// </summary>
        public static bool IsHalfGolden(Type type)
        {
            return
                (int)type >= (int)Type.RockBasicGold && (int)type <= (int)Type.RockHardGold;
        }

        /// <summary>
        /// Gives two coins on destruction.
        /// </summary>
        public static bool IsGolden(Type type)
        {
            return
                (int)type >= (int)Type.GoldBasic && (int)type <= (int)Type.GoldHard;
        }
    }
}
