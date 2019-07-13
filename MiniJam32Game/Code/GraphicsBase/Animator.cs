using Amasuri.Reusable.Graphics;
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
            finiteFieldAnimations.Add(new Animation(null, "", 16, Minijam32.Scale, 50, sheet: bombSheet));
        }

        static public void DrawFiniteFieldAnimations()
        {
        }
    }
}
