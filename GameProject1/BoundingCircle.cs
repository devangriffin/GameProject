using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameProject1
{
    public class BoundingCircle
    {
        /// <summary>
        /// The center of the circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// The radius of the circle
        /// </summary>
        public float Radius;

        public bool IsColliding = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Collides(BoundingCircle collidingCircle)
        {
            return Math.Pow(Radius + collidingCircle.Radius, 2) >=
                Math.Pow(Center.X - collidingCircle.Center.X, 2) +
                Math.Pow(Center.Y - collidingCircle.Center.Y, 2);
        }

        public bool Collides(BoundingRectangle collidingRectangle)
        {
            float nearestX = MathHelper.Clamp(Center.X, collidingRectangle.Left, collidingRectangle.Right);
            float nearestY = MathHelper.Clamp(Center.Y, collidingRectangle.Top, collidingRectangle.Bottom);

            return Math.Pow(Radius, 2) >=
                Math.Pow(Center.X - nearestX, 2) +
                Math.Pow(Center.Y - nearestY, 2);
        }
    }
}
