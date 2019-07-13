using System;
using System.Collections.Generic;
using System.IO;
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
            tileInHand = TileData.Type.FloorDirtMediumPlain;
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

            //Saving
            if (keys.IsKeyDown(Keys.S) && keys.IsKeyDown(Keys.LeftControl) && oldKeys.IsKeyUp(Keys.S))
                this.SaveState();

            //Getting current tile under the mouse
            currentTile = new Point((int)(mouse.Position.X / TileData.ScaledTileSize.X), (int)(mouse.Position.Y / TileData.ScaledTileSize.Y));

            //Setting new tile on press
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if(newTileGrid[currentTile.X, currentTile.Y] == null)
                    newTileGrid[currentTile.X, currentTile.Y] = new TileData(tileInHand);
                else if (newTileGrid[currentTile.X, currentTile.Y].type != tileInHand)
                    newTileGrid[currentTile.X, currentTile.Y] = new TileData(tileInHand);
            }

            //Switching tiles in hand
            if (mouse.ScrollWheelValue > oldMouse.ScrollWheelValue)
                JumpToNextAppropriateTile(mouse, oldMouse, jumping: +1);
            if (mouse.ScrollWheelValue < oldMouse.ScrollWheelValue)
                JumpToNextAppropriateTile(mouse, oldMouse, jumping: -1);

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
                    if(newTileGrid[x, y] != null)
                        TileDrawer.DrawTileAt(spriteBatch, newTileGrid[x, y].type, new Point(x, y));
                }

            //Drawuptiles
            for (int x = newTileGrid.GetLength(0) - 1; x >= 0; x--)
                for (int y = newTileGrid.GetLength(1) - 1; y >= 0; y--)
                {
                    if (newTileGrid[x, y] != null)
                        TileDrawer.DrawTileRoofingAt(spriteBatch, newTileGrid[x, y].type, new Point(x, y));
                }
        }

        private void SaveState()
        {
            string[] file = new string[newTileGrid.GetLength(1)];

            for (int y = 0; y < newTileGrid.GetLength(1); y++)
            {
                string appendix = "";
                for (int x = 0; x < newTileGrid.GetLength(0); x++)
                {
                    if (newTileGrid[x, y] == null)
                        newTileGrid[x, y] = new TileData(TileData.Type.FloorDirtMediumPlain);
                    appendix += (char)( (int)newTileGrid[x, y].type + (int)'@');
                }
                file[y] = appendix;
            }

            File.WriteAllLines("New Data/levelX.leveldata", file);
        }

        private void CopyFromOriginalLevelData()
        {
            this.newTileGrid = new TileData[(int)(UnscaledWidth / TileData.TileSize.X) + 1, (int)(UnscaledHeight / TileData.TileSize.Y) + 1];

            for (int x = 0; x < this.levelData.tileGrid.GetLength(0); x++)
                for (int y = 0; y < this.levelData.tileGrid.GetLength(1); y++)
                {
                    newTileGrid[x, y] = new TileData(this.levelData.tileGrid[x, y].type);
                }
        }

        private void JumpToNextAppropriateTile(MouseState mouse, MouseState oldMouse, int jumping)
        {
            TileData.Type oldType = this.tileInHand;

            if (jumping > 0 && (int)oldType + 1 < 57)
                tileInHand = oldType + 1;
            else if (jumping < 0 && (int)oldType - 1 >= 0)
                tileInHand = oldType - 1;
            else
                tileInHand = oldType;
        }
    }
}
