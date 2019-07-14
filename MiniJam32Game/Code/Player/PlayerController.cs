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

            if (keyState.IsKeyDown( keyUp ))
                PlayerDataManager.TryMove(game.levelData, new Point(0, -1));
            else if (keyState.IsKeyDown(keyDown))
                PlayerDataManager.TryMove(game.levelData, new Point(0, +1));
            else if (keyState.IsKeyDown(keyLeft))
                PlayerDataManager.TryMove(game.levelData, new Point(-1, 0));
            else if (keyState.IsKeyDown(keyRight))
                PlayerDataManager.TryMove(game.levelData, new Point(1, 0));

            if (OneKeyPress( keyBomb ))
                game.levelData.TryPlantBombAt(PlayerDataManager.tilePosition);

            oldKeyState = keyState;
        }

        static private bool OneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
