using System;

#if EDITOR
using System.Windows.Forms;
#endif

namespace MyGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if EDITOR
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NaughtyEditor());
#else
            using (Game game = new Game())
            {
                game.Run();
            }
#endif
        }
    }
#endif
}

