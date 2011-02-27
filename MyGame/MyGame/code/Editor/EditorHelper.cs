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
        void writeEntity(XmlTextWriter writer, Entity2D entity2D, string element)
        {
            writer.WriteStartElement(element);
            writer.WriteAttributeString("entityName", entity2D.entityName);
            writer.WriteAttributeString("worldMatrix", entity2D.worldMatrix.toXML());
            writer.WriteAttributeString("id", entity2D.id.ToString());
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
            writer.WriteAttributeString("nextEntityID", Entity2D.NEXT_ENTITY_ID.ToString());
            
            // static props
            writer.WriteStartElement("staticProps");
            for (int i = 0; i < LevelManager.Instance.getStaticProps().Count; i++)
            {
                Entity2D sp = LevelManager.Instance.getStaticProps()[i];
                writeEntity(writer, sp, "staticProp");
            }
            writer.WriteEndElement();

            // animated props
            writer.WriteStartElement("animatedProps");
            for (int i = 0; i < LevelManager.Instance.getAnimatedProps().Count; i++)
            {
                Entity2D ap = LevelManager.Instance.getAnimatedProps()[i];
                writeEntity(writer, ap, "animatedProp");
            }
            writer.WriteEndElement();

            // groups
            writer.WriteStartElement("groups");
            for (int i = 0; i < LevelManager.Instance.getGroups().Count; i++)
            {
                List<int> group = LevelManager.Instance.getGroups()[i];
                writer.WriteStartElement("group");
                foreach (int id in group)
                {
                    writer.WriteStartElement("entity");
                    writer.WriteAttributeString("id", id.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            // enemies
            writer.WriteStartElement("enemies");
            for (int i = 0; i < EnemyManager.Instance.getEnemies().Count; i++)
            {
                Enemy e = (Enemy)EnemyManager.Instance.getEnemies()[i];
                writeEntity(writer, e, "enemy");
            }
            writer.WriteEndElement();

            // close the tag <level> and the writer
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        // loads the specified file into the editor
        private List<Entity2D> loadLevel(string fileName, bool loadIDs = true)
        {
            List<Entity2D> list = new List<Entity2D>();
            if (File.Exists(fileName))
            {
                Stream stream = File.OpenRead(fileName);
                XmlDocument xml_doc = new XmlDocument();
                xml_doc.Load(stream);

                int id;
                XmlNodeList nodes;

                if (loadIDs)
                {
                    nodes = xml_doc.GetElementsByTagName("level");
                    XmlElement levelNode = (XmlElement)nodes[0];
                    if (levelNode.HasAttribute("nextEntityID"))
                    {
                        Entity2D.NEXT_ENTITY_ID = int.Parse(levelNode.GetAttribute("nextEntityID"));
                    }
                }

                nodes = xml_doc.GetElementsByTagName("staticProp"); // read static props
                foreach (XmlElement node in nodes)
                {
                    id = -1;
                    if (loadIDs && node.HasAttribute("id")) id = int.Parse(node.GetAttribute("id"));
                    RenderableEntity2D re =
                        new RenderableEntity2D("staticProps", node.GetAttribute("entityName"), Vector3.Zero, 0, id);
                    re.worldMatrix = node.GetAttribute("worldMatrix").toMatrix();
                    LevelManager.Instance.addStaticProp(re);
                    re.setInit();
                    list.Add(re);
                }
                nodes = xml_doc.GetElementsByTagName("animatedProp"); // read animated props
                foreach (XmlElement node in nodes)
                {
                    id = -1;
                    if (loadIDs && node.HasAttribute("id")) id = int.Parse(node.GetAttribute("id"));
                    AnimatedEntity2D ae =
                        new AnimatedEntity2D("animatedProps", node.GetAttribute("entityName"), Vector3.Zero, 0, id);
                    ae.worldMatrix = node.GetAttribute("worldMatrix").toMatrix();
                    LevelManager.Instance.addAnimatedProp(ae);
                    ae.setInit();
                    list.Add(ae);
                }
                nodes = xml_doc.GetElementsByTagName("enemy"); // read enemies
                foreach (XmlElement node in nodes)
                {
                    id = -1;
                    if (loadIDs && node.HasAttribute("id")) id = int.Parse(node.GetAttribute("id"));
                    string name = node.GetAttribute("entityName");
                    Matrix world = node.GetAttribute("worldMatrix").toMatrix();
                    Entity2D e = EnemyManager.Instance.addEnemy(name, world.Translation, id);
                    e.setInit();
                    list.Add(e);
                }

                if (loadIDs)
                {
                    // groups
                    nodes = xml_doc.GetElementsByTagName("group"); // read enemies
                    foreach (XmlElement node in nodes)
                    {
                        XmlNodeList ids = node.GetElementsByTagName("entity");
                        List<int> idList = new List<int>();
                        foreach (XmlElement entityId in ids)
                        {
                            idList.Add(int.Parse(entityId.GetAttribute("id")));
                        }
                        LevelManager.Instance.addGroup(idList);
                    }
                }

                stream.Close();
            }

            return list;
        }

        // loads the specified file into the editor
        public List<Entity2D> loadNewLevel(string fileName)
        {
            LevelManager.Instance.cleanLevel();

            GamerManager.getGamerEntities()[0].createPlayer();

            // FAKE LOADING
            CameraManager.Instance.loadXMLfake();
            // END FAKE LOADING

            return loadLevel(fileName);
        }

        // loads the specified file into the editor
        public List<Entity2D> importLevel(string fileName)
        {
            return loadLevel(fileName, false);
        }
        #endregion
    }
}
