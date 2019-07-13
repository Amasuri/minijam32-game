using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.GUI.Level
{
    static public class InfoDrawer
    {
        static private Texture2D playerHpGui;
        static private Texture2D numbersSheet; //6*8 size

        static public void LoadAssets(Minijam32 game)
        {
            playerHpGui = game.Content.Load<Texture2D>("res/gui/hp_bar");
            numbersSheet = game.Content.Load<Texture2D>("res/gui/numbers");
        }

        static public void Draw(SpriteBatch batch)
        {
            batch.Draw(playerHpGui, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
