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
using System.Xml.Linq;

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
            Vector3 nearPoint = GraphicsManager.Instance.graphicsDevice.Viewport.Unproject(nearSource, Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 farPoint = GraphicsManager.Instance.graphicsDevice.Viewport.Unproject(farSource, Camera2D.projection, Camera2D.view, Matrix.Identity);

            // Find the direction vector that goes from the nearPoint to the farPoint and normalize it...
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            // ... and then create a new ray using nearPoint as the source.
            return new Ray(nearPoint, direction);
        }

        public Vector3 getMousePosInZ(Vector2 mousePos, float z = 0.0f)
        {
            Vector3 near = GraphicsManager.Instance.graphicsDevice.Viewport.Unproject(new Vector3(mousePos.X, mousePos.Y, 0.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 far = GraphicsManager.Instance.graphicsDevice.Viewport.Unproject(new Vector3(mousePos.X, mousePos.Y, 1.0f), Camera2D.projection, Camera2D.view, Matrix.Identity);
            Vector3 normal = new Vector3(0, 0, -1);

            float u = Vector3.Dot(normal, new Vector3(0.0f, 0.0f, z) - near) / Vector3.Dot(normal, far - near);
            Vector3 pos = near + u * (far - near);
            return new Vector3(pos.X, pos.Y, z);
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
        public Entity2D rayVsEntities(Ray ray, List<Entity2D> entities, Entity2D ent = null, List<Entity2D> ignoreEntities = null)
        {
            float currentSeta = ent != null ? ent.position.Z : float.MinValue;
            foreach (Entity2D e in entities)
            {
                if (ignoreEntities.IndexOf(e) == -1 &&  rayVsEntity(ray, e))
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
#if !XBOX360
        void writeEntity(XmlTextWriter writer, Entity2D entity2D, string element)
        {
            writer.WriteStartElement(element);
            writer.WriteAttributeString("entityName", entity2D.entityName);
            writer.WriteAttributeString("worldMatrix", entity2D.worldMatrix.toXML());
            writer.WriteAttributeString("id", entity2D.id.ToString());
            RenderableEntity2D ent = (RenderableEntity2D) entity2D;
            writer.WriteAttributeString("color", ent.color.toXML());
            writer.WriteAttributeString("flipH", ent.flipHorizontal.ToString());
            writer.WriteAttributeString("flipV", ent.flipVertical.ToString());
            writer.WriteAttributeString("living", ent.living.ToString());
            writer.WriteAttributeString("livingIntensityMin", ent.livingIntensityMin.ToString());
            writer.WriteAttributeString("livingIntensityMax", ent.livingIntensityMax.ToString());
            writer.WriteAttributeString("livingSpeedMin", ent.livingSpeedMin.ToString());
            writer.WriteAttributeString("livingSpeedMax", ent.livingSpeedMax.ToString());
            writer.WriteEndElement();
        }

        void writeLevelCollision(XmlTextWriter writer, Line line)
        {
            writer.WriteStartElement("levelCollision");
            writer.WriteAttributeString("p1", line.p1.toXML());
            writer.WriteAttributeString("p2", line.p2.toXML());
            writer.WriteEndElement();
        }

        private void writeEnemyZone(XmlTextWriter writer, EnemySpawnZone esz)
        {
            writer.WriteStartElement("enemySpawnZone");
            writer.WriteAttributeString("zone", esz.getZone().toXML());
            writer.WriteAttributeString("enemy", esz.getEnemyName());
            writer.WriteAttributeString("count", esz.getTotalSpawns().ToString());
            writer.WriteEndElement();
        }

        public void saveLevelToXML(string name)
        {
            XmlTextWriter writer = new XmlTextWriter(name, null);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("level");

            // here is the general information for the level
            writer.WriteAttributeString("nextEntityID", Entity2D.NEXT_ENTITY_ID.ToString());
            writer.WriteAttributeString("BGColor", SB.BGColor.toXML());
            writer.WriteAttributeString("CameraMode", CameraManager.Instance.cameraMode.ToString());

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

            //enemy zones
            writer.WriteStartElement("enemySpawnZones");
            for (int i = 0; i < EnemyManager.Instance.getEnemySpawnZones().Count; i++)
            {
                EnemySpawnZone esz = EnemyManager.Instance.getEnemySpawnZones()[i];
                writeEnemyZone(writer, esz);
            }
            writer.WriteEndElement();

            // level lines
            writer.WriteStartElement("levelCollisions");
            for (int i = 0; i < LevelManager.Instance.getLevelCollisions().Count; i++)
            {
                writeLevelCollision(writer, LevelManager.Instance.getLevelCollisions()[i]);
            }
            writer.WriteEndElement();

            //camera
            writer.WriteStartElement("camera");
            foreach(NetworkNode<CameraData> cameraNode in CameraManager.Instance.getNodes().getNodes() )
            {
                writer.WriteStartElement("cameraNode");
                writer.WriteAttributeString("id", cameraNode.value.id.ToString());
                writer.WriteAttributeString("position", cameraNode.position.toXML());
                writer.WriteAttributeString("target", cameraNode.value.target.toXML());
                writer.WriteAttributeString("isFirst", cameraNode.value.isFirst.ToString());
                if (cameraNode.getNext() != null)
                    writer.WriteAttributeString("link", cameraNode.getNext().value.id.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //particles
            writer.WriteStartElement("particles");
            foreach (ParticleSystem particle in ParticleManager.Instance.getParticles())
            {
                writer.WriteStartElement("particle");
                writer.WriteAttributeString("name", particle.data.name);
                writer.WriteAttributeString("position", particle.position.toXML());
                writer.WriteAttributeString("direction", particle.direction.toXML());
                writer.WriteAttributeString("color", particle.data.color.toXML());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //triggers
            writer.WriteStartElement("triggers");
            foreach (Trigger trigger in TriggerManager.Instance.getTriggers())
            {
                writer.WriteStartElement("trigger");
                writer.WriteAttributeString("position", trigger.position.toXML());

                writer.WriteStartElement("conditions");
                foreach (Function func in trigger.conditions)
                {
                    writer.WriteStartElement("condition");
                    writer.WriteAttributeString("functionName", func.functionName);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("consecuences");
                foreach (Function func in trigger.executions)
                {
                    writer.WriteStartElement("consecuence");
                    writer.WriteAttributeString("functionName", func.functionName);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            //triggers
            writer.WriteStartElement("players");
            writer.WriteStartElement("player");
            writer.WriteAttributeString("playerId", "0");
            writer.WriteAttributeString("pos", GamerManager.getMainPlayer().initPos.toXML());
            writer.WriteEndElement();
            writer.WriteEndElement();

            // close the tag <level> and the writer
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
#endif
        // loads the specified file into the editor
        private List<Entity2D> loadLevel(string fileName, bool loadIDs = true)
        {
            // TODO ugly! this must have to be set with the editor and loaded from xml
            if (GamerManager.getMainPlayer() == null)
            {
                GamerManager.createGamerEntity(PlayerIndex.One, true);
            }
            GamerManager.getMainPlayer().mode = Player.tMode.Arcade;

            //fileName = SB.content.RootDirectory + "/xml/levels/" + fileName + ".xml";
            List<Entity2D> list = new List<Entity2D>();
            if (File.Exists(fileName))
            {
                XDocument xml_doc = XDocument.Load(fileName);

                int id;
                IEnumerable<XElement> nodes;

                if (loadIDs)
                {
                    nodes = xml_doc.Descendants("level");
                    XElement levelNode = nodes.First();
                    if (levelNode.Attributes("nextEntityID").Count() > 0)
                    {
                        Entity2D.NEXT_ENTITY_ID = levelNode.Attribute("nextEntityID").Value.toInt();
                    }

                    if (levelNode.Attributes("BGColor").Count() > 0)
                    {
                        SB.BGColor = levelNode.Attribute("BGColor").Value.toColor();
                    }

                    if (levelNode.Attributes("CameraMode").Count() > 0)
                    {
                        CameraManager.Instance.cameraMode = (CameraManager.tCameraMode)Enum.Parse(typeof(CameraManager.tCameraMode), levelNode.Attribute("CameraMode").Value, true);
                    }
                }

                // read static props
                nodes = xml_doc.Descendants("staticProp"); 
                foreach (XElement node in nodes)
                {
                    id = -1;
                    if (loadIDs && node.Attributes("id").Count() > 0) id = node.Attribute("id").Value.toInt();
                    RenderableEntity2D re = new RenderableEntity2D("staticProps", node.Attribute("entityName").Value, Vector3.Zero, 0, Color.White, true, id);
                    re.worldMatrix = node.Attribute("worldMatrix").Value.toMatrix();
                    if (re.worldMatrix.AnyNanCoord())
                        continue;

                    if (node.Attributes("color").Count() > 0)
                    {
                        re.color = node.Attribute("color").Value.toColor();
                    }
                    if (node.Attributes("flipH").Count() > 0)
                    {
                        re.flipHorizontal = node.Attribute("flipH").Value.toBool();
                        re.flipVertical = node.Attribute("flipV").Value.toBool();
                    }
                    if (node.Attributes("living").Count() > 0)
                    {
                        re.living = node.Attribute("living").Value.toBool();
                    }
                    if (node.Attributes("livingIntensityMin").Count() > 0)
                    {
                        re.livingIntensityMin = node.Attribute("livingIntensityMin").Value.toFloat();
                        re.livingIntensityMax = node.Attribute("livingIntensityMax").Value.toFloat();
                        re.livingSpeedMin = node.Attribute("livingSpeedMin").Value.toFloat();
                        re.livingSpeedMax = node.Attribute("livingSpeedMax").Value.toFloat();
                    }

                    LevelManager.Instance.addStaticProp(re);
                    re.setInit();
                    list.Add(re);
                }

                // read animated props
                nodes = xml_doc.Descendants("animatedProp");
                foreach (XElement node in nodes)
                {
                    id = -1;
                    if (loadIDs && node.Attributes("id").Count() > 0) id = node.Attribute("id").Value.toInt();
                    AnimatedEntity2D ae = new AnimatedEntity2D("animatedProps", node.Attribute("entityName").Value, Vector3.Zero, 0, Color.White, true, id);
                    ae.worldMatrix = node.Attribute("worldMatrix").Value.toMatrix();
                    if (ae.worldMatrix.AnyNanCoord())
                        continue;
                    if (node.Attributes("color").Count() > 0)
                    {
                        ae.color = node.Attribute("color").Value.toColor();
                    }
                    if (node.Attributes("flipH").Count() > 0)
                    {
                        ae.flipHorizontal = node.Attribute("flipH").Value.toBool();
                        ae.flipVertical = node.Attribute("flipV").Value.toBool();
                    }
                    if (node.Attributes("living").Count() > 0)
                    {
                        ae.living = node.Attribute("living").Value.toBool();
                    }
                    if (node.Attributes("livingIntensityMin").Count() > 0)
                    {
                        ae.livingIntensityMin = node.Attribute("livingIntensityMin").Value.toFloat();
                        ae.livingIntensityMax = node.Attribute("livingIntensityMax").Value.toFloat();
                        ae.livingSpeedMin = node.Attribute("livingSpeedMin").Value.toFloat();
                        ae.livingSpeedMax = node.Attribute("livingSpeedMax").Value.toFloat();
                    }

                    LevelManager.Instance.addAnimatedProp(ae);
                    ae.setInit();
                    list.Add(ae);
                }

                // read enemies
                nodes = xml_doc.Descendants("enemy");
                foreach (XElement node in nodes)
                {
                    id = -1;
                    if (loadIDs && node.Attributes("id").Count() > 0) id = node.Attribute("id").Value.toInt();
                    string name = node.Attribute("entityName").Value;
                    Matrix world = node.Attribute("worldMatrix").Value.toMatrix();
                    if (world.AnyNanCoord())
                        continue;
                    RenderableEntity2D e = (RenderableEntity2D)EnemyManager.Instance.addEnemy(name, world.Translation, id);
                    if (node.Attributes("color").Count() > 0)
                    {
                        e.color = node.Attribute("color").Value.toColor();
                    }
                    if (node.Attributes("flipH").Count() > 0)
                    {
                        e.flipHorizontal = node.Attribute("flipH").Value.toBool();
                        e.flipVertical = node.Attribute("flipV").Value.toBool();
                    }

                    if (node.Attributes("living").Count() > 0)
                    {
                        e.living = node.Attribute("living").Value.toBool();
                    }
                    if (node.Attributes("livingIntensityMin").Count() > 0)
                    {
                        e.livingIntensityMin = node.Attribute("livingIntensityMin").Value.toFloat();
                        e.livingIntensityMax = node.Attribute("livingIntensityMax").Value.toFloat();
                        e.livingSpeedMin = node.Attribute("livingSpeedMin").Value.toFloat();
                        e.livingSpeedMax = node.Attribute("livingSpeedMax").Value.toFloat();
                    }

                    e.setInit();
                    list.Add(e);
                }

                //Enemy spaws zones
                nodes = xml_doc.Descendants("enemySpawnZone");
                foreach (XElement node in nodes)
                {
                    string name = node.Attribute("enemy").Value;
                    Rectangle rect = node.Attribute("zone").Value.toRectangle();
                    int count = node.Attribute("count").Value.toInt();

                    EnemyManager.Instance.addEnemySpawnZone(new EnemySpawnZone(name, rect, count));
                }

                // read level collisions
                nodes = xml_doc.Descendants("levelCollision");
                foreach (XElement node in nodes)
                {
                    Line l = new Line(node.Attribute("p1").Value.toVector2(), node.Attribute("p2").Value.toVector2());
                    LevelManager.Instance.addLevelCollision(l);
                }

                // player
                if (GamerManager.getMainPlayer() == null)
                {
                    GamerManager.createGamerEntity(PlayerIndex.One, true);
                }

                nodes = xml_doc.Descendants("player");
                foreach (XElement node in nodes)
                {
                    int playerId = node.Attribute("playerId").Value.toInt();
                    Vector3 pos = node.Attribute("pos").Value.toVector3();
                    GamerManager.getMainPlayer().initPos = pos;
                }

                if (fileName == "Content/xml/levels/mapa.xml")
                {
                    GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.NoRender;
                }
                else
                {
                    EntityManager.Instance.registerEntity(GamerManager.getMainPlayer());
                    GamerManager.getMainPlayer().renderState = RenderableEntity2D.tRenderState.Render;
                    GamerManager.getMainPlayer().position = GamerManager.getMainPlayer().initPos;
                }
                GamerManager.getMainPlayer().initLevel();

                //Camera
                nodes = xml_doc.Descendants("cameraNode");
                foreach (XElement node in nodes)
                {
                    Vector3 position = node.Attribute("position").Value.toVector3();
                    Vector3 target = node.Attribute("target").Value.toVector3();
                    int nodeId = node.Attribute("id").Value.toInt();
                    bool isFirst = node.Attribute("isFirst").Value.toBool();
                    NetworkNode<CameraData> cameraNode = new NetworkNode<CameraData>(new CameraData(target, nodeId, isFirst), position);
                    if(node.Attributes("link").Count() > 0)
                        cameraNode.value.next = node.Attribute("link").Value.toInt();

                    CameraManager.Instance.addNode(cameraNode);
                }
                //link camera nodes
                foreach(NetworkNode<CameraData> cameraNode in CameraManager.Instance.getNodes().getNodes())
                {
                    cameraNode.addLinkedNode(CameraManager.Instance.getNode(cameraNode.value.next));
                }
                CameraManager.Instance.setupCamera();

                //particles
                nodes = xml_doc.Descendants("particle");
                foreach (XElement node in nodes)
                {
                    string name = node.Attribute("name").Value;
                    Vector3 position = node.Attribute("position").Value.toVector3();
                    Vector3 direction = node.Attribute("direction").Value.toVector3();
                    Color color = node.Attribute("color").Value.toColor();
                    if(!position.AnyNanCoord() && !direction.AnyNanCoord())
                        ParticleManager.Instance.addParticles(name, position, direction, color);
                }

                //triggers
                nodes = xml_doc.Descendants("trigger");
                foreach (XElement node in nodes)
                {
                    Vector2 position = node.Attribute("position").Value.toVector2();
                    Trigger trigger = new Trigger();
                    trigger.position = position;

                    IEnumerable<XElement> moreNodes = node.Descendants("condition");
                    foreach (XElement func in moreNodes)
                    {
                        trigger.addFunction(true, func.Attribute("functionName").Value);
                    }

                    moreNodes = node.Descendants("consecuence");
                    foreach (XElement func in moreNodes)
                    {
                        trigger.addFunction(false, func.Attribute("functionName").Value);
                    }

                    TriggerManager.Instance.addTrigger(trigger);
                }

                if (loadIDs)
                {
                    // groups
                    nodes = xml_doc.Descendants("group"); // read enemies
                    foreach (XElement node in nodes)
                    {
                        IEnumerable<XElement> ids = node.Descendants("entity");
                        List<int> idList = new List<int>();
                        foreach (XElement entityId in ids)
                        {
                            int entityID = entityId.Attribute("id").Value.toInt();
                            if (EntityManager.Instance.getEntityByID(entityID) != null)
                            {
                                idList.Add(entityID);
                            }
                        }
                        if(idList.Count > 1)
                            LevelManager.Instance.addGroup(idList);
                    }
                }
            }

            return list;
        }

        public List<Entity2D> loadNewLevelFromGame(string fileName, bool loadIDs = true)
        {
            fileName = SB.content.RootDirectory + "/xml/levels/" + fileName + ".xml";
            return loadNewLevel(fileName);
        }

        // loads the specified file into the editor
        public List<Entity2D> loadNewLevel(string fileName)
        {
            LevelManager.Instance.cleanLevel();

            List<Entity2D> list = loadLevel(fileName);

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
