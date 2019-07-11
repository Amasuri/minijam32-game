using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace BPO.Minijam32.Music
{
    public class SoundPlayer
    {
        public enum Type
        {
        }

        private Dictionary<Type, SoundEffect> sounds;

        public SoundPlayer(Game game)
        {
        }

        public void PlaySound(Type type)
        {
            if (sounds.ContainsKey(type))
                sounds[type].Play();
        }
    }
}
