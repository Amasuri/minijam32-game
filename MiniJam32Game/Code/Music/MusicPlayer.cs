using Amasuri.Reusable.Graphics;
using BPO.Minijam32.Player;
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
            upbeat1,
            upbeat2,
            upbeat3,
        }

        private SongType currentSong;
        private Dictionary<SongType, Song> songs;

        private bool isMuted = false;

        public MusicPlayer(Game game )
        {
            this.songs = new Dictionary<SongType, Song>
            {
                { SongType.upbeat1, game.Content.Load<Song>("res/music/cwby1") },
                { SongType.upbeat2, game.Content.Load<Song>("res/music/cwby2") },
                { SongType.upbeat3, game.Content.Load<Song>("res/music/cwby4") },
            };

            this.currentSong = (SongType)(-1);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.75f;
        }

        public void Update(Minijam32 game)
        {
            if(!isMuted && PlayerDataManager.isDead)
            {
                isMuted = true;
                MediaPlayer.Stop();

                SoundPlayer.PlaySound(SoundPlayer.Type.GameOverLick);
            }

            if (game.screenPool.screenState == ScreenPool.ScreenState.Playing)
            {
                SongType shouldBePlaying = (SongType)(((game.levelData.currentLevelId - 1) % 3));

                if (shouldBePlaying != this.currentSong)
                {
                    this.currentSong = shouldBePlaying;
                    MediaPlayer.Play(this.songs[this.currentSong]);
                }
            }
        }

        public void Unmute()
        {
            this.isMuted = false;
        }

        public void Mute()
        {
            this.isMuted = true;
            MediaPlayer.Stop();
        }
    }
}
