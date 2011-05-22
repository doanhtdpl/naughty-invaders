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
    class EditorState_EditEffect : EditorState
    {
        ParticleSystem selected = null;
        float selectedDelta;

        public EditorState_EditEffect()
            : base()
        {
        }

        public override void enter()
        {
            base.enter();
        }

        public override void update()
        {
            base.update();

            if (justPressedLeftButton() && isPosInScreen(gameScreenPos)) //&& (Math.Abs(lastMouseState.X - mouseState.X) > 2 || Math.Abs(lastMouseState.Y - mouseState.Y) > 2))
            {
                selected = null;
                foreach (ParticleSystem particle in ParticleManager.Instance.getParticles())
                {
                    float delta = (getMousePosInZ(particle.position.Z) - particle.position).Length();
                    if (delta < 25 && (selected == null || delta < selectedDelta))
                    {
                        selected = particle;
                        selectedDelta = delta;
                    }
                }

                if (selected != null)
                {
                    MyEditor.Instance.effectDirectionX.Text = selected.direction.X.toString();
                    MyEditor.Instance.effectDirectionY.Text = selected.direction.Y.toString();
                    MyEditor.Instance.effectDirectionZ.Text = selected.direction.Z.toString();
                    MyEditor.Instance.effectColorR.Text = selected.data.color.R.ToString();
                    MyEditor.Instance.effectColorG.Text = selected.data.color.G.ToString();
                    MyEditor.Instance.effectColorB.Text = selected.data.color.B.ToString();

                    MyEditor.Instance.effectScale.Text = "0";
                    MyEditor.Instance.effectNumParticles.Text = "0";
                    MyEditor.Instance.effectLifetime.Text = "0";
                }
            }
            else if(selected != null && mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 current = new Vector2(mouseState.X, mouseState.Y);
                Vector2 last = new Vector2(lastMouseState.X, lastMouseState.Y);
                Vector3 currentZ = EditorHelper.Instance.getMousePosInZ(current, selected.position.Z);
                Vector3 lastZ = EditorHelper.Instance.getMousePosInZ(last, selected.position.Z);

                selected.position += (currentZ - lastZ);
            }
            else if (selected != null && mouseState.RightButton == ButtonState.Pressed)
            {
                selected.position += new Vector3(0, 0, -(mouseState.Y - lastMouseState.Y));
            }
        }

        public void changeColor(Color color)
        {
            if (selected != null)
            {
                selected.data.color = color;
            }
        }

        public void updateEffect(Vector3 direction, float scale, int numParticles, float lifetime)
        {
            if (selected != null)
            {
                selected.direction = direction;
            }
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            foreach(ParticleSystem particle in ParticleManager.Instance.getParticles())
            {
                if(selected != null && particle == selected)
                    DebugManager.Instance.addRectangle(particle.position - new Vector3(20, 20, 0), particle.position + new Vector3(20, 20, 0), Color.Red);
                else
                    DebugManager.Instance.addRectangle(particle.position - new Vector3(20, 20, 0), particle.position + new Vector3(20, 20, 0), Color.Yellow);
            }
            ParticleManager.Instance.update();
            ParticleManager.Instance.render();
            base.render();
        }
    }
}
#endif