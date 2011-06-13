using System;
using Microsoft.Xna.Framework;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;

namespace MyGame
{
    class SoundManager
    {
        const float STOP_FADE_DURATION = 1.0f;

        bool stopFade = false;
        float stopFadeTime;
        float stopFadeDuration;

        Song songPlaying = null;

        string songToPlay = null;
        bool loopToPlay;

        static SoundManager instance = null;
        SoundManager()
        {
            songs = new Dictionary<string, Song>();
            effects = new Dictionary<string, SoundEffect>();
        }
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }
                return instance;
            }
        }

        public Dictionary<string, Song> songs { get; set; }
        public Dictionary<string, SoundEffect> effects { get; set; }

        public void playEffect(string name)
        {
#if !EDITOR
            if (effects.ContainsKey(name))
            {
                effects[name].Play();
            }
#endif
        }
        public void playSong(string name, bool loop = false)
        {
#if !EDITOR
            if (songs.ContainsKey(name) && songPlaying != songs[name])
            {
                MediaPlayer.IsRepeating = loop;
                if (songPlaying != null)
                {
                    songToPlay = name;
                }
                else
                {
                    MediaPlayer.Play(songs[name]);
                    songPlaying = songs[name];
                }
            }
#endif
        }

        public void playWithTransition(string songToPlay, bool loopToPlay = false)
        {
            stopWithFade();
            this.songToPlay = songToPlay;
            this.loopToPlay = loopToPlay;
        }

        public void loadXML()
        {
            songs.Clear();
            effects.Clear();

            XDocument xml = XDocument.Load(SB.content.RootDirectory + "/xml/sound/audioFiles.xml");

            IEnumerable<XElement> audioFileList = xml.Descendants("audioFile");

            foreach (XElement af in audioFileList)
            {
                if (af.Attribute("type").Value == "song")
                {
                    string name = af.Attribute("name").Value;
                    songs.Add( name, SB.content.Load<Song>("sounds\\songs\\" + name));
                }
                else if (af.Attribute("type").Value == "effect")
                {
                    string name = af.Attribute("name").Value;
                    effects.Add(name, SB.content.Load<SoundEffect>("sounds\\effects\\" + name));
                }
            }
        }

        public void stopWithFade(float duration = STOP_FADE_DURATION)
        {
            stopFade = true;
            stopFadeTime = duration;
            stopFadeDuration = duration;
        }

        public void update()
        {
            if (stopFade)
            {
                stopFadeTime -= SB.dt;

                MediaPlayer.Volume = Math.Min(Math.Max(stopFadeTime / stopFadeDuration, 0), 1);

                if (stopFadeTime <= 0)
                {
                    MediaPlayer.Stop();
                    stopFade = false;
                    MediaPlayer.Volume = 1;
                    songPlaying = null;
                }
            }
            else if (songToPlay != null)
            {
                playSong(songToPlay, loopToPlay);
                songToPlay = null;
            }

            if (songPlaying != null)
            {
            }
        }

        public void clean()
        {
            songs.Clear();
            effects.Clear();
        }
    }
}
