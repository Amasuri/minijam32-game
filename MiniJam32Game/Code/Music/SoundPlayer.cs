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
        }

        static private Dictionary<Type, SoundEffect> sounds;

        static public void InitAssets(Game game)
        {
            sounds = new Dictionary<Type, SoundEffect>
            {
            };
        }

        static public void PlaySound(Type type)
        {
            if (sounds.ContainsKey(type))
                sounds[type].Play();
        }
    }
}
