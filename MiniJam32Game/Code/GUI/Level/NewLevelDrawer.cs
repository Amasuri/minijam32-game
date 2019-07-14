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

        public NewLevelDrawer(Minijam32 game)
        {
            screen = new Pixel(game.GraphicsDevice);
        }

        public void Draw(SpriteBatch batch)
        {
            screen.Draw(batch, new Color(0, 0, 0, 255), Vector2.Zero, new Vector2(Minijam32.ScaledWidth, Minijam32.ScaledHeight));
        }
    }
}
