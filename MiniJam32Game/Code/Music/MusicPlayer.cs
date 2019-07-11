using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace BPO.Minijam32.Music
{
    public class MusicPlayer
    {
        public enum SongType
        {
        }

        private SongType currentSong;
        private Dictionary<SongType, Song> songs;

        public MusicPlayer(Game game )
        {
        }

        public void Update(Minijam32 game)
        {
        }
    }
}
