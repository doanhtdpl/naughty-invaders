using System;

#if EDITOR
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
#endif

namespace MyGame
{
#if WINDOWS || XBOX
    static class Program
    {
#if EDITOR
        [STAThread]
#endif
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if EDITOR
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyEditor());
#else
            using (Game game = new Game())
            {
                game.Run();
            }
#endif
        }

#if EDITOR
        public static List<string> LoadContent(this ContentManager contentManager, string contentFolder)
        {
            //Load directory info, abort if none
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "\\" + contentFolder);
            if (!dir.Exists)
                throw new DirectoryNotFoundException();
            //Init the resulting list
            List<string> result = new List<string>();

            //Load all files that matches the file filter
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                //result.Add(contentFolder + "/" + key);
                result.Add(key);
            }
            //Return the result
            return result;
        }
#endif
    }
#endif
}