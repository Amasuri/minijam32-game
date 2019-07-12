using BPO.Minijam32.Level.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Player
{
    static public class PlayerDrawer
    {
        //On player move, it'll remember the last position and base it's drawing (aka player's facing direction) based on that
        //But it'll check only on move, else it'd change only for one frame and get set to default value

        public enum State
        {
            FacingDown
        }
        static private State currentState;
        static private Texture2D spritesheet;
        static private Dictionary<State, Rectangle> stateSourceRect;
        static private Vector2 heroDrawOffset;

        static public void InitAssets(Minijam32 game)
        {
            currentState = State.FacingDown;
            heroDrawOffset = new Vector2(0, -2);
            spritesheet = game.Content.Load<Texture2D>("res/mob/hero");
            stateSourceRect = new Dictionary<State, Rectangle>
            {
                { State.FacingDown, new Rectangle(0, 14, 16, 18) },
            };
        }

        static public void DrawCurrentState(SpriteBatch batch, Point tilePos)
        {
            batch.Draw
            (
                spritesheet,
                new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y) + heroDrawOffset * Minijam32.Scale,
                stateSourceRect[currentState],
                Color.White,
                0.0f,
                Vector2.Zero, //table of origins for walls?
                Minijam32.Scale,
                SpriteEffects.None,
                0.0f
            );
        }
    }
}
