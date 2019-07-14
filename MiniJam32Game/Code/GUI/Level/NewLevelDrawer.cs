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
    public class NewLevelDrawer
    {
        private Pixel screen;
        private int deathAlpha;

        public NewLevelDrawer(Minijam32 game)
        {
            screen = new Pixel(game.GraphicsDevice);
            deathAlpha = 0;
        }

        public void DrawNextLevelIntro(SpriteBatch batch)
        {
            screen.Draw(batch, new Color(21, 15, 10, 255), Vector2.Zero, new Vector2(Minijam32.ScaledWidth, Minijam32.ScaledHeight));
        }

        public void DrawDeathScene(SpriteBatch batch)
        {
            screen.Draw(batch, new Color(21, 15, 10, deathAlpha), Vector2.Zero, new Vector2(Minijam32.ScaledWidth, Minijam32.ScaledHeight));

            if (deathAlpha < 255)
                deathAlpha++;
        }
    }
}
