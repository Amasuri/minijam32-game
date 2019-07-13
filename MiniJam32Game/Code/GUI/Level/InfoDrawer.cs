using BPO.Minijam32.Player;
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

        static private Color normalColor;
        static private Color dangerColor;
        static private Color deathColor;

        static public void LoadAssets(Minijam32 game)
        {
            playerHpGui = game.Content.Load<Texture2D>("res/gui/hp_bar");
            numbersSheet = game.Content.Load<Texture2D>("res/gui/numbers");

            normalColor = new Color(238, 216, 261);
            dangerColor = new Color(165, 140, 39);
            deathColor = new Color(239, 58, 12);
        }

        static public void Draw(SpriteBatch batch)
        {
            batch.Draw(playerHpGui, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);

            Color drawColor = normalColor;
            int playerHp = PlayerDataManager.currentHP;
            if (playerHp == 2)
                drawColor = dangerColor;
            else if (playerHp < 2)
                drawColor = deathColor;

            batch.Draw(numbersSheet, new Vector2(34, 25) * Minijam32.Scale, new Rectangle(0, 0, 6, 8), drawColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
            batch.Draw(numbersSheet, new Vector2(42, 25) * Minijam32.Scale, new Rectangle(6 * playerHp, 0, 6, 8), drawColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
