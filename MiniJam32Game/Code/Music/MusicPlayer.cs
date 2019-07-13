using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace BPO.Minijam32.Music
{
    /// <summary>
    /// A kind of updater, which takes major screen state changes to change music it's playing.
    ///
    /// Is not static because we don't need it to be called from anywhere.
    /// </summary>
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
