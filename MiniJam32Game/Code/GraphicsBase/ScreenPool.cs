using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Amasuri.Reusable.Graphics
{
    /// <summary>
    /// Calls other draw things. Doesn't draw anything on it's own.
    /// </summary>
    public class ScreenPool : IDrawArranger
    {
        public enum ScreenState { Start, Playing, EndGame }
        public ScreenState screenState { get; private set; }

        private MouseState _mouse;
        private MouseState _oldMouse;
        private KeyboardState _key;
        private KeyboardState _oldKey;

        public ScreenPool(Game game)
        {
            this.screenState = ScreenState.Playing;
        }

        /// <summary>
        /// Main draw cycle. Calls other drawers.
        /// </summary>
        public void CallDraws(Game shaderDemo, SpriteBatch defaultSpriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.SlateGray);
            defaultSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if (screenState == ScreenState.Start)
            {
            }
            else if (screenState == ScreenState.Playing)
            {
            }
            else if (screenState == ScreenState.EndGame)
            {
            }

            defaultSpriteBatch.End();
        }

        public void CallGuiControlUpdates(Game game)
        {
            this._key = Keyboard.GetState();
            this._mouse = Mouse.GetState();

            //code
            if (screenState == ScreenState.Start)
            {
            }
            else if (screenState == ScreenState.Playing)
            {
            }
            else if (screenState == ScreenState.EndGame)
            {
            }

            this._oldKey = this._key;
            this._oldMouse = this._mouse;
        }
    }
}
