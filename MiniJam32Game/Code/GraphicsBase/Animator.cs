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

        static private Dictionary<Point, Animation> heartAnimations;

        static private Texture2D bombSheet;
        static private Texture2D heartSheet;

        static public void InitAssets(Minijam32 game)
        {
            bombSheet = game.Content.Load<Texture2D>("res/tile/exp");
            heartSheet = game.Content.Load<Texture2D>("res/tile/heart");

            finiteFieldAnimations = new List<Animation> { };
            heartAnimations = new Dictionary <Point, Animation> { };
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

        static public void AddHeartAnimation(Point location)
        {
            if (heartAnimations.ContainsKey(location))
                return;

            Animation newAnim = new Animation(
                null, "", 16, Minijam32.Scale, 200,
                x: (int)(location.X * TileData.ScaledTileSize.X),
                y: (int)(location.Y * TileData.ScaledTileSize.Y),
                sheet: heartSheet);

            newAnim.EnableDrawing(isALoop: true);

            heartAnimations.Add(location, newAnim);
        }

        static public void RemoveHeart(Point location)
        {
            if (heartAnimations.ContainsKey(location))
                heartAnimations.Remove(location);
        }

        static public void DrawFieldAnimations(SpriteBatch batch)
        {
            foreach (var animation in finiteFieldAnimations)
            {
                animation.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw);
            }

            foreach (var animation in heartAnimations)
            {
                animation.Value.Draw(batch, SpriteEffects.None, Minijam32.DeltaDraw);
            }
        }

        public static void ClearAnimations()
        {
            finiteFieldAnimations = new List<Animation> { };
            heartAnimations = new Dictionary<Point, Animation> { };
        }
    }
}
