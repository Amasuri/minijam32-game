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

        private const Keys keyBomb = Keys.Space;

        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        static public void UpdateMovement(Minijam32 game)
        {
            keyState = Keyboard.GetState();

            //On death, no controls
            if (PlayerDataManager.isDead)
                return;

            if (oneKeyPress(keyUp))
                PlayerDataManager.Move(game.levelData, new Point(0, -1));
            else if (oneKeyPress(keyDown))
                PlayerDataManager.Move(game.levelData, new Point(0, +1));
            else if (oneKeyPress(keyLeft))
                PlayerDataManager.Move(game.levelData, new Point(-1, 0));
            else if (oneKeyPress(keyRight))
                PlayerDataManager.Move(game.levelData, new Point(1, 0));

            if (oneKeyPress(keyBomb))
                game.levelData.PlantBombAt(PlayerDataManager.tilePosition);

            oldKeyState = keyState;
        }

        static private bool oneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
