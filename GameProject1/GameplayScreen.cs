﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace GameProject1
{
    public class GameplayScreen
    {
        private GraphicsDeviceManager graphics;

        private Coin coin;
        private CueBall cueBall;
        private bool Colliding = false;
        private bool CueColliding = false;
        private bool GameOver = false;
        private Texture2D background;
        private SpriteFont font;
        private int coinsCollected = 0;
        private int endAmount;
        private BoxCharacter boxMan;

        private Song music;
        private SoundEffect coinPickup;
        private SoundEffect bounce;

        public GameplayScreen(GraphicsDeviceManager graphics, SpriteFont font)
        {
            this.graphics = graphics;
            this.font = font;
        }

        public void Initialize(int endAmount)
        {
            boxMan = new BoxCharacter();
            coin = new Coin();
            cueBall = new CueBall(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            this.endAmount = endAmount;
        }

        public void Load(ContentManager c)
        {
            boxMan.LoadContent(c);
            coin.LoadContent(c);
            cueBall.LoadContent(c);
            background = c.Load<Texture2D>("Space1");
            coinPickup = c.Load<SoundEffect>("coinPickup");
            bounce = c.Load<SoundEffect>("bounce");
            music = c.Load<Song>("SpaceMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
        }

        public void Update(GameTime gameTime)
        {
            boxMan.Update(gameTime);
            if (!GameOver) { cueBall.Update(gameTime, bounce); }

            if (cueBall.HitBox.Collides(boxMan.HitBox))
            {
                Colliding = true;

                // Makes sure it doesn't bounce multiple times while colliding
                if (CueColliding == false)
                {
                    bounce.Play();

                    float DistanceToX = 0;
                    float DistanceToY = 0;

                    // Decides which direction the ball goes after bouncing off the character
                    if (cueBall.HitBox.Center.X < boxMan.HitBox.X) { DistanceToX = boxMan.HitBox.X - cueBall.HitBox.Center.X; }
                    else if (cueBall.HitBox.Center.X > boxMan.HitBox.X + boxMan.HitBox.Width) { DistanceToX = cueBall.HitBox.Center.X - (boxMan.HitBox.X + boxMan.HitBox.Width); }
                    if (cueBall.HitBox.Center.Y < boxMan.HitBox.Y) { DistanceToY = boxMan.HitBox.Y - cueBall.HitBox.Center.Y; }
                    else if (cueBall.HitBox.Center.Y > boxMan.HitBox.Y + boxMan.HitBox.Height) { DistanceToY = cueBall.HitBox.Center.Y - (boxMan.HitBox.Y + boxMan.HitBox.Height); }

                    if (DistanceToY > DistanceToX) { cueBall.Velocity.Y *= -1; }
                    else { cueBall.Velocity.X *= -1; }

                    CueColliding = true;
                }
            }
            else
            {
                Colliding = false;
                CueColliding = false;
            }

            if (cueBall.HitBox.Collides(coin.HitBox))
            {
                coinPickup.Play();
                coin.MoveCoin();
                coinsCollected++;
            }
        }

        public bool Draw(GameTime gameTime, SpriteBatch sb)
        {
            int minutes = gameTime.TotalGameTime.Minutes;
            int seconds = gameTime.TotalGameTime.Seconds;

            if (coinsCollected < endAmount)
            {
                sb.Draw(background, new Vector2(0, 0), Color.White);
                boxMan.Draw(gameTime, sb, Colliding);
                coin.Draw(gameTime, sb);
                cueBall.Draw(gameTime, sb);
                sb.DrawString(font, "Coins Collected: " + coinsCollected, new Vector2(0, 0), Color.White);
                if (seconds < 10) { sb.DrawString(font, "Time: " + minutes + ":0" + seconds, new Vector2(0, 456), Color.White); }
                else { sb.DrawString(font, "Time: " + minutes + ":" + seconds, new Vector2(0, 456), Color.White); }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}