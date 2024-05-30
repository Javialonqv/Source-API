using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace API
{
    /// <summary>
    /// Toolkit for audio managment.
    /// </summary>
    public class AudioSource
    {
        /// <summary>
        /// The class that storages the audio to play.
        /// </summary>
        WaveOutEvent outputDevice = new WaveOutEvent();
        /// <summary>
        /// Specifies if the audio is currently playing.
        /// </summary>
        public bool isPlaying
        {
            get { return outputDevice.PlaybackState == PlaybackState.Playing; }
        }
        /// <summary>
        /// Spceifies the path to the audio file.
        /// </summary>
        public string audioPath { get; private set; } = "";
        /// <summary>
        /// Specifies the volume of the audio.
        /// </summary>
        public float volume
        {
            get { return outputDevice.Volume; }
            set
            {
                if (value >= 0 && value <= 1) { outputDevice.Volume = value; }
            }
        }
        /// <summary>
        /// Specifies if the audio should be on a loop.
        /// </summary>
        public bool loop = false;

        /// <summary>
        /// Creates a new instance of the AudioSource with a audio file.
        /// </summary>
        /// <param name="audioPath"></param>
        public AudioSource(string audioPath)
        {
            this.audioPath = audioPath;
            InitOutputDevice();
        }

        /// <summary>
        /// Inits the audio file to be played.
        /// </summary>
        void InitOutputDevice()
        {
            try
            {
                AudioFileReader reader = new AudioFileReader(audioPath);
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
                outputDevice.Init(reader);
            }
            catch
            {
                ExceptionsManager.ErrorLoadingAudioFile(audioPath);
            }
        }

        /// <summary>
        /// Plays the loaded audio file.
        /// </summary>
        public void Play()
        {
            outputDevice.Play();
        }
        /// <summary>
        /// Plays an audio file without a AudioSource class instance.
        /// </summary>
        /// <param name="audioPath"></param>
        public static void Play(string audioPath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(audioPath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                }
            }
            catch
            {
                ExceptionsManager.ErrorLoadingAudioFile(audioPath);
            }
        }

        /// <summary>
        /// Specifies what happen when the audio stop playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (loop)
            {
                InitOutputDevice(); // Restart the output device.
                Play(); // Replay the audio file.
            }
        }

        internal static bool CanCreateFrom(string audioPath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(audioPath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                }
                return true; // If no exceptions occur, return true.
            }
            catch
            {
                return false;
            }
        }
    }
}
