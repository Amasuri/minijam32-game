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
            DrawHpGui(batch);
            DrawCoinsGui(batch);
        }

        public static void DrawCoinsGui(SpriteBatch batch, bool centered = false)
        {
            Vector2 offset = Vector2.Zero;
            if(centered)
            {
                //offset = new Vector2(Minijam32.ScaledWidth - 6 * 2 * Minijam32.Scale, Minijam32.ScaledHeight - 8 * Minijam32.Scale) / 2;
                offset = new Vector2(183, 110) * Minijam32.Scale;
            }

            int coinsLessTens = PlayerDataManager.coins % 10;
            int coinsTens = PlayerDataManager.coins / 10;

            if (offset == Vector2.Zero)
            {
                batch.Draw(numbersSheet, new Vector2(289, 25) * Minijam32.Scale, new Rectangle(6 * coinsLessTens, 0, 6, 8), normalColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
                batch.Draw(numbersSheet, new Vector2(282, 25) * Minijam32.Scale, new Rectangle(6 * coinsTens, 0, 6, 8), normalColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
            }
            else
            {
                batch.Draw(numbersSheet, offset + new Vector2(7, 0) * Minijam32.Scale, new Rectangle(6 * coinsLessTens, 0, 6, 8), normalColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
                batch.Draw(numbersSheet, offset, new Rectangle(6 * coinsTens, 0, 6, 8), normalColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
            }
        }

        private static void DrawHpGui(SpriteBatch batch)
        {
            batch.Draw(playerHpGui, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);

            Color drawColor = normalColor;
            int playerHp = PlayerDataManager.currentHP;
            if (playerHp == 2)
                drawColor = dangerColor;
            else if (playerHp < 2)
                drawColor = deathColor;

            batch.Draw(numbersSheet, new Vector2(35, 25) * Minijam32.Scale, new Rectangle(0, 0, 6, 8), drawColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
            batch.Draw(numbersSheet, new Vector2(42, 25) * Minijam32.Scale, new Rectangle(6 * playerHp, 0, 6, 8), drawColor, 0.0f, Vector2.Zero, Minijam32.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
