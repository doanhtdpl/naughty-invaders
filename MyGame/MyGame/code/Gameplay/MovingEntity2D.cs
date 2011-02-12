using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class MovingEntity2D : Entity2D
    {
        Vector3 directionVector;
        Vector3 accelerationVector;

        public Vector3 direction { set; get; }
        public Vector3 acceleration { set; get; }
        public Vector2 direction2D
        {
            get { return new Vector2(directionVector.X, directionVector.Y); }
            set { directionVector.X = value.X; directionVector.Y = value.Y; }
        }
        public Vector2 acceleration2D
        {
            get { return new Vector2(accelerationVector.X, accelerationVector.Y); }
            set { accelerationVector.X = value.X; accelerationVector.Y = value.Y; }
        }

        public MovingEntity2D(string entityName, Vector3 position, float orientation)
            : base(entityName, position, orientation)
        {
            direction = Vector3.Zero;
            acceleration = Vector3.Zero;
        }
    }
}
