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

            if (OneKeyPress(keyUp ))
                PlayerDataManager.TryMove(game.levelData, new Point(0, -1));
            else if (OneKeyPress(keyDown))
                PlayerDataManager.TryMove(game.levelData, new Point(0, +1));
            else if (OneKeyPress(keyLeft))
                PlayerDataManager.TryMove(game.levelData, new Point(-1, 0));
            else if (OneKeyPress(keyRight))
                PlayerDataManager.TryMove(game.levelData, new Point(1, 0));

#if DEBUG
            if (OneKeyPress(Keys.D1))
                game.levelData.ReInitializeLevelData(1);
            if (OneKeyPress(Keys.D2))
                game.levelData.ReInitializeLevelData(2);
            if (OneKeyPress(Keys.D3))
                game.levelData.ReInitializeLevelData(3);
            if (OneKeyPress(Keys.D4))
                game.levelData.ReInitializeLevelData(4);
            if (OneKeyPress(Keys.D5))
                game.levelData.ReInitializeLevelData(5);
            if (OneKeyPress(Keys.D6))
                game.levelData.ReInitializeLevelData(6);
            if (OneKeyPress(Keys.D7))
                game.levelData.ReInitializeLevelData(7);
            if (OneKeyPress(Keys.D8))
                game.levelData.ReInitializeLevelData(8);
            if (OneKeyPress(Keys.D9))
                game.levelData.ReInitializeLevelData(9);
            if (OneKeyPress(Keys.D0))
                game.levelData.ReInitializeLevelData(10);
#endif

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
