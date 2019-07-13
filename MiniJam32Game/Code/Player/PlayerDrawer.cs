using Amasuri.Reusable.Graphics;
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
            FacingDownStill = 0,
            FacingLeftStill = 4,
            FacingUpStill = 8,
            FacingRightStill = 12,
        }
        static private State currentState;
        static private Texture2D spritesheet;
        static private Dictionary<State, Rectangle> stateSourceRect;
        static private Vector2 heroDrawOffset;
        static private Vector2 animationDrawOffset => new Vector2(0, -16) * Minijam32.Scale;
        static private int frameStep => (int)TileData.ScaledTileSize.X / maxFrames;

        static private Animation movingLeft;
        static private Animation movingRight;
        static private Animation movingUp;
        static private Animation movingDown;

        static private bool isMoving => currentAnimationMs > 0f;
        private const int maxFrames = 4;
        private const float maxAnimationMs = maxFrames * oneFrameTime;
        private const int oneFrameTime = 100;
        static private float currentAnimationMs;

        static public void InitAssets(Minijam32 game)
        {
            currentState = State.FacingDownStill;
            heroDrawOffset = new Vector2(0, -2);
            spritesheet = game.Content.Load<Texture2D>("res/mob/hero");
            stateSourceRect = new Dictionary<State, Rectangle>
            {
                { State.FacingDownStill, new Rectangle(0, 14, 16, 18) },
                { State.FacingLeftStill, new Rectangle(0, 46, 16, 18) },
                { State.FacingUpStill, new Rectangle(0, 78, 16, 18) },
                { State.FacingRightStill, new Rectangle(0, 110, 16, 18) },
            };

            movingLeft = new Animation(game, "res/mob/hero_left", 16, Minijam32.Scale, oneFrameTime);
            movingRight = new Animation(game, "res/mob/hero_right", 16, Minijam32.Scale, oneFrameTime);
            movingUp = new Animation(game, "res/mob/hero_up", 16, Minijam32.Scale, oneFrameTime);
            movingDown = new Animation(game, "res/mob/hero_down", 16, Minijam32.Scale, oneFrameTime);

            currentAnimationMs = 0f;
        }

        static public void DrawCurrentState(SpriteBatch batch, Point tilePos)
        {
            //State is literally draw state, so it makes sense to put & update it right on draw cycles
            UpdateCurrentState();

            //Select the correct source rect & draw it
            if (!isMoving)
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
            else
            {
                var drawPos = new Vector2(tilePos.X * TileData.ScaledTileSize.X, tilePos.Y * TileData.ScaledTileSize.Y) + animationDrawOffset;
                switch (currentState)
                {
                    case State.FacingDownStill:
                        movingDown.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw, drawPos + new Vector2(0, maxFrames - frameStep * ((int)currentAnimationMs/(int)oneFrameTime)));
                        break;

                    case State.FacingLeftStill:
                        movingLeft.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw, drawPos + new Vector2(frameStep * ((int)currentAnimationMs / (int)oneFrameTime), 0));
                        break;

                    case State.FacingRightStill:
                        movingRight.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw, drawPos + new Vector2(maxFrames - frameStep * ((int)currentAnimationMs / (int)oneFrameTime), 0));
                        break;

                    case State.FacingUpStill:
                        movingUp.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw, drawPos + new Vector2(0, frameStep * ((int)currentAnimationMs / (int)oneFrameTime)));
                        break;
                }
            }
        }

        private static void UpdateCurrentState()
        {
            var lastMove = PlayerDataManager.lastMove;

            if (lastMove == new Point(0, -1))
                currentState = State.FacingUpStill;
            if (lastMove == new Point(0, +1))
                currentState = State.FacingDownStill;
            if (lastMove == new Point(-1, 0))
                currentState = State.FacingLeftStill;
            if (lastMove == new Point(+1, 0))
                currentState = State.FacingRightStill;

            if (currentAnimationMs > 0f)
                currentAnimationMs -= Minijam32.DeltaDraw;
        }

        public static void NotifyAboutSuccessfulMoving()
        {
            currentAnimationMs = maxAnimationMs;

            var lastMove = PlayerDataManager.lastMove;

            if (lastMove == new Point(0, -1))
                movingUp.EnableDrawing(isALoop: true);
            if (lastMove == new Point(0, +1))
                movingDown.EnableDrawing(isALoop: true);
            if (lastMove == new Point(-1, 0))
                movingLeft.EnableDrawing(isALoop: true);
            if (lastMove == new Point(+1, 0))
                movingRight.EnableDrawing(isALoop: true);
        }
    }
}
