using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Amasuri.Reusable.Graphics
{
    /// <summary>
    /// A little stretchable drawable to easily draw dots, rectangles and lines
    /// </summary>
    public class Pixel : IDisposable
    {
        private readonly Texture2D pix;

        public Pixel(GraphicsDevice graphics)
        {
            pix = new Texture2D(graphics, 1, 1);
            pix.SetData<Color>(new Color[] { Color.White });
        }

        public void Draw(SpriteBatch spriteBatch, Color color, Vector2 pos, Vector2 stretch, float scale = 1.0f)
        {
            spriteBatch.Draw
            (
                pix, new Rectangle(
                    (pos * scale).ToPoint(),
                    (stretch * scale).ToPoint()),
                    color
            );
        }

        public virtual void Dispose()
        {
            this.pix.Dispose();
        }
    }
}
