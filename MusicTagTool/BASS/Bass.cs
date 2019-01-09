using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.Misc;

namespace TotalTagger.BASS
{
    class BassLibrary
    {
        public static void InitSoundDevice()
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, TotalTagger.MainWindow.theHandle);
        }

        public static void UnloadAudioFile()
        {
            StopPlayer();
            Bass.BASS_Free();
        }

        public static void LoadAudioFile(string filename)
        {
            Bass.BASS_ChannelStop(TotalTagger.MainWindow.GetStreamHandle);
            int stream = -1;
            if (!LoadStream(filename, out stream, BASSFlag.BASS_DEFAULT | BASSFlag.BASS_SAMPLE_SOFTWARE))
            {
                TotalTagger.MainWindow.streamLoaded = false;
                return;
            }

            TotalTagger.MainWindow.streamLoaded = true;
            TotalTagger.MainWindow.GetStreamHandle = stream;

        }
        public static void Play()
        {
            Bass.BASS_ChannelPlay(TotalTagger.MainWindow.GetStreamHandle, !IsPaused());
            TotalTagger.MainWindow.timeListenedTracker.Start();
        }

        public static void StopPlayer()
        {
            if (!TotalTagger.MainWindow.streamLoaded)
                return;

            Bass.BASS_ChannelStop(TotalTagger.MainWindow.GetStreamHandle);
            TotalTagger.MainWindow.timeListenedTracker.Stop();
            SetPosition(0);
        }

        public static void PausePlayer()
        {
            if (IsPlaying())
            {
                Bass.BASS_ChannelPause(TotalTagger.MainWindow.GetStreamHandle);
                TotalTagger.MainWindow.timeListenedTracker.Stop();
            }
        }

        public static bool LoadStream(string filename, out int outStream, BASSFlag flags)
        {
            outStream = Bass.BASS_StreamCreateFile(filename, 0, 0, flags);
            return true;
        }

        public static long GetPosition()
        {
            return Bass.BASS_ChannelGetPosition(TotalTagger.MainWindow.GetStreamHandle);
        }

        public static void SetPosition(long pos)
        {
            Bass.BASS_ChannelSetPosition(TotalTagger.MainWindow.GetStreamHandle, pos);
        }

        public static long GetLength()
        {
            return Bass.BASS_ChannelGetLength(TotalTagger.MainWindow.GetStreamHandle);
        }


        public static bool IsPlaying()
        {
            return Bass.BASS_ChannelIsActive(TotalTagger.MainWindow.GetStreamHandle) == BASSActive.BASS_ACTIVE_PLAYING;
        }


        public static bool IsPaused()
        {
            return Bass.BASS_ChannelIsActive(TotalTagger.MainWindow.GetStreamHandle) == BASSActive.BASS_ACTIVE_PAUSED;
        }

        public static double ChannelBytes2Seconds(long value)
        {
            return Bass.BASS_ChannelBytes2Seconds(TotalTagger.MainWindow.GetStreamHandle, value);
        }

    }
}
