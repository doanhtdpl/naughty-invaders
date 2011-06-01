using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace MyGame
{
    class SoundManagerOLD
    {
/*        static AudioEngine engine;
        static SoundBank soundBank;
        static WaveBank waveBank;

        static Cue music = null;
        //static string currentSong = null;
        static Cue sound;

        static AudioCategory musicCat;
        static AudioCategory soundCat;

        public static void initialize()
        {
            engine = new AudioEngine("Content/sound/SCsound.xgs");
            soundBank = new SoundBank(engine, "Content/sound/Sound Bank.xsb");
            waveBank = new WaveBank(engine, "Content/sound/Wave Bank.xwb");

            musicCat = engine.GetCategory("Music");
            soundCat = engine.GetCategory("Sound");
        }

        public static void update()
        {
            engine.Update();
        }

        public static void dispose()
        {
            engine.Dispose();
            soundBank.Dispose();
            waveBank.Dispose();
        }

        public static void play(string songName)
        {
#if EDITOR
            return;
#else
            //if (currentSong == songName && !music.IsStopped)
            //    return;
            //if (music != null)
            //    stopMusic();
            //currentSong = songName;
            ////if (music != null)
            ////    music.Stop(AudioStopOptions.Immediate);
            //music = soundBank.GetCue(songName);
            //music.Play();
#endif
        }
        public static void playSound(string soundName)
        {
            try
            {
                sound = soundBank.GetCue(soundName);
                sound.Play();
            }
            catch (Exception) { }
        }

        public static void stopSound(string soundName)
        {
            sound = soundBank.GetCue(soundName);
            sound.Stop(AudioStopOptions.Immediate);
        }

        public static void stopMusic()
        {
            if (music != null)
            {
                music.Stop(AudioStopOptions.Immediate);
            }
        }

        public static void updateMusicVolume(float newVolume)
        {
            //GamerManager.getGamerEntities()[0].saveData.musicLevel = newVolume;
            //musicCat.SetVolume(GamerManager.getMainControls().saveData.musicLevel / 100f);
        }
        public static void updateSoundVolume(float newVolume)
        {
            //GamerManager.getMainControls().saveData.soundLevel = newVolume;
            //soundCat.SetVolume(GamerManager.getMainControls().saveData.soundLevel / 100f);
        }
        public static void initVolumes()
        {
            //musicCat.SetVolume(GamerManager.getMainControls().saveData.musicLevel / 100f);
            //soundCat.SetVolume(GamerManager.getMainControls().saveData.soundLevel / 100f);
        }*/
    }
}