using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Amasuri.Reusable.Graphics
{
    /// <summary>
    /// A special class that loads several images and on .Draw() cycle iterates over them per some time.
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// One frame width for horizontal stripes, one frame height for vertical stripes.
        /// Used as divisor.
        /// </summary>
        protected int frameLimInPixels;
        protected int changeFrameMs;
        protected int currentFrameInd;
        protected float currentTimeMs;
        protected int maxFrameInd;
        protected bool isDrawn;
        protected Point frameSize;
        protected float imageScale;
        protected bool isALoop;
        private Vector2 staticPos;
        private readonly Texture2D img;

        //TODO:
        public Animation(Game game, string path, int frameLimiterInPixels, float imgScale = 1.0f, int msBetweenFrames = 80, int x=0, int y=0, Texture2D sheet = null)
        {
            this.staticPos = new Vector2(x, y);

            if (sheet == null)
                this.img = game.Content.Load<Texture2D>(path);
            else
                this.img = sheet;

            this.changeFrameMs = msBetweenFrames;
            this.frameLimInPixels = frameLimiterInPixels;
            this.currentFrameInd = 0;
            this.currentTimeMs = 0;
            this.isDrawn = false;

            this.maxFrameInd = (this.img.Width / this.frameLimInPixels) - 1;
            this.frameSize = new Point(frameLimiterInPixels, this.img.Height);

            this.imageScale = imgScale;
            this.isALoop = false;
        }

        /// <summary>
        /// Draws current frame of animation, with offset from usual pos by offset.
        /// </summary>
        public void Draw(SpriteBatch spritebatch, Vector2 offsetFromUsualDrawPos, SpriteEffects effects, float delta, bool doTick = true)
        {
            if (!this.isDrawn)
                return;

            spritebatch.Draw(this.img, this.staticPos+offsetFromUsualDrawPos, new Rectangle(
                  frameSize.X * currentFrameInd,
                  0,
                  frameSize.X,
                  frameSize.Y),
                  Color.White, 0f, Vector2.Zero, this.imageScale, effects, 1);

            if(doTick)
                this.Tick(delta);
        }

        /// <summary>
        /// Draws current frame of animation at pos.
        /// </summary>
        public void Draw(SpriteBatch spritebatch, SpriteEffects effects, float delta, Vector2 pos, bool doTick = true)
        {
            if (!this.isDrawn)
                return;

            spritebatch.Draw(this.img, pos, new Rectangle(
                    frameSize.X * currentFrameInd,
                    0,
                    frameSize.X,
                    frameSize.Y),
                Color.White, 0f, Vector2.Zero, this.imageScale, effects, 1);

            if (doTick)
                this.Tick(delta);
        }

        /// <summary>
        /// Draws animation at default pos.
        /// </summary>
        public void Draw(SpriteBatch spritebatch, SpriteEffects effects, float delta, bool doTick = true)
        {
            if (!this.isDrawn)
                return;

            spritebatch.Draw(this.img, staticPos, new Rectangle(
                    frameSize.X * currentFrameInd,
                    0,
                    frameSize.X,
                    frameSize.Y),
                Color.White, 0f, Vector2.Zero, this.imageScale, effects, 1);

            if (doTick)
                this.Tick(delta);
        }

        /// <summary>
        /// At set ms intervals, rotate current frame indexes whithin [0 , maxIndex].
        /// Usually is called inside draw, but can be called separatedly for shared assets
        /// (to not Tick gajillion times for one obj and multiple draws)
        /// </summary>
        public void Tick(float delta)
        {
            this.currentTimeMs += delta;

            //Inscrementing frame, if the gap is more that standart set gap
            if (this.currentTimeMs >= this.changeFrameMs)
            {
                this.currentFrameInd++;
                this.currentTimeMs -= this.changeFrameMs;
            }

            //On last frame, either start over or stop drawing, depending on how was animation initialized
            if (this.currentFrameInd > this.maxFrameInd)
            {
                if (this.isALoop)
                    this.currentFrameInd = 0;
                else
                    this.DisableDrawing();
            }
        }

        /// <summary>
        /// Do actions necessary on enabling (resetting index, deciding loopable/one-shot), but only if not already set.
        /// </summary>
        public void EnableDrawing(bool isALoop = true)
        {
            if (this.isDrawn)
                return;

            this.isDrawn = true;
            this.isALoop = isALoop;
            this.currentFrameInd = 0;
        }

        /// <summary>
        /// Do actions necessary on enabling (resetting index, deciding loopable/one-shot), but only if not already set.
        /// Also changes draw pos.
        /// </summary>
        public void EnableDrawingOnPos(Vector2 pos, bool isALoop = true)
        {
            SetDrawPos(pos);
            EnableDrawing(isALoop);
        }

        /// <summary>
        /// Do actions necessary on disabling (resetting index), but only if not already disabled.
        /// </summary>
        public void DisableDrawing()
        {
            if (!this.isDrawn)
                return;

            this.isDrawn = false;
            this.currentFrameInd = 0;
        }

        public void SetDrawPos(Vector2 newPos)
        {
            this.staticPos = newPos;
        }

        public Vector2 GetSingleFrameSize()
        {
            return frameSize.ToVector2();
        }
    }
}
