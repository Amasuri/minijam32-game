using Amasuri.Reusable.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BPO.Minijam32
{
    public class Minijam32 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static float DeltaUpdate { get; private set; }
        public static float DeltaDraw { get; private set; }

        static public float Scale { get; private set; }
        static public int UnscaledWidth { get; private set; }
        static public int UnscaledHeight { get; private set; }

        public ScreenPool screenPool;

        public Minijam32()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            UnscaledWidth = 128;
            UnscaledHeight = 64;
            Scale = 4;

            graphics.PreferredBackBufferWidth = (int)(UnscaledWidth * Scale);
            graphics.PreferredBackBufferHeight = (int)(UnscaledHeight * Scale);
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            this.Window.Title = "Minijam32 BPO game";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenPool = new ScreenPool(this);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaUpdate = gameTime.ElapsedGameTime.Milliseconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DeltaDraw = gameTime.ElapsedGameTime.Milliseconds;

            base.Draw(gameTime);
        }
    }
}
