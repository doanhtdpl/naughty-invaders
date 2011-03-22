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

        const int NUMBER_OF_COLUMNS = 100;
        const int NUMBER_OF_ROWS = 50;
        public void renderGrid(int gridSpacing)
        {
            Vector3 startingPosition = Camera2D.position - new Vector3(
                    (NUMBER_OF_COLUMNS / 2) * gridSpacing,
                    (NUMBER_OF_ROWS / 2) * gridSpacing,
                    0.0f)
                    - new Vector3(Camera2D.position.X % gridSpacing, Camera2D.position.Y % gridSpacing, 0.0f);
            startingPosition.Z = 0;

            float lineLength = (NUMBER_OF_ROWS - 1) * gridSpacing;
            for(int i=0; i<NUMBER_OF_COLUMNS; ++i)
            {
                DebugManager.Instance.addLine(
                    startingPosition + new Vector3(gridSpacing * i, 0.0f, 0.0f),
                    startingPosition + new Vector3(gridSpacing * i, lineLength, 0.0f),
                    Color.Gray); 
            }
            lineLength = (NUMBER_OF_COLUMNS - 1) * gridSpacing;
            for (int i = 0; i < NUMBER_OF_ROWS; ++i)
            {
                DebugManager.Instance.addLine(
                    startingPosition + new Vector3(0.0f, gridSpacing * i, 0.0f),
                    startingPosition + new Vector3(lineLength, gridSpacing * i, 0.0f),
                    Color.Gray);
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
        public Entity2D rayVsEntities(Ray ray, List<Entity2D> entities, Entity2D ent = null)
        {
            float currentSeta = ent != null ? ent.position.Z : float.MinValue;
            foreach (Entity2D e in entities)
            {
                if (rayVsEntity(ray, e))
                {
                    if (ent == null || e.position.Z > currentSeta)
                    {
                        currentSeta = e.position.Z;
                        ent = e;
                    }
                }
            }
            return ent;
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

            //player
            writer.WriteStartElement("players");
            for (int i = 0; i < GamerManager.getGamerEntities().Count; i++)
            {
                Player player = GamerManager.getGamerEntity((PlayerIndex)i).Player;
                writer.WriteStartElement("player");
                writer.WriteAttributeString("playerIndex", i.ToString());
                writer.WriteAttributeString("worldMatrix", player.worldMatrix.toXML());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //camera
            writer.WriteStartElement("camera");
            foreach(NetworkNode<CameraData> cameraNode in CameraManager.Instance.getNodes().getNodes() )
            {
                writer.WriteStartElement("cameraNode");
                writer.WriteAttributeString("id", cameraNode.value.id.ToString());
                writer.WriteAttributeString("position", cameraNode.value.position.toXML());
                writer.WriteAttributeString("target", cameraNode.value.target.toXML());
                writer.WriteAttributeString("isFirst", cameraNode.value.isFirst.ToString());
                if (cameraNode.getNext() != null)
                    writer.WriteAttributeString("link", cameraNode.getNext().value.id.ToString());
                writer.WriteEndElement();
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
                        new RenderableEntity2D("staticProps", node.GetAttribute("entityName"), Vector3.Zero, 0, Color.White, id);
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
                        new AnimatedEntity2D("animatedProps", node.GetAttribute("entityName"), Vector3.Zero, 0, Color.White, id);
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
                nodes = xml_doc.GetElementsByTagName("player"); // read players
                foreach (XmlElement node in nodes)
                {
                    PlayerIndex index = (PlayerIndex)node.GetAttribute("playerIndex").toInt();
                    Matrix world = node.GetAttribute("worldMatrix").toMatrix();
                    GamerEntity gamer = GamerManager.getGamerEntity(index);
                    gamer.Player.worldMatrix = world;
                    gamer.Player.setInit();

                    //Register player again as we don't destroy it (yet...)
                    EntityManager.Instance.registerEntity(gamer.Player);
                }

                //Camera
                nodes = xml_doc.GetElementsByTagName("cameraNode");
                foreach (XmlElement node in nodes)
                {
                    Vector3 position = node.GetAttribute("position").toVector3();
                    Vector3 target = node.GetAttribute("target").toVector3();
                    int nodeId = node.GetAttribute("id").toInt();
                    bool isFirst = node.GetAttribute("isFirst").toBool();
                    NetworkNode<CameraData> cameraNode = new NetworkNode<CameraData>(new CameraData(position, target, nodeId));
                    if(node.HasAttribute("link"))
                        cameraNode.value.next = node.GetAttribute("link").toInt();

                    CameraManager.Instance.addNode(cameraNode);
                }

                //link camera nodes
                foreach(NetworkNode<CameraData> cameraNode in CameraManager.Instance.getNodes().getNodes())
                {
                    cameraNode.addLinkedNode(CameraManager.Instance.getNode(cameraNode.value.next));
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

            List<Entity2D> list = loadLevel(fileName);

            // FAKE LOADING
            CameraManager.Instance.loadXMLfake();
            // END FAKE LOADING

            return list;
        }

        // loads the specified file into the editor
        public List<Entity2D> importLevel(string fileName)
        {
            return loadLevel(fileName, false);
        }
        #endregion
    }
}
