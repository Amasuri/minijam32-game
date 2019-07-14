using Amasuri.Reusable.Graphics;
using BPO.Minijam32.GraphicsBase;
using BPO.Minijam32.GUI.Level;
using BPO.Minijam32.Level;
using BPO.Minijam32.Level.Enemies;
using BPO.Minijam32.Level.Tile;
using BPO.Minijam32.Music;
using BPO.Minijam32.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BPO.Minijam32
{
    public class Minijam32 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static float DeltaUpdate { get; private set; }
        public static float DeltaDraw { get; private set; }

        static public int Scale { get; private set; }
        static public int UnscaledWidth { get; private set; }
        static public int UnscaledHeight { get; private set; }

        static public int ScaledWidth => UnscaledWidth * Scale;
        static public int ScaledHeight => UnscaledHeight * Scale;

        public ScreenPool screenPool;
        public MusicPlayer musicPlayer;

        public LevelData levelData;

        static public Random Rand;

        public Minijam32()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            UnscaledWidth = 342;
            UnscaledHeight = 214;
            Scale = 3;

            graphics.PreferredBackBufferWidth = (int)(UnscaledWidth * Scale);
            graphics.PreferredBackBufferHeight = (int)(UnscaledHeight * Scale);
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            this.Window.Title = "Minijam32 BPO game";

            Rand = new Random();

            levelData = new LevelData(this);
            PlayerDataManager.InitData(levelData);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TileDrawer.InitAssets(this);
            PlayerDrawer.InitAssets(this);
            EnemyDrawer.LoadAssets(this);

            screenPool = new ScreenPool(this);

            this.musicPlayer = new MusicPlayer(this);
            SoundPlayer.InitAssets(this);

            Animator.InitAssets(this);
            InfoDrawer.LoadAssets(this);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaUpdate = gameTime.ElapsedGameTime.Milliseconds;

            this.screenPool.CallGuiControlUpdates(this);

            if (this.screenPool.screenState == ScreenPool.ScreenState.Playing)
            {
                this.levelData.Update(this);
                PlayerDataManager.Update();
                EnemyDrawer.UpdateTicks(Minijam32.DeltaUpdate);
            }

            musicPlayer.Update(this);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DeltaDraw = gameTime.ElapsedGameTime.Milliseconds;

            this.screenPool.CallDraws(this, spriteBatch, this.GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}
