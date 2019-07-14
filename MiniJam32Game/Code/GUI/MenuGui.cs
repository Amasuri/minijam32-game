using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.Code.GUI
{
    public class MenuGui : ADrawingClass
    {
        private Texture2D menuButtons;
        private Texture2D menuBackground;
        private Texture2D menuPressed;

                                            //129 124
        private Vector2 UnscaledPlayCoords; //129 101
        private Vector2 UnscaledExitCoords; //129 147

        private int pressedButton = 0;

        public MenuGui(Minijam32 game)
        {
            menuButtons = game.Content.Load<Texture2D>("res/gui/menu");
            menuBackground = game.Content.Load<Texture2D>("res/gui/menu_background");
            menuPressed = game.Content.Load<Texture2D>("res/gui/menu_selected");

            UnscaledPlayCoords = new Vector2(129, 101);
            UnscaledExitCoords = new Vector2(129, 147);
        }

        public override void Draw(Minijam32 game, SpriteBatch spriteBatch)
        {
            DrawBackground(spriteBatch);

            this.DrawTexture(spriteBatch, this.menuButtons, Vector2.Zero, Minijam32.Scale);

            if (pressedButton == 0)
                this.DrawTexture(spriteBatch, menuPressed, UnscaledPlayCoords * Minijam32.Scale, Minijam32.Scale);
            if (pressedButton == 1)
                this.DrawTexture(spriteBatch, menuPressed, UnscaledExitCoords * Minijam32.Scale, Minijam32.Scale);
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Minijam32.UnscaledWidth / menuBackground.Width + 1; x++)
                for (int y = 0; y < Minijam32.UnscaledHeight / menuBackground.Height + 1; y++)
                {
                    this.DrawTexture(spriteBatch, this.menuBackground, new Vector2(x * Minijam32.Scale * menuBackground.Width, y * Minijam32.Scale * menuBackground.Height), Minijam32.Scale);
                }
        }

        public override void Update(Minijam32 game, MouseState mouse, MouseState oldMouse, KeyboardState keys, KeyboardState oldKeys)
        {
            if ((keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W)) || (keys.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up)))
            {
                pressedButton--;
                SoundPlayer.PlaySound(SoundPlayer.Type.MenuSwitch);
            }
            else if ((keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S)) || (keys.IsKeyDown(Keys.Down) && oldKeys.IsKeyUp(Keys.Down)))
            {
                pressedButton++;
                SoundPlayer.PlaySound(SoundPlayer.Type.MenuSwitch);
            }

            if (pressedButton < 0)
                pressedButton = 0;
            if (pressedButton > 1)
                pressedButton = 1;

            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
            {
                game.screenPool.TriggerGameStart();
                SoundPlayer.PlaySound(SoundPlayer.Type.MenuConfirm);
            }
        }
    }
}
