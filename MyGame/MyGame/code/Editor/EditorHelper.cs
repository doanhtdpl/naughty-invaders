using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
        public Ray getMouseCursorRay()
        {
            MouseState currentMouseState = Mouse.GetState();
            // Create 2 positions in screenspace using the cursor position.
            // 0 is as close as possible to the camera, 1 is as far away as possible.
            Vector3 nearSource = new Vector3(currentMouseState.X, currentMouseState.Y, 0.0f);
            Vector3 farSource = new Vector3(currentMouseState.X, currentMouseState.Y, 1.0f);

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
        // returns the 4 points that conforms the quad of this entity
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
            Vector3 min, max;
            Matrix world = entity.worldMatrix;
            Vector3 point = new Vector3(0.5f, 0.5f, 0.0f);
            Vector3.Transform(ref point, ref world, out min);
            point = new Vector3(-0.5f, -0.5f, 0.0f);
            Vector3.Transform(ref point, ref world, out max);
            BoundingBox boundingBox = new BoundingBox(min, max);

            return ray.Intersects(boundingBox) != null;
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
    }
}
