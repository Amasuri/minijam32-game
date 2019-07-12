using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Amasuri.Reusable.Audio
{
    /// <summary>
    /// Phoenix Wright-like pixelated voice player.
    /// Is used to be played repeatedly along with text, pitch-shifted to fit male/female/young/old chars.
    /// I tried to design it in more reusable way.
    /// </summary>
    public class VoicePlayer
    {
        /// <summary>
        /// Smallest playable *bipt*.
        /// </summary>
        private readonly SoundEffect soundBit;

        private const float YoungFemale = 0.7f;
        private const float LowerFemale = 0.5f;
        private const float YoungMale = 0.2f;
        private const float BassyMale = -0.3f;

        public const float maxMsSinceLastBitPaceNormal = 100.0f; //create various speeds later?
        public const float maxMsSinceLastBitPaceSlow = 150.0f;

        private float msSinceLastBit;

        public VoicePlayer(Game game, string bleepSoundPath)
        {
            soundBit = game.Content.Load<SoundEffect>(bleepSoundPath);
            msSinceLastBit = 0.0f;

            VoicedTextUpdated += this.Play;
        }

        private void Play(object sender, VoicedTextUpdatedEventArgs e)
        {
            this.TryPlayIndividualSound(e.delta, e.pace, e.actor);
        }

        private void TryPlayIndividualSound(float delta, float maxMsSinceLastBit, char actor)
        {
            msSinceLastBit += delta;

            if (msSinceLastBit >= maxMsSinceLastBit)
            {
                msSinceLastBit = 0.0f;
                soundBit.Play(1.0f, GetPitchByActor(actor), 0.0f);
            }
        }

        private static float GetPitchByActor(char actor)
        {
            if (actor == 'H' || actor == 'F')
                return YoungFemale;
            else if (actor == 'A')
                return YoungMale;
            else if (actor == 'R')
                return LowerFemale;
            else
                return BassyMale;
        }

        public static void OnVoicedText(VoicedTextUpdatedEventArgs e)
        {
            EventHandler<VoicedTextUpdatedEventArgs> handler = VoicedTextUpdated;
            handler(null, e);
        }

        public static event EventHandler<VoicedTextUpdatedEventArgs> VoicedTextUpdated;

        public class VoicedTextUpdatedEventArgs : EventArgs
        {
            public float delta = 17.0f; //default value in case no provided/forgotten, but this is to be overridden by caller
            public float pace = maxMsSinceLastBitPaceNormal;
            public char actor = 'n';
        }
    }
}
