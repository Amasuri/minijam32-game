using BPO.Minijam32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Amasuri.Reusable.Graphics
{
    /// <summary>
    /// Doesn't draw anything on it's own, but calls scene drawers based on logic.
    /// Same with interface input handling.
    /// </summary>
    public interface IDrawArranger
    {
        void CallDraws(Minijam32 game, SpriteBatch defaultSpriteBatch, GraphicsDevice graphicsDevice);

        void CallGuiControlUpdates(Minijam32 game);
    }
}
