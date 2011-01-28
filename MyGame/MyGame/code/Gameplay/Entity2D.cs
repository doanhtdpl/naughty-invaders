using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Entity2D
    {
        protected string entityName;

        Matrix world;
        public Matrix worldMatrix
        {
            get { return world; }
            set { world = value; }
        }
        // gets or set the position
        public Vector3 position
        {
            get { return world.Translation; }
            set { world.Translation = value; }
        }
        public Vector2 position2D
        {
            get { return new Vector2(world.Translation.X, world.Translation.Y); }
            set { world.Translation = new Vector3(value.X, value.Y, world.Translation.Z); }
        }
        // gets or sets the scale
        public Vector3 scale
        {
            get
            {
                Vector3 position, scale; Quaternion quaternion;
                world.Decompose(out scale, out quaternion, out position);
                return scale;
            }
            set
            {
                // create a matrix with scale 1
                world = Matrix.CreateWorld(world.Translation, world.Forward, world.Up);
                // multiply it to the new scale
                world = Matrix.CreateScale(value) * world;
            }
        }
        // gets or sets the X Y components of the scale
        public Vector2 scale2D
        {
            get
            {
                Vector3 position, scale; Quaternion quaternion;
                world.Decompose(out scale, out quaternion, out position);
                return new Vector2(scale.X, scale.Y);
            }
            set
            {
                // create a matrix with scale 1
                world = Matrix.CreateWorld(world.Translation, world.Forward, world.Up);
                // multiply it to the new scale
                world = Matrix.CreateScale(value.X, value.Y, 1) * world;
            }
        }
        // orientation is the angle of the rotation in the Z axis
        // set and get works only for entities parallel to the XY plane (the UP vector is 0,0,1)
        public float orientation
        {
            get
            {
                float angle = Calc.getAngleOfDirectionsXY(Vector3.Right, world.Right);
                return angle;
            }
            set
            {
                // set the new orientation
                Vector3 positionBackup = position;
                position = new Vector3(0, 0, 0);
                world = world * Matrix.CreateRotationZ(value - orientation);
                position = positionBackup;
            }
        }

        // public constructors
        public Entity2D(Vector3 position, Vector2 scale, float orientation, string entityName = "NoName" )
        {
            this.entityName = entityName;
            initializeWorldMatrix2D(position, scale, orientation);
        }
        public Entity2D(string entityName = "NoName") : this(Vector3.Zero, Vector2.Zero, 0, entityName) { }

        // initialize the world matrix, must be called at the creation of each entity
        public void initializeWorldMatrix2D(Vector3 position, Vector2 scale, float orientation)
        {
            world = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
            this.scale2D = scale;
            this.orientation = orientation;
        }
        // resets rotations
        public void resetRotation()
        {
            Vector3 position, scale; Quaternion quaternion;
            world.Decompose(out scale, out quaternion, out position);
            world = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
            this.scale2D = new Vector2(scale.X, scale.Y);
        }
        // returns the 2D rectangle. Good for 2D collisions
        public Rectangle getRectangle()
        {
            Vector3 pos = position;
            Vector3 size = scale;
            return new Rectangle((int)(pos.X - size.X * 0.5), (int)(pos.Y - size.Y * 0.5), (int)size.X, (int)size.Y);
        }
    }
}
