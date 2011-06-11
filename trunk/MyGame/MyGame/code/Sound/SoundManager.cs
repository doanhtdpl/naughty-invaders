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
        public void playSong(string name)
        {
#if !EDITOR
            if (songs.ContainsKey(name))
            {
                MediaPlayer.Play(songs[name]);
            }
#endif
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
                    try
                    {
                        effects.Add(name, SB.content.Load<SoundEffect>("sounds\\effects\\" + name));
                    }
                    catch (Exception e) { }
                }
            }
        }

        public void clean()
        {
            songs.Clear();
            effects.Clear();
        }
    }
}
