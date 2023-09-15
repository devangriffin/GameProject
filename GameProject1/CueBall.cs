using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1
{
    public class CueBall
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 origin = new Vector2(32, 32);
        private Vector2 velocity;
        private float rotation = 0;
        private double animationTimer;
        private int graphicsWidth;
        private int graphicsHeight;

        public int Radius = 32;

        /// <summary>
        /// Constructor for the cue ball
        /// </summary>
        /// <param name="width">The width of the screen</param>
        /// <param name="height">The height of the screen</param>
        public CueBall(int width, int height)
        {
            graphicsWidth = width;
            graphicsHeight = height;

            Random rand = new Random();
            position = new Vector2(rand.NextInt64(width - 64), rand.NextInt64(height - 64));

            velocity = new Vector2(rand.NextInt64(), rand.NextInt64());
            velocity.Normalize();
            velocity *= 200;
        }

        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="cm">The content manager</param>
        public void LoadContent(ContentManager cm)
        {
            texture = cm.Load<Texture2D>("Cue");
        }

        /// <summary>
        /// Updates the cue ball
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.X < 32 || position.X > graphicsWidth - 32) { velocity.X *= -1; }
            if (position.Y < 32 || position.Y > graphicsHeight - 32) { velocity.Y *= -1; }
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.05)
            {
                rotation += 0.25f;
                animationTimer -= 0.05;
            }
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }

        public bool CollidesWith(Character c)
        {
            return Math.Pow(Radius + c.Radius, 2) >= Math.Pow(c.)
        }
    }
}
