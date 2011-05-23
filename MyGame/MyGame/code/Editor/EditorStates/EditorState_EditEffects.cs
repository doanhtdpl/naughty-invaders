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
    class EditorState_EditEffects : EditorState
    {
        public string selected = null;

        public EditorState_EditEffects()
            : base()
        {
        }

        public override void enter()
        {
            base.enter();
            List<string> items = ParticleManager.Instance.getBaseParticleSystemNames();
            MyEditor.Instance.editEffectsList.Items.Clear();
            foreach (string name in items)
                MyEditor.Instance.editEffectsList.Items.Add(name);

            ParticleManager.Instance.getParticles().Clear();

            //textures
            MyEditor.Instance.ee_texture.Items.Clear();
            var textures = SB.content.LoadContent("textures/particles");
            for (int i = 0; i < textures.Count(); i++)
            {
                MyEditor.Instance.ee_texture.Items.Add(textures[i]);
            }
        }

        public override void update()
        {
            base.update();

            if (selected != null)
            {
                if (ParticleManager.Instance.getParticles().Count <= 0)
                {
                    ParticleManager.Instance.addParticles(selected, Vector3.Zero, Vector3.Zero, Color.White);
                }
            }
        }

        public override void exit()
        {
            base.exit();
        }

        public void setSelected(string name)
        {
            selected = name;
            ParticleSystemData data = ParticleManager.Instance.getBaseParticleSystemData(name);

            MyEditor.Instance.ee_texture.Text = data.textureName;
            MyEditor.Instance.ee_type.Text = data.type == ParticleSystemData.tParticleSystem.Burst ? "burst" : "fountain";
            MyEditor.Instance.ee_render.Text = data.color.A == 0 ? "additive" : "normal";

            MyEditor.Instance.ee_posX.Text = data.position.X.toString();
            MyEditor.Instance.ee_PosY.Text = data.position.Y.toString();
            MyEditor.Instance.ee_PosZ.Text = data.position.Z.toString();
            MyEditor.Instance.ee_PosMinX.Text = data.positionVarianceMin.X.toString();
            MyEditor.Instance.ee_PosMinY.Text = data.positionVarianceMin.Y.toString();
            MyEditor.Instance.ee_PosMinZ.Text = data.positionVarianceMin.Z.toString();
            MyEditor.Instance.ee_PosMaxX.Text = data.positionVarianceMax.X.toString();
            MyEditor.Instance.ee_PosMaxY.Text = data.positionVarianceMax.Y.toString();
            MyEditor.Instance.ee_PosMaxZ.Text = data.positionVarianceMax.Z.toString();

            MyEditor.Instance.ee_DirX.Text = data.direction.X.toString();
            MyEditor.Instance.ee_DirY.Text = data.direction.Y.toString();
            MyEditor.Instance.ee_DirZ.Text = data.direction.Z.toString();
            MyEditor.Instance.ee_DirMinX.Text = data.directionVarianceMin.X.toString();
            MyEditor.Instance.ee_DirMinY.Text = data.directionVarianceMin.Y.toString();
            MyEditor.Instance.ee_DirMinZ.Text = data.directionVarianceMin.Z.toString();
            MyEditor.Instance.ee_DirMaxX.Text = data.directionVarianceMax.X.toString();
            MyEditor.Instance.ee_DirMaxY.Text = data.directionVarianceMax.Y.toString();
            MyEditor.Instance.ee_DirMaxZ.Text = data.directionVarianceMax.Z.toString();

            MyEditor.Instance.ee_AccX.Text = data.acceleration.X.toString();
            MyEditor.Instance.ee_AccY.Text = data.acceleration.Y.toString();
            MyEditor.Instance.ee_AccZ.Text = data.acceleration.Z.toString();
            MyEditor.Instance.ee_AccMinX.Text = data.accelerationVarianceMin.X.toString();
            MyEditor.Instance.ee_AccMinY.Text = data.accelerationVarianceMin.Y.toString();
            MyEditor.Instance.ee_AccMinZ.Text = data.accelerationVarianceMin.Z.toString();
            MyEditor.Instance.ee_AccMaxX.Text = data.accelerationVarianceMax.X.toString();
            MyEditor.Instance.ee_AccMaxY.Text = data.accelerationVarianceMax.Y.toString();
            MyEditor.Instance.ee_AccMaxZ.Text = data.accelerationVarianceMax.Z.toString();

            MyEditor.Instance.ee_ColorR.Text = data.color.R.ToString();
            MyEditor.Instance.ee_ColorG.Text = data.color.G.ToString();
            MyEditor.Instance.ee_ColorB.Text = data.color.B.ToString();
            MyEditor.Instance.ee_ColorA.Text = data.color.A.ToString();
            MyEditor.Instance.ee_ColorMinR.Text = data.colorVarianceMin.R.ToString();
            MyEditor.Instance.ee_ColorMinG.Text = data.colorVarianceMin.G.ToString();
            MyEditor.Instance.ee_ColorMinB.Text = data.colorVarianceMin.B.ToString();
            MyEditor.Instance.ee_ColorMinA.Text = data.colorVarianceMin.A.ToString();
            MyEditor.Instance.ee_ColorMaxR.Text = data.colorVarianceMax.R.ToString();
            MyEditor.Instance.ee_ColorMaxG.Text = data.colorVarianceMax.G.ToString();
            MyEditor.Instance.ee_ColorMaxB.Text = data.colorVarianceMax.B.ToString();
            MyEditor.Instance.ee_ColorMaxA.Text = data.colorVarianceMax.A.ToString();

            MyEditor.Instance.ee_NumPart.Text = data.nParticles.ToString();
            MyEditor.Instance.ee_SystemLife.Text = data.systemLife.ToString();
            MyEditor.Instance.ee_Rot.Text = data.particlesRotation.ToString();
            MyEditor.Instance.ee_RotVar.Text = data.particlesRotationVariance.ToString();
            MyEditor.Instance.ee_RotSpeed.Text = data.particlesRotationSpeed.ToString();
            MyEditor.Instance.ee_RotSpeedVar.Text = data.particlesRotationSpeedVariance.ToString();
            MyEditor.Instance.ee_size.Text = data.size.ToString();
            MyEditor.Instance.ee_SizeIni.Text = data.sizeIni.ToString();
            MyEditor.Instance.ee_SizeEnd.Text = data.sizeEnd.ToString();
            MyEditor.Instance.ee_FadeIn.Text = data.fadeIn.ToString();
            MyEditor.Instance.ee_FadeOut.Text = data.fadeOut.ToString();
            MyEditor.Instance.ee_Life.Text = data.particlesLife.toString();
        }

        public void updateSelected()
        {
            if (selected == null)
                return;

            ParticleSystemData data = ParticleManager.Instance.getBaseParticleSystemData(selected);

            data.textureName = MyEditor.Instance.ee_texture.Text;
            data.texture = TextureManager.Instance.getTexture("particles/" + data.textureName);

            data.type = MyEditor.Instance.ee_type.Text == "burst" ? ParticleSystemData.tParticleSystem.Burst : ParticleSystemData.tParticleSystem.Fountain;

            data.position.X = MyEditor.Instance.ee_posX.Text.toFloat();
            data.position.Y = MyEditor.Instance.ee_PosY.Text.toFloat();
            data.position.Z = MyEditor.Instance.ee_PosZ.Text.toFloat();
            data.positionVarianceMin.X = MyEditor.Instance.ee_PosMinX.Text.toFloat();
            data.positionVarianceMin.Y = MyEditor.Instance.ee_PosMinY.Text.toFloat();
            data.positionVarianceMin.Z = MyEditor.Instance.ee_PosMinZ.Text.toFloat();
            data.positionVarianceMax.X = MyEditor.Instance.ee_PosMaxX.Text.toFloat();
            data.positionVarianceMax.Y = MyEditor.Instance.ee_PosMaxY.Text.toFloat();
            data.positionVarianceMax.Z = MyEditor.Instance.ee_PosMaxZ.Text.toFloat();

            data.direction.X = MyEditor.Instance.ee_DirX.Text.toFloat();
            data.direction.Y = MyEditor.Instance.ee_DirY.Text.toFloat();
            data.direction.Z = MyEditor.Instance.ee_DirZ.Text.toFloat();
            data.directionVarianceMin.X = MyEditor.Instance.ee_DirMinX.Text.toFloat();
            data.directionVarianceMin.Y = MyEditor.Instance.ee_DirMinY.Text.toFloat();
            data.directionVarianceMin.Z = MyEditor.Instance.ee_DirMinZ.Text.toFloat();
            data.directionVarianceMax.X = MyEditor.Instance.ee_DirMaxX.Text.toFloat();
            data.directionVarianceMax.Y = MyEditor.Instance.ee_DirMaxY.Text.toFloat();
            data.directionVarianceMax.Z = MyEditor.Instance.ee_DirMaxZ.Text.toFloat();

            data.acceleration.X = MyEditor.Instance.ee_AccX.Text.toFloat();
            data.acceleration.Y = MyEditor.Instance.ee_AccY.Text.toFloat();
            data.acceleration.Z = MyEditor.Instance.ee_AccZ.Text.toFloat();
            data.accelerationVarianceMin.X = MyEditor.Instance.ee_AccMinX.Text.toFloat();
            data.accelerationVarianceMin.Y = MyEditor.Instance.ee_AccMinY.Text.toFloat();
            data.accelerationVarianceMin.Z = MyEditor.Instance.ee_AccMinZ.Text.toFloat();
            data.accelerationVarianceMax.X = MyEditor.Instance.ee_AccMaxX.Text.toFloat();
            data.accelerationVarianceMax.Y = MyEditor.Instance.ee_AccMaxY.Text.toFloat();
            data.accelerationVarianceMax.Z = MyEditor.Instance.ee_AccMaxZ.Text.toFloat();

            data.color = new Color(MyEditor.Instance.ee_ColorR.Text.toFloat(), MyEditor.Instance.ee_ColorG.Text.toFloat(), MyEditor.Instance.ee_ColorB.Text.toFloat(), MyEditor.Instance.ee_ColorA.Text.toFloat());
            data.colorVarianceMin = new Color(MyEditor.Instance.ee_ColorMinR.Text.toFloat(), MyEditor.Instance.ee_ColorMinG.Text.toFloat(), MyEditor.Instance.ee_ColorMinB.Text.toFloat(), MyEditor.Instance.ee_ColorMinA.Text.toFloat());
            data.colorVarianceMax = new Color(MyEditor.Instance.ee_ColorMaxR.Text.toFloat(), MyEditor.Instance.ee_ColorMaxG.Text.toFloat(), MyEditor.Instance.ee_ColorMaxB.Text.toFloat(), MyEditor.Instance.ee_ColorMaxA.Text.toFloat());

            data.nParticles = MyEditor.Instance.ee_NumPart.Text.toInt();
            data.systemLife = MyEditor.Instance.ee_SystemLife.Text.toFloat();
            data.particlesRotation = MyEditor.Instance.ee_Rot.Text.toFloat();
            data.particlesRotationVariance = MyEditor.Instance.ee_RotVar.Text.toFloat();
            data.particlesRotationSpeed = MyEditor.Instance.ee_RotSpeed.Text.toFloat();
            data.particlesRotationSpeedVariance = MyEditor.Instance.ee_RotSpeedVar.Text.toFloat();
            data.size = MyEditor.Instance.ee_size.Text.toFloat();
            data.sizeIni = MyEditor.Instance.ee_SizeIni.Text.toFloat();
            data.sizeEnd = MyEditor.Instance.ee_SizeEnd.Text.toFloat();
            data.fadeIn = MyEditor.Instance.ee_FadeIn.Text.toFloat();
            data.fadeOut = MyEditor.Instance.ee_FadeOut.Text.toFloat();
            data.particlesLife = MyEditor.Instance.ee_Life.Text.toFloat();

            if (MyEditor.Instance.ee_render.Text == "additive")
            {
                data.color.A = 0;
                data.colorVarianceMin.A = 0;
                data.colorVarianceMax.A = 0;
            }

            ParticleManager.Instance.setBaseParticleSystemData(selected, data);
        }

        public override void render()
        {
            ParticleManager.Instance.update();
            ParticleManager.Instance.render();
            base.render();
        }
    }
}
#endif