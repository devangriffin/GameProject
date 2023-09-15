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
            return Math.Pow(this.Radius + collidingCircle.Radius, 2) >=
                Math.Pow(this.Center.X - collidingCircle.Center.X, 2) +
                Math.Pow(this.Center.Y - collidingCircle.Center.Y, 2);
        }
    }
}
