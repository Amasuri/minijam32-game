using BPO.Minijam32;
using BPO.Minijam32.GraphicsBase;
using BPO.Minijam32.GUI.Level;
using BPO.Minijam32.Music;
using BPO.Minijam32.Player;
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
        public enum ScreenState { Start, Playing, EndGame,
            SwitchingLevel
        }
        public ScreenState screenState { get; private set; }

        private MouseState _mouse;
        private MouseState _oldMouse;
        private KeyboardState _key;
        private KeyboardState _oldKey;

        private Color backgroundDirtColor;

        private const float NewLevelDelay = 2000f;
        private float currentNewLevelDelayLeft;
        public bool IsHavingNewLevelOverlay => ( currentNewLevelDelayLeft >= 0f );

        private NewLevelDrawer newLevelDrawer;

        public ScreenPool(Minijam32 game)
        {
            this.screenState = ScreenState.Playing;
            this.backgroundDirtColor = new Color(104, 76, 60);

            currentNewLevelDelayLeft = 0f;

            newLevelDrawer = new NewLevelDrawer(game);
        }

        /// <summary>
        /// Main draw cycle. Calls other drawers.
        /// </summary>
        public void CallDraws(Minijam32 game, SpriteBatch batch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(backgroundDirtColor);
            batch.Begin(samplerState: SamplerState.PointClamp);

            if (screenState == ScreenState.Start)
            {
            }
            else if (screenState == ScreenState.Playing)
            {
                game.levelData.DrawBelow(game, batch);
                PlayerDrawer.DrawCurrentState(batch, PlayerDataManager.tilePosition);
                game.levelData.DrawAbove(game, batch);
                Animator.DrawFieldAnimations(batch);

                InfoDrawer.Draw(batch);
            }
            else if (screenState == ScreenState.EndGame)
            {
            }
            else if (screenState == ScreenState.SwitchingLevel)
            {
                game.levelData.DrawBelow(game, batch);
                PlayerDrawer.DrawCurrentState(batch, PlayerDataManager.tilePosition);
                game.levelData.DrawAbove(game, batch);
                Animator.DrawFieldAnimations(batch);

                this.newLevelDrawer.Draw(batch);
            }

            batch.End();
        }

        public void CallGuiControlUpdates(Minijam32 game)
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
            else if (screenState == ScreenState.SwitchingLevel)
            {
                this.currentNewLevelDelayLeft -= Minijam32.DeltaUpdate;
                if (currentNewLevelDelayLeft <= 0f)
                {
                    screenState = ScreenState.Playing;
                    game.musicPlayer.Unmute();
                }
            }

            this._oldKey = this._key;
            this._oldMouse = this._mouse;
        }

        public void StartNewLevelDelay(MusicPlayer music)
        {
            currentNewLevelDelayLeft = NewLevelDelay;
            screenState = ScreenState.SwitchingLevel;

            music.Mute();
            SoundPlayer.PlaySound(SoundPlayer.Type.NextLevelLick);
        }
    }
}
