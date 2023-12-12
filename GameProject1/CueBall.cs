using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class CueBall
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 origin = new Vector2(32, 32);
        private float rotation = 0;
        private double animationTimer;
        private int graphicsWidth;
        private int graphicsHeight;
        private SoundEffect sound;
        private Color ballColor;

        public Vector2 Velocity;
        public int Radius = 32;
        public BoundingCircle HitBox;

        public int Speed = 800;

        public bool IsBouncingX = false;
        public bool IsBouncingY = false;

        /// <summary>
        /// Constructor for the cue ball
        /// </summary>
        /// <param name="width">The width of the screen</param>
        /// <param name="height">The height of the screen</param>
        public CueBall(int width, int height, Color color)
        {
            graphicsWidth = width;
            graphicsHeight = height;
            ballColor = color;

            position = new Vector2(300, 300);

            Velocity = new Vector2(1, 1);
            Velocity.Normalize();
            Velocity *= Speed;

            HitBox = new BoundingCircle(new Vector2(position.X, position.Y), Radius);
        }

        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="cm">The content manager</param>
        public void LoadContent(ContentManager cm, SoundEffect bounce)
        {
            texture = cm.Load<Texture2D>("CueBall2");
            sound = bounce;
        }

        /// <summary>
        /// Updates the cue ball
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += Velocity * t;
            HitBox.Center += Velocity * t;

            if (position.X < 32 || position.X > graphicsWidth - 32)
            {
                if (IsBouncingX == false)
                {
                    Velocity.X *= -1;
                    sound.Play();
                }
                IsBouncingX = true;
            }
            else { IsBouncingX = false; }
            if (position.Y < 32 || position.Y > graphicsHeight - 32) 
            {
                if (IsBouncingY == false)
                {
                    Velocity.Y *= -1;
                    sound.Play();
                }
                IsBouncingY = true;
            }  
            else { IsBouncingY = false; }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.05)
            {
                rotation += 0.25f;
                animationTimer -= 0.05;
            }
            spriteBatch.Draw(texture, position, null, ballColor, rotation, origin, 1, SpriteEffects.None, 0);
        }

        /*
        public bool CollidesWith(Character c)
        {
            return Math.Pow(Radius + c.Radius, 2) >= Math.Pow(c.)
        }
        */
    }
}
