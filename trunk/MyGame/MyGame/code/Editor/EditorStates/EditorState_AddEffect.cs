#if EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class EditorState_AddEffect : EditorState
    {
        public EditorState_AddEffect()
            : base()
        {
        }

        public override void enter()
        {
            base.enter();
            List<string> items = ParticleManager.Instance.getBaseParticleSystemNames();
            MyEditor.Instance.effectsCombo.Items.Clear();
            foreach (string name in items)
                MyEditor.Instance.effectsCombo.Items.Add(name);
        }

        public override void update()
        {
            if (!MyEditor.Instance.effectsCombo.Focused && justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                ParticleManager.Instance.addParticles(MyEditor.Instance.effectsCombo.Text, getMousePosInZ(), Vector3.Zero, Color.White);
            }
            base.update();
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            foreach (ParticleSystem particle in ParticleManager.Instance.getParticles())
            {
                DebugManager.Instance.addRectangle(particle.position - new Vector3(20, 20, 0), particle.position + new Vector3(20, 20, 0), Color.Yellow);
            }
            ParticleManager.Instance.update();
            ParticleManager.Instance.render();
            base.render();
        }
    }
}
#endif