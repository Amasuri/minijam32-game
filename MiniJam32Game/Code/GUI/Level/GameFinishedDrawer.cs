using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.GUI.Level
{
    public class GameFinishedDrawer
    {
        private Pixel screen;
        private Texture2D sorryNothing;
        private int fadeOut;

        public GameFinishedDrawer(Minijam32 game)
        {
            screen = new Pixel(game.GraphicsDevice);
            sorryNothing = game.Content.Load<Texture2D>("res/gui/sorry_nothing");
            fadeOut = 0;
        }

        public void DrawGameCompletedScene(Minijam32 game, SpriteBatch batch)
        {
            screen.Draw(batch, new Color(21, 15, 10, fadeOut), Vector2.Zero, new Vector2(Minijam32.ScaledWidth, Minijam32.ScaledHeight));

            if (fadeOut < 255)
            {
                fadeOut++;
            }
            else
            {
                game.screenPool.menuGui.DrawBackground(batch);
                batch.Draw(sorryNothing, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
                InfoDrawer.DrawCoinsGui(batch, centered: true);
            }
        }

        public void Update(Minijam32 game, KeyboardState keys, KeyboardState oldKeys)
        {
            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
            {
                game.levelData.ResetToDefault();
                game.screenPool.GoMenu();
                PlayerDataManager.ResetToDefaultState();
            }
        }
    }
}
