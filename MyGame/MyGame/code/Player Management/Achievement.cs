using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MyGame
{
    public class AchievementManager
    {
        static List<RenderAchievement> toRenderAchievements = new List<RenderAchievement>();

        static int renderPositionY = +310;
        public const int SUPER_SCORE = 40000;
        public const int ACHIEVEMENT_TIME = 5000;
        public const int FADEIN_TIME = 1000;
        public const int FADEOUT_TIME = 1000;

        public static void updateAchievements()
        {
        }

        public static void addAchievementToRender(Achievement achievement)
        {
            //SaveGameManager.saveGame();
            toRenderAchievements.Add(new RenderAchievement(0, achievement));
        }

        public static void renderAchievements()
        {
            if (toRenderAchievements.Count > 0)
            {
                float alpha;
                float time = toRenderAchievements[0].time;
                if (time < FADEIN_TIME)
                {
                    alpha = time / (float)FADEIN_TIME;
                }
                else if (time > ACHIEVEMENT_TIME - FADEOUT_TIME)
                {
                    float timeInFadeOut = time - (ACHIEVEMENT_TIME - FADEOUT_TIME);
                    alpha = 1 - (timeInFadeOut / (float)FADEOUT_TIME);
                }
                else
                {
                    alpha = 1;
                }
                string str = "You got an imaginary achievement!";
                GraphicsManager.Instance.spriteBatch.Begin();
                str.renderSC(new Vector2(0, renderPositionY), 1.0f,
                    Color.White, Color.Black, StringManager.tTextAlignment.Centered);
                str = toRenderAchievements[0].achievement.message + " - " + toRenderAchievements[0].achievement.points.ToString() + " imaginary points";
                str.renderSC(new Vector2(0, renderPositionY -50), 0.9f,
                    Color.White, Color.Black, StringManager.tTextAlignment.Centered);
                GraphicsManager.Instance.spriteBatch.End();
            }
        }
    }

    public class RenderAchievement
    {
        public int time;
        public Achievement achievement;

        public RenderAchievement(int time, Achievement achievement)
        {
            this.time = time;
            this.achievement = achievement;
        }
    }

    public struct Achievement
    {
        public string name;
        public string message;
        public int points;

        public Achievement(string name, string message, int points)
        {
            this.name = name;
            this.message = message;
            this.points = points;
        }
    }
}
