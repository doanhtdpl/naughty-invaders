using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Globalization;
using System.IO;

namespace MyGame
{
    class EditorHelper
    {
        static EditorHelper instance = null;
        EditorHelper()
        {
        }
        public static EditorHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EditorHelper();
                }
                return instance;
            }
        }

        #region Mouse
        public Ray getMouseCursorRay(Vector2 mousePos)
        {
            // Create 2 positions in screenspace using the cursor position.
            // 0 is as close as possible to the camera, 1 is as far away as possible.
            Vector3 nearSource = new Vector3(mousePos.X, mousePos.Y, 0.0f);
            Vector3 farSource = new Vector3(mousePos.X, mousePos.Y, 1.0f);

            // Use Viewport. Unproject to tell what those two screen space positions would be in world space. 
            // We'll need the projection matrix and view matrix, which we have saved as member variables. 
            // We also need a world matrix, which can just be identity.
            Vector3 nearPoint = SB.graphicsDevice.Viewport.Unproject(nearSource, Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 farPoint = SB.graphicsDevice.Viewport.Unproject(farSource, Camera2D.projection, Camera2D.view, Matrix.Identity);

            // Find the direction vector that goes from the nearPoint to the farPoint and normalize it...
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            // ... and then create a new ray using nearPoint as the source.
            return new Ray(nearPoint, direction);
        }
        #endregion
        #region Entities
        // returns the 4 points that conforms the quad of this entity in order: up-right, up-left, bottom-left, bottom-right
        Vector3[] getEntityQuad(Entity2D entity)
        {
            Matrix world = entity.worldMatrix;

            Vector3[] quad = new Vector3[4];
            Vector3 point = new Vector3(0.5f, 0.5f, 0.0f);
            Vector3.Transform(ref point, ref world, out quad[0]);
            point = new Vector3(-0.5f, 0.5f, 0.0f);
            Vector3.Transform(ref point, ref world, out quad[1]);
            point = new Vector3(-0.5f, -0.5f, 0.0f);
            Vector3.Transform(ref point, ref world, out quad[2]);
            point = new Vector3(0.5f, -0.5f, 0.0f);
            Vector3.Transform(ref point, ref world, out quad[3]);
            return quad;
        }
        // renders the border lines of the quad representing this entity
        public void renderEntityQuad(Entity2D entity)
        {
            Vector3[] quad = getEntityQuad(entity);
            DebugManager.Instance.addLine(quad[0], quad[1], Color.Green);
            DebugManager.Instance.addLine(quad[1], quad[2], Color.Green);
            DebugManager.Instance.addLine(quad[2], quad[3], Color.Green);
            DebugManager.Instance.addLine(quad[3], quad[0], Color.Green);
        }
        // returns true if the ray collides with the quad representing this entity
        public bool rayVsEntity(Ray ray, Entity2D entity)
        {
            Vector3[] vertexs = getEntityQuad(entity);

            return ray.intersectsTriangle(vertexs[0], vertexs[1], vertexs[2])
                || ray.intersectsTriangle(vertexs[0], vertexs[2], vertexs[3]);
        }
        // returns the entity from the lits that collides with a ray or null if none collides
        public Entity2D rayVsEntities(Ray ray, List<Entity2D> entities)
        {
            foreach (Entity2D e in entities)
            {
                if (rayVsEntity(ray, e))
                {
                    return e;
                }
            }
            return null;
        }
        #endregion
        #region XML
        // NOTE maybe those functions can be only one passing a string with the entity to write in the xml ("staticProp", "enemy", etc)
        void writeStaticProp(XmlTextWriter writer, Entity2D entity2D)
        {
            writer.WriteStartElement("staticProp");
            writer.WriteAttributeString("entityName", entity2D.entityName);
            writer.WriteAttributeString("worldMatrix", entity2D.worldMatrix.toXML());
            writer.WriteEndElement();
        }
        void writeAnimatedProp(XmlTextWriter writer, Entity2D entity2D)
        {
            writer.WriteStartElement("animatedProp");
            writer.WriteAttributeString("entityName", entity2D.entityName);
            writer.WriteAttributeString("worldMatrix", entity2D.worldMatrix.toXML());
            writer.WriteEndElement();
        }
        void writeEnemy(XmlTextWriter writer, Entity2D entity2D)
        {
            writer.WriteStartElement("animatedProp");
            writer.WriteAttributeString("entityName", entity2D.entityName);
            writer.WriteAttributeString("worldMatrix", entity2D.worldMatrix.toXML());
            writer.WriteEndElement();
        }
        public void saveLevelToXML(string name)
        {
            // creates directory if it doesnt already exist
            //Directory.CreateDirectory(SB.content.RootDirectory + "/xml/levels/");
            //XmlTextWriter writer = new XmlTextWriter(SB.content.RootDirectory + "/xml/levels/" + name + ".xml", null);
            XmlTextWriter writer = new XmlTextWriter(name, null);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("level");
            // here is the general information for the level
            
            // static props
            writer.WriteStartElement("staticProps");
            for (int i = 0; i < LevelManager.Instance.getStaticProps().Count; i++)
            {
                Entity2D sp = LevelManager.Instance.getStaticProps()[i];
                writeStaticProp(writer, sp);
            }
            writer.WriteEndElement();

            // animated props
            writer.WriteStartElement("animatedProps");
            for (int i = 0; i < LevelManager.Instance.getAnimatedProps().Count; i++)
            {
                Entity2D ap = LevelManager.Instance.getAnimatedProps()[i];
                writeStaticProp(writer, ap);
            }
            writer.WriteEndElement();

            // enemies
            writer.WriteStartElement("enemies");
            for (int i = 0; i < EnemyManager.Instance.getEnemies().Count; i++)
            {
                Enemy e = (Enemy)EnemyManager.Instance.getEnemies()[i];
                writeEnemy(writer, e);
            }
            writer.WriteEndElement();

            // close the tag <level> and the writer
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        // loads the specified file into the editor
        public void loadLevelFromXML(string fileName)
        {
            LevelManager.Instance.cleanLevel();

            // FAKE LOADING
            CameraManager.Instance.loadXMLfake();
            // END FAKE LOADING

            if (File.Exists(fileName))
            {
                Stream stream = File.OpenRead(fileName);
                XmlDocument xml_doc = new XmlDocument();
                xml_doc.Load(stream);

                XmlNodeList nodes;
                nodes = xml_doc.GetElementsByTagName("staticProp"); // read static props
                foreach (XmlElement node in nodes)
                {
                    RenderableEntity2D re =
                        new RenderableEntity2D( "staticProps", node.GetAttribute("entityName"), Vector3.Zero, 0);
                    re.worldMatrix = node.GetAttribute("worldMatrix").toMatrix();
                    LevelManager.Instance.addStaticProp(re);
                }
                nodes = xml_doc.GetElementsByTagName("animatedProp"); // read animated props
                foreach (XmlElement node in nodes)
                {
                    AnimatedEntity2D ae =
                        new AnimatedEntity2D("animatedProp", node.GetAttribute("entityName"), Vector3.Zero, 0);
                    ae.worldMatrix = node.GetAttribute("worldMatrix").toMatrix();
                    LevelManager.Instance.addAnimatedProp(ae);
                }
                nodes = xml_doc.GetElementsByTagName("enemy"); // read enemies
                foreach (XmlElement node in nodes)
                {
                    Enemy e = new Enemy(node.GetAttribute("entityName"), Vector3.Zero, 0);
                    e.worldMatrix = node.GetAttribute("worldMatrix").toMatrix();
                    EnemyManager.Instance.addEnemy(e);
                }

                stream.Close();
            }
            
        }
        #endregion
    }
}
