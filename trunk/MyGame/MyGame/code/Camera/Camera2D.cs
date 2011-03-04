using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    public class Camera2D
    {
        #region fields
        // Como la cámara es para 2D, la dirección siempre será la misma. Solo necesitamos un vector3: 
        // las coordenadas X,Y sirven para especificar la coordenada 2D del mundo, y la Z para el zoom
        public static Vector3 position;
        // este rectángulo abarca lo que vemos en la pantalla. Se actualiza cuando se modifica la Z que es el
        // zoom de la cámara. Utilizamos para ver si los objetos están o no en pantalla.
        static public Rectangle screen;
        static public Matrix view;
        static public Matrix projection;
        // este rectángulo es similar al screen, es donde el player puede moverse. Como depende del zoom
        // está situado aquí también. La cámara es accesible en todo el juego.
        static public Rectangle playableZone;
        static public Line[] playableZoneCollisions = new Line[4];
        static public Vector2 playableMargins;
        private Rectangle atZ;
        private int aspectZ;
        #endregion

        //private static bool inZoom = false;
        public static float zoomToReach;
        public Camera2D()
        {
            // sabemos que con Z = 930 la X y la Y de la pantalla son la mitad negativa de los preferred back buffer.
            // y que el ancho y largo son los mismos preferred. Esto es para la resolución 1024x800 así que hay que
            // adaptarlo a la resolución que tengamos.
            // para 1280 x 720 z debe ser 870
            atZ = new Rectangle(-SB.width / 2,
                    -SB.height / 2,
                    SB.width,
                    SB.height);
            if (SB.width == 1280)
            {
                aspectZ = 870;
            }
            else
            {
                aspectZ = 930;
            }

            for (int i = 0; i < 4; ++i)
            {
                playableZoneCollisions[i] = new Line(Vector2.Zero, Vector2.Zero);
            }
        }

        public void init(float zoom)
        {
            position.X = 0;
            position.Y = 0;
            position.Z = zoom;
            update();
        }

        public void update()
        {
            //position.X = 0;
            //position.Y = 0;
            //position.Z = 800;
            activate();
        }
        public static void setZoom(float zoom)
        {
            zoomToReach = zoom;
            //inZoom = true;
        }

        public void activate()
        {
            calculateScreen();
            calculatePlayableZone();
            Camera2D.view = Matrix.CreateLookAt(position, new Vector3(position.X, position.Y, 0), Vector3.Up);
        }
        private void calculateScreen()
        {
            Vector2 screenPos;
            screenPos.X = (atZ.X) * (position.Z / aspectZ) + position.X;
            screenPos.Y = (atZ.Y) * (position.Z / aspectZ) + position.Y;

            int width = (int)(atZ.Width * (position.Z / aspectZ));
            int height = (int)(atZ.Height * (position.Z / aspectZ));

            screen = new Rectangle((int)screenPos.X, (int)screenPos.Y, width, height);
        }
        // playable zone = la zona comprendida por la screen y unos márgenes de fuera de la screen
        // donde el juego calcula todo lo que ocurre
        private void calculatePlayableZone()
        {
            float addWidth = playableMargins.X * (position.Z / 930);
            float addHeight = playableMargins.Y * (position.Z / 930);
            playableZone = new Rectangle((int)(screen.X - addWidth), (int)(screen.Y - addHeight), (int)(screen.Width + (addWidth * 2)), (int)(screen.Height + (addHeight * 2)));

            playableZoneCollisions[0].p1.X = playableZone.Left; playableZoneCollisions[0].p1.Y = playableZone.Top;
            playableZoneCollisions[0].p2.X = playableZone.Right; playableZoneCollisions[0].p2.Y = playableZone.Top;
            playableZoneCollisions[1].p1.X = playableZone.Right; playableZoneCollisions[1].p1.Y = playableZone.Top;
            playableZoneCollisions[1].p2.X = playableZone.Right; playableZoneCollisions[1].p2.Y = playableZone.Bottom;
            playableZoneCollisions[2].p1.X = playableZone.Right; playableZoneCollisions[2].p1.Y = playableZone.Bottom;
            playableZoneCollisions[2].p2.X = playableZone.Left; playableZoneCollisions[2].p2.Y = playableZone.Bottom;
            playableZoneCollisions[3].p1.X = playableZone.Left; playableZoneCollisions[3].p1.Y = playableZone.Bottom;
            playableZoneCollisions[3].p2.X = playableZone.Left; playableZoneCollisions[3].p2.Y = playableZone.Top;
        }
        public static Vector2 getScreenLeftBottomCorner()
        {
            return new Vector2(screen.Left, screen.Top);
        }
        public static Vector2 getScreenCenter()
        {
            return new Vector2((screen.Right - screen.Left) / 2, (screen.Bottom - screen.Top) / 2);
        }
        public static Vector2 getSpriteBatchCoordinate(Vector2 cursor)
        {
            float X = (cursor.X - screen.Left) / screen.Width;
            float Y = (cursor.Y - screen.Top) / screen.Height;
            return new Vector2(SB.width * X, SB.height - SB.height * Y);
        }
        public static Vector2 getWorldCoordinate(Vector2 coord)
        {
            float X = (screen.Right - screen.Left) * (coord.X / SB.width);
            float Y = (screen.Bottom - screen.Top) * (coord.Y / SB.height);
            return new Vector2(screen.Left + X, screen.Top + Y);
        }
        public static Vector2 getWorldSize(Vector2 size)
        {
            float X = (screen.Right - screen.Left) * (size.X / SB.width);
            float Y = (screen.Bottom - screen.Top) * (size.Y / SB.height);
            return new Vector2(X, Y);
        }

        public bool isVisible(Rectangle rectangle)
        {
            return screen.Intersects(rectangle);
        }
        public bool isVisible(Vector2 pos, int radius)
        {
            return screen.Intersects(new Rectangle((int)(pos.X - radius), (int)(pos.Y - radius), radius + radius, radius + radius));
        }
        public bool isInPlayableMargins(Vector2 p, int bodyRadius)
        {
            // dice si está en los márgenes cercanos a la zona de juego
            return !CoolizionManager.pointVSrectangle(p, screen, bodyRadius) && isPlayable(p);
        }
        public bool isPlayable(Vector2 p)
        {
            return (p.X > playableZone.X && p.X < playableZone.X + playableZone.Width
                 && p.Y > playableZone.Y && p.Y < playableZone.Y + playableZone.Height);
        }
        public static bool isPlayable(Rectangle rectangle)
        {
            return playableZone.Contains(rectangle);
        }

        public static bool keepInXLeft(Vector2 pos)
        {
            return (pos.X - 100 > screen.Left);
        }
        public static bool keepInXRight(Vector2 pos)
        {
            return (pos.X + 100 < screen.Right);
        }
        public static bool isPointIn(Vector2 pos)
        {
            return (pos.X > screen.Left && pos.X < screen.Right && pos.Y > screen.Top && pos.Y < screen.Bottom);
        }
        public static bool isPointInX(Vector2 pos)
        {
            return (pos.X > screen.Left && pos.X < screen.Right);
        }
        public static bool isPointInPlayableZone(Vector2 pos)
        {
            return (pos.X > playableZone.Left && pos.X < playableZone.Right && pos.Y > playableZone.Top && pos.Y < playableZone.Bottom);
        }
    }
}