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

        private const Keys keyUp_a = Keys.Up;
        private const Keys keyDown_a = Keys.Down;
        private const Keys keyLeft_a = Keys.Left;
        private const Keys keyRight_a = Keys.Right;

        private const Keys keyBomb = Keys.Space;

        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        static public void UpdateMovement(Minijam32 game, KeyboardState keys, KeyboardState oldKeys)
        {
            keyState = keys;
            oldKeyState = oldKeys;

            //On death, no controls
            if (PlayerDataManager.isDead)
                return;

            if (OneKeyPress(keyUp ) || OneKeyPress(keyUp_a))
                PlayerDataManager.TryMove(game.levelData, new Point(0, -1));
            else if (OneKeyPress(keyDown) || OneKeyPress(keyDown_a))
                PlayerDataManager.TryMove(game.levelData, new Point(0, +1));
            else if (OneKeyPress(keyLeft) || OneKeyPress(keyLeft_a))
                PlayerDataManager.TryMove(game.levelData, new Point(-1, 0));
            else if (OneKeyPress(keyRight) || OneKeyPress(keyRight_a))
                PlayerDataManager.TryMove(game.levelData, new Point(1, 0));

#if DEBUG
            if (OneKeyPress(Keys.D1))
                game.levelData.DebugSetLevel(1);
            if (OneKeyPress(Keys.D2))
                game.levelData.DebugSetLevel(2);
            if (OneKeyPress(Keys.D3))
                game.levelData.DebugSetLevel(3);
            if (OneKeyPress(Keys.D4))
                game.levelData.DebugSetLevel(4);
            if (OneKeyPress(Keys.D5))
                game.levelData.DebugSetLevel(5);
            if (OneKeyPress(Keys.D6))
                game.levelData.DebugSetLevel(6);
            if (OneKeyPress(Keys.D7))
                game.levelData.DebugSetLevel(7);
            if (OneKeyPress(Keys.D8))
                game.levelData.DebugSetLevel(8);
            if (OneKeyPress(Keys.D9))
                game.levelData.DebugSetLevel(9);
            if (OneKeyPress(Keys.D0))
                game.levelData.DebugSetLevel(10);
#endif

            if (OneKeyPress( keyBomb ))
                game.levelData.TryPlantBombAt(PlayerDataManager.tilePosition);
        }

        static private bool OneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
