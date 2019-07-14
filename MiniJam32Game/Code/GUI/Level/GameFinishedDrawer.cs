using Amasuri.Reusable.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private int fadeOut;

        public GameFinishedDrawer(Minijam32 game)
        {
            screen = new Pixel(game.GraphicsDevice);
            fadeOut = 0;
        }

        public void DrawGameCompletedScene(SpriteBatch batch)
        {
            screen.Draw(batch, new Color(21, 15, 10, fadeOut), Vector2.Zero, new Vector2(Minijam32.ScaledWidth, Minijam32.ScaledHeight));

            if (fadeOut < 255)
            {
                fadeOut++;
            }
            else
            {
                InfoDrawer.DrawCoinsGui(batch, centered: true);
            }
        }
    }
}
