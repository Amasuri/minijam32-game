using Amasuri.Reusable.Graphics;
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
            FloorDirtMediumPlain = 1,

            //3-4: Bomb tile zone

            //5-18: Walls zone (roofings IDs are skipped, because are drawn only on demand)
            WallBricksContourFinished = 5,

            //19-29: Water IDs
            FloorWaterStillSimple = 19,

            //29+: etc... name them whatever, you can rename the current ones with F2 if you think it fits better, too
        }

        public enum State
        {
            Normal,
            Destroyed,
        }

        public readonly Type type;
        public State state { get; private set; }

        static public Vector2 TileSize => new Vector2(16, 16);
        static public Vector2 ScaledTileSize => TileSize * Minijam32.Scale;

        public TileData(Type type, State state = State.Destroyed)
        {
            this.type = type;
            this.state = state;
        }

        public void Destroy()
        {
            this.state = State.Destroyed;
        }

        /// <summary>
        /// Can player pass through this block type?
        /// </summary>
        public static bool IsSolid(Type type)
        {
            return
                type == Type.WallBricksContourFinished;
        }

        /// <summary>
        /// Is this water?
        /// </summary>
        public static bool IsWater(Type type)
        {
            return
                type == Type.FloorWaterStillSimple;
        }

        /// <summary>
        /// For tiles like walls that have a bit of a head above it.
        /// Which is drawn in another cycle above player.
        /// </summary>
        public static bool HasBlockAboveIt(Type type)
        {
            return
                type == Type.WallBricksContourFinished;
        }
    }
}
