using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BPO.Minijam32.Player
{
    /// <summary>
    /// A class which takes input and produces action based on current screen and state. Must know what's going on in the game.
    ///
    /// No player information is being hold there - go to PlayerData for that.
    /// </summary>
    static public class PlayerController
    {
        private const Keys keyUp = Keys.W;
        private const Keys keyDown = Keys.S;
        private const Keys keyLeft = Keys.A;
        private const Keys keyRight = Keys.D;

        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        static public void Update(Minijam32 game)
        {
            keyState = Keyboard.GetState();

            if (oneKeyPress(keyUp))
                PlayerDataManager.Move(new Point(0, -1));
            else if (oneKeyPress(keyDown))
                PlayerDataManager.Move(new Point(0, +1));
            else if (oneKeyPress(keyLeft))
                PlayerDataManager.Move(new Point(-1, 0));
            else if (oneKeyPress(keyRight))
                PlayerDataManager.Move(new Point(1, 0));

            oldKeyState = keyState;
        }

        static private bool oneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
