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
            FloorDirtMediumPlain, //I suppose it would be better to break them down & rewrite IDs into zones
            FloorWaterStillSimple, //Like, zone 70-80 is for water
            WallBricksContourFinished, //That would make checking really easy, because water is not (insert "&& that tile" x10), but "(int)id > 69 && (int)id <81"
        }

        public enum State
        {
            Normal,
            Destroyed,
        }

        /// <summary>
        /// For tiles like walls that have a bit of a head above it.
        /// Which is drawn in another cycle above player.
        /// </summary>
        public readonly bool IsTwoBlocks;
        public readonly Type type;
        public State state { get; private set; }

        static public Vector2 TileSize => new Vector2(16, 16);
        static public Vector2 ScaledTileSize => TileSize * Minijam32.Scale;

        public TileData(Type type, State state = State.Destroyed, bool IsTwoBlocks = false)
        {
            this.type = type;
            this.state = state;
            this.IsTwoBlocks = IsTwoBlocks;
        }

        public void Destroy()
        {
            this.state = State.Destroyed;
        }

        public static bool IsSolid(Type type)
        {
            return
                type == Type.FloorWaterStillSimple ||
                type == Type.WallBricksContourFinished;
        }
    }
}
