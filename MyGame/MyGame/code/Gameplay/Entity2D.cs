using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Entity2D : SortableEntity
    {
        public static int NEXT_ENTITY_ID = 1;

        public enum tEntityState { Active, Waiting, Dying, ToDelete }
        public string entityName { set; get; }
        public tEntityState entityState { get; set; }
        public int id = -1;

        Matrix initWorld;

        Matrix world;
        public Matrix worldMatrix
        {
            get { return world; }
            set { world = value; }
        }
        public override float getZ()
        {
            return world.Translation.Z;
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
                value.X = Math.Max(0.1f, value.X);
                value.Y = Math.Max(0.1f, value.Y);
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
                Vector2 scaleBackup = scale2D;
                position = new Vector3(0, 0, 0);
                world = world * Matrix.CreateRotationZ(value - orientation);
                position = positionBackup;
                scale2D = scaleBackup;
            }
        }

        public void setId(int id)
        {
            this.id = id;
        }

        // public constructors
        public Entity2D(string entityName, Vector3 position, float orientation, int id = -1)
        {
            this.entityName = entityName;
            position.Z += Calc.randomScalar(-0.02f, 0.02f);

            initializeWorldMatrix2D(position, scale2D, orientation);

            if (id == -1)
                this.id = NEXT_ENTITY_ID++;
            else
                this.id = id;
        }

        // initialize the world matrix, must be called at the creation of each entity
        public void initializeWorldMatrix2D(Vector3 position, Vector2 scale, float orientation)
        {
            world = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
            this.scale2D = scale;
            this.orientation = orientation;
        }
        public void rotateInX(float radians)
        {
            Vector3 pos = world.Translation;
            world.Translation = Vector3.Zero;
            world *= Matrix.CreateRotationX(radians);
            world.Translation = pos;
        }
        public void rotateInY(float radians)
        {
            Vector3 pos = world.Translation;
            world.Translation = Vector3.Zero;
            world *= Matrix.CreateRotationY(radians);
            world.Translation = pos;
        }
        // resets rotations
        public void resetRotation()
        {
            Vector3 position, scale; Quaternion quaternion;
            world.Decompose(out scale, out quaternion, out position);
            world = Matrix.CreateWorld(position, Vector3.Forward, Vector3.Up);
            this.scale2D = new Vector2(scale.X, scale.Y);
        }
        // returns the 2D rectangle
        public Rectangle getRectangle()
        {
            return new Rectangle((int)(position.X - scale.X * 0.5), (int)(position.Y - scale.Y * 0.5), (int)scale.X, (int)scale.Y);
        }
        // returns the aproximate radius of this entity
        public virtual float getRadius()
        {
            return scale.X * 0.4f;
        }

        public virtual void update() { }

        public virtual void delete()
        {
            EntityManager.Instance.removeEntity(this);

            LevelManager.Instance.removeEntity(this);
        }

        public virtual void setInit() { initWorld = world; }
        public override void reset() { world = initWorld; }
    }
}
