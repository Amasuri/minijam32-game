using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace BPO.Minijam32.Music
{
    /// <summary>
    /// It's <see langword="static"/>, hence callable from anywhere.
    ///
    /// In proper games sound events are realized via <see langword="event"/> mechanics, but
    /// we're jamming here, so let's just easy things up and make it callable from anywhere, on any use. You
    /// can peek usage with ctrl + k, r anyway.
    /// </summary>
    static public class SoundPlayer
    {
        public enum Type
        {
            BombExplosion,
        }

        static private Dictionary<Type, SoundEffect> sounds;

        static public void InitAssets(Game game)
        {
            sounds = new Dictionary<Type, SoundEffect>
            {
                { Type.BombExplosion, game.Content.Load<SoundEffect>("res/sound/explosion0") },
            };
        }

        static public void PlaySound(Type type)
        {
            float volume = 1.0f; //0.0f is silence, 1.0f is full volume
            float pitch = 0.0f; //-1.0f (down one octave), 1.0f (up one octave), 0.0f is normal pitch.
            float pan = 0.0f; //-1.0f (full left) to 1.0f (full right). 0.0f is centered.

            if (sounds.ContainsKey(type))
                sounds[type].Play(volume, pitch, pan);
        }
    }
}
