using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class FireBall
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 origin = new Vector2(24, 24);
        private float rotation = 0;
        private double animationTimer;
        private GraphicsDeviceManager graphics;
        private SoundEffect sound;

        public Vector2 Velocity;
        public int Radius = 24;
        public BoundingCircle HitBox;

        public const int SPEED = 1200;

        public bool IsBouncingX = false;
        public bool IsBouncingY = false;

        public bool IsActive = false;

        private KeyboardState oldKeyState;
        private KeyboardState keyState;

        private const int MAXBOUNCE = 20;
        public int Bounces = 0;

        public FireBall(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            position = new Vector2(-200, -200);

            HitBox = new BoundingCircle(new Vector2(position.X, position.Y), Radius);

            keyState = Keyboard.GetState();
        }

        public void LoadContent(ContentManager cm, SoundEffect sfx)
        {
            texture = cm.Load<Texture2D>("FireBall");
            sound = sfx;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            if (IsActive)
            {
                float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
                position += Velocity * t;
                HitBox.Center += Velocity * t;

                if (position.X < 24 || position.X > graphics.PreferredBackBufferWidth - 24)
                {
                    if (IsBouncingX == false)
                    {
                        Velocity.X *= -1;
                    }
                    IsBouncingX = true;
                    Bounces++;
                }
                else { IsBouncingX = false; }
                if (position.Y < 24 || position.Y > graphics.PreferredBackBufferHeight - 24)
                {
                    if (IsBouncingY == false)
                    {
                        Velocity.Y *= -1;
                    }
                    IsBouncingY = true;
                    Bounces++;
                }
                else { IsBouncingY = false; }
            }

            if (Bounces >= MAXBOUNCE)
            {
                ResetFireBall();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.025)
            {
                rotation += 0.25f;
                animationTimer -= 0.025;
            }
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }

        public bool ShootBall(Vector2 playerPosition)
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyUp(Keys.Space) && oldKeyState.IsKeyDown(Keys.Space) && IsActive == false)
            {
                IsActive = true;
                sound.Play();

                if (keyState.IsKeyDown(Keys.Left))
                {
                    if (keyState.IsKeyDown(Keys.Up))
                    {
                        position = new Vector2(playerPosition.X - 48, playerPosition.Y);
                        Velocity = new Vector2(-1, -1);
                    }
                    else if (keyState.IsKeyDown(Keys.Down))
                    {
                        position = new Vector2(playerPosition.X - 48, playerPosition.Y + 96);
                        Velocity = new Vector2(-1, 1);
                    }
                    else
                    {
                        position = new Vector2(playerPosition.X - 48, playerPosition.Y + 48);
                        Velocity = new Vector2(-1, 0);
                    }
                }
                else if (keyState.IsKeyDown(Keys.Right))
                {
                    if (keyState.IsKeyDown(Keys.Up))
                    {
                        position = new Vector2(playerPosition.X + 144, playerPosition.Y);
                        Velocity = new Vector2(1, -1);
                    }
                    else if (keyState.IsKeyDown(Keys.Down))
                    {
                        position = new Vector2(playerPosition.X + 144, playerPosition.Y + 96);
                        Velocity = new Vector2(1, 1);
                    }
                    else
                    {
                        position = new Vector2(playerPosition.X + 144, playerPosition.Y + 48);
                        Velocity = new Vector2(1, 0);
                    }
                }
                else if (keyState.IsKeyDown(Keys.Up))
                {
                    position = new Vector2(playerPosition.X + 48, playerPosition.Y - 48);
                    Velocity = new Vector2(0, -1);
                }
                else if (keyState.IsKeyDown(Keys.Down))
                {
                    position = new Vector2(playerPosition.X + 48, playerPosition.Y + 144);
                    Velocity = new Vector2(0, 1);
                }
                else
                {
                    position = new Vector2(playerPosition.X + 144, playerPosition.Y + 48);
                    Velocity = new Vector2(1, 0);
                }

                HitBox.Center = new Vector2(position.X, position.Y);
                Velocity.Normalize();
                Velocity *= SPEED;

                return true;
            }
            else { return false; }
        }

        public void ResetFireBall()
        {
            Velocity = new Vector2(0, 0);
            position = new Vector2(-200, -200);
            HitBox.Center = new Vector2(-200, -200);
            IsActive = false;
            Bounces = 0;
        }
    }
}
