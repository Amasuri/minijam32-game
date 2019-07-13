using System;
using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Level.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BPO.Minijam32.LevelEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LevelEditor : Minijam32
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState keys;
        private KeyboardState oldKeys;
        private MouseState mouse;
        private MouseState oldMouse;

        private Point currentTile;
        private TileData.Type tileInHand;

        private TileData[,] newTileGrid;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            this.CopyFromOriginalLevelData();
            tileInHand = TileData.Type.FloorWaterStillSimple;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (keys.IsKeyDown(Keys.S) && keys.IsKeyDown(Keys.LeftControl) && oldKeys.IsKeyUp(Keys.S))
                this.SaveState();

            currentTile = new Point((int)(mouse.Position.X / TileData.ScaledTileSize.X), (int)(mouse.Position.Y / TileData.ScaledTileSize.Y));

            if (mouse.LeftButton == ButtonState.Pressed && newTileGrid[currentTile.X, currentTile.Y].type != tileInHand)
                newTileGrid[currentTile.X, currentTile.Y] = new TileData(tileInHand);

            oldKeys = keys;
            oldMouse = mouse;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            DrawTiles();
            DrawTileInHand();
            DrawTileSelection();

            spriteBatch.End();
        }

        private void DrawTileInHand()
        {
            TileDrawer.DrawTileAt(spriteBatch, tileInHand, currentTile);
        }

        private void DrawTileSelection()
        {
            var pix = new Pixel(this.GraphicsDevice);

            pix.Draw(spriteBatch, Color.White, this.currentTile.ToVector2() * TileData.ScaledTileSize.X, new Vector2(Scale, Scale * 16));
            pix.Draw(spriteBatch, Color.White, this.currentTile.ToVector2() * TileData.ScaledTileSize.X + new Vector2(0, TileData.ScaledTileSize.X), new Vector2(Scale * 16, Scale));
        }

        private void DrawTiles()
        {
            //Draw downtiles
            for (int x = newTileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = newTileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    TileDrawer.DrawTileAt(spriteBatch, newTileGrid[x, y].type, new Point(x, y));
                }

            //Drawuptiles
            for (int x = newTileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = newTileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    TileDrawer.DrawTileRoofingAt(spriteBatch, newTileGrid[x, y].type, new Point(x, y));
                }
        }

        private void SaveState()
        {
            throw new NotImplementedException();
        }

        private void CopyFromOriginalLevelData()
        {
            this.newTileGrid = new TileData[this.levelData.tileGrid.GetLength(0), this.levelData.tileGrid.GetLength(1)];

            for (int x = newTileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = newTileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    newTileGrid[x, y] = new TileData(this.levelData.tileGrid[x, y].type);
                }
        }
    }
}
