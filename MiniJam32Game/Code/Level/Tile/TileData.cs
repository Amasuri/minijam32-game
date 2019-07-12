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
        public Vector2 TileSize => new Vector2(15, 15);
        public Vector2 ScaledTileSize => TileSize * Minijam32.Scale;

        public enum Type
        {
            FloorDirt,
            FloorWater,
            WallDirt,
        }

        public enum State
        {
            Normal,
            Destroyed,
        }

        public readonly Type type;
        public State state { get; private set; }

        public TileData(Type type, State state = State.Destroyed)
        {
            this.type = type;
            this.state = state;
        }

        public void Destroy()
        {
            this.state = State.Destroyed;
        }
    }
}
