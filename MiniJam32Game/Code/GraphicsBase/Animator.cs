using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Level.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPO.Minijam32.GraphicsBase
{
    static public class Animator
    {
        static private List<Animation> finiteFieldAnimations;

        static private Texture2D bombSheet;

        static public void InitAssets(Minijam32 game)
        {
            bombSheet = game.Content.Load<Texture2D>("res/tile/exp");

            finiteFieldAnimations = new List<Animation> { };
        }

        static public void NewBombAnimation(Point location)
        {
            Animation newAnim = new Animation(
                null, "", 48, Minijam32.Scale, 50,
                x: (int)(location.X * TileData.ScaledTileSize.X),
                y: (int)(location.Y * TileData.ScaledTileSize.Y),
                sheet: bombSheet);

            newAnim.EnableDrawing(isALoop: false);

            finiteFieldAnimations.Add( newAnim );
        }

        static public void DrawFiniteFieldAnimations(SpriteBatch batch)
        {
            foreach (var animation in finiteFieldAnimations)
            {
                animation.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw);
            }
        }
    }
}
