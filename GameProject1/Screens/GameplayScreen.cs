using System;
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
using ParticleSystemExample;
using GameProject1.Enums;

namespace GameProject1.Screens
{
    public class GameplayScreen
    {
        private GraphicsDeviceManager graphics;

        // GameMode gameMode;

        private List<Coin> coins;
        private List<Alien> aliens;
        private CueBall coinBall;
        private FireBall fireBall;
        // private CueBall alienBall;
        private BoxCharacter boxMan;

        private Texture2D background;
        private SpriteFont font;
        private SoundEffect coinPickup;
        private SoundEffect bounce;
        private SoundEffect alienSound;
        private SoundEffect fireSound;
        private Firework firework;
        private Song music;

        private bool GameOver = false;

        public int CoinsCollected { get; private set; } = 0;
        private int endAmount;
        private int CoinCount = 3;
        private int maxAlienCount = 100;
        private int alienCount = 1;

        private double addAlienTimer;

        public float seconds = 0;
        public int minutes = 0;

        private GameProject1 game;

        public GameplayScreen(GraphicsDeviceManager graphics, SpriteFont font)
        {
            this.graphics = graphics;
            this.font = font;
            // gameMode = gm;
        }

        public void Initialize(int endAmount, GameProject1 game)
        {
            boxMan = new BoxCharacter(graphics);
            coins = new List<Coin>();
            aliens = new List<Alien>();
            for (int i = 0; i < CoinCount; i++) { coins.Add(new Coin(graphics)); }
            for (int i = 0; i < maxAlienCount; i++) { aliens.Add(new Alien(boxMan.Position, graphics)); }
            coinBall = new CueBall(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Color.White);
            fireBall = new FireBall(graphics);
            // alienBall = new CueBall(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Color.Red);

            this.endAmount = endAmount;
            this.game = game;

            firework = new Firework(game, 40);
            game.Components.Add(firework);
        }

        public void Load(ContentManager c)
        {
            background = c.Load<Texture2D>("Space1");
            coinPickup = c.Load<SoundEffect>("coinPickup");
            bounce = c.Load<SoundEffect>("bounce");
            alienSound = c.Load<SoundEffect>("AlienSound");
            fireSound = c.Load<SoundEffect>("explosion");

            boxMan.LoadContent(c);

            foreach (Coin coin in coins) { coin.LoadContent(c); }

            foreach (Alien alien in aliens) { alien.LoadContent(c); }
            coinBall.LoadContent(c, bounce);
            fireBall.LoadContent(c, fireSound);
            // alienBall.LoadContent(c, bounce);

            // music = c.Load<Song>("SpaceMusic");
            // MediaPlayer.IsRepeating = true;
            // MediaPlayer.Play(music);
        }

        public bool Update(GameTime gameTime)
        {
            boxMan.Update(gameTime);
            if (CoinsCollected >= 5)
            {
                if (fireBall.ShootBall(boxMan.Position)) { CoinsCollected -= 5; }
            }

            for (int i = 0; i < alienCount; i++)
            {
                aliens[i].Update(boxMan.Position);
                if (coinBall.HitBox.Collides(aliens[i].HitBox))
                {
                    if (aliens[i].HitBox.IsColliding == false)
                    {
                        Bounce(coinBall, aliens[i].HitBox);
                        aliens[i].NewPosition(boxMan.Position);
                        alienSound.Play();
                    }

                    aliens[i].HitBox.IsColliding = true;
                }
                else { aliens[i].HitBox.IsColliding = false; }

                if (boxMan.HitBox.Collides(aliens[i].HitBox)) 
                { 
                    UpdateRecordTime();
                    fireBall.ResetFireBall();
                    return true; 
                }

                if (fireBall.HitBox.Collides(aliens[i].HitBox))
                {
                    if (aliens[i].HitBox.IsColliding == false)
                    {
                        FireBallBounce(fireBall, aliens[i].HitBox);
                        aliens[i].NewPosition(boxMan.Position);
                        alienSound.Play();
                        fireBall.Bounces++;
                    }

                    aliens[i].HitBox.IsColliding = true;
                }
                else { aliens[i].HitBox.IsColliding = false; }
            }

            if (!GameOver) { coinBall.Update(gameTime); fireBall.Update(gameTime, boxMan.Position); }

            SpaceManBallCollision(coinBall, boxMan.HitBox);
            SpaceManFireBallCollision(fireBall, boxMan.HitBox);

            foreach (Coin coin in coins)
            {
                if (coinBall.HitBox.Collides(coin.HitBox))
                {
                    coinPickup.Play();
                    firework.PlaceFirework(coin.Position);
                    coin.NewCoinPosition();
                    CoinsCollected++;
                }
            }

            addAlienTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (addAlienTimer > 7)
            {
                alienCount++;
                addAlienTimer -= 7;
            }

            return false;
        }

        public bool Draw(GameTime gameTime, SpriteBatch sb)
        {
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > 60)
            {
                minutes++;
                seconds -= 60;
            }

            // if (CoinsCollected < endAmount)
            //sb.Draw(background, new Rectangle(300, 300, 400, 300), new Rectangle(0, 0, 300, 300), Color.White);
            sb.Draw(background, new Vector2(0, 0), Color.White);
            boxMan.Draw(gameTime, sb);

            foreach (Coin coin in coins) { coin.Draw(gameTime, sb); }
            for (int i = 0; i < alienCount; i++) { aliens[i].Draw(sb); }
            coinBall.Draw(gameTime, sb);
            fireBall.Draw(gameTime, sb);
            // alienBall.Draw(gameTime, sb);

            sb.DrawString(font, "Coins Collected: " + CoinsCollected, new Vector2(0, 0), Color.Gold);
            if (seconds < 10) { sb.DrawString(font, "Time: " + minutes + ":0" + (int)seconds, new Vector2(0, graphics.PreferredBackBufferHeight - 30), Color.Gold); }
            else { sb.DrawString(font, "Time: " + minutes + ":" + (int)seconds, new Vector2(0, graphics.PreferredBackBufferHeight - 30), Color.Gold); }
            // sb.DrawString(font, "Level " + level, new Vector2(graphics.PreferredBackBufferWidth - 60, 0), Color.Gold);

            return true;
        }

        public void ResetGamePlay()
        {
            CoinsCollected = 0;
            minutes = 0;
            seconds = 0;

            boxMan.ResetPosition();
            alienCount = 0;
            foreach (Alien alien in aliens) { alien.NewPosition(boxMan.Position); }
            foreach (Coin coin in coins) { }
        }

        private void Bounce(CueBall cueBall, BoundingRectangle squareHitBox)
        {
            if (cueBall.IsBouncingX == false && cueBall.IsBouncingY == false)
            {
                float DistanceToX = 0;
                float DistanceToY = 0;

                // Decides which direction the ball goes after bouncing off the character
                if (cueBall.HitBox.Center.X < squareHitBox.X) { DistanceToX = squareHitBox.X - cueBall.HitBox.Center.X; }
                else if (cueBall.HitBox.Center.X > squareHitBox.X + squareHitBox.Width) { DistanceToX = cueBall.HitBox.Center.X - (squareHitBox.X + squareHitBox.Width); }
                if (cueBall.HitBox.Center.Y < squareHitBox.Y) { DistanceToY = squareHitBox.Y - cueBall.HitBox.Center.Y; }
                else if (cueBall.HitBox.Center.Y > squareHitBox.Y + squareHitBox.Height) { DistanceToY = cueBall.HitBox.Center.Y - (squareHitBox.Y + squareHitBox.Height); }

                if (DistanceToY > DistanceToX) { cueBall.Velocity.Y *= -1; }
                else { cueBall.Velocity.X *= -1; }

                //cueBall.IsBouncing = true;
            }
            else { /* cueBall.IsBouncing = false; */ }
        }

        private void FireBallBounce(FireBall fBall, BoundingRectangle squareHitBox)
        {
            if (fBall.IsBouncingX == false && fBall.IsBouncingY == false)
            {
                float DistanceToX = 0;
                float DistanceToY = 0;

                // Decides which direction the ball goes after bouncing off the character
                if (fBall.HitBox.Center.X < squareHitBox.X) { DistanceToX = squareHitBox.X - fBall.HitBox.Center.X; }
                else if (fBall.HitBox.Center.X > squareHitBox.X + squareHitBox.Width) { DistanceToX = fBall.HitBox.Center.X - (squareHitBox.X + squareHitBox.Width); }
                if (fBall.HitBox.Center.Y < squareHitBox.Y) { DistanceToY = squareHitBox.Y - fBall.HitBox.Center.Y; }
                else if (fBall.HitBox.Center.Y > squareHitBox.Y + squareHitBox.Height) { DistanceToY = fBall.HitBox.Center.Y - (squareHitBox.Y + squareHitBox.Height); }

                if (DistanceToY > DistanceToX) { fBall.Velocity.Y *= -1; }
                else { fBall.Velocity.X *= -1; }

                fireBall.Bounces++;
                //cueBall.IsBouncing = true;
            }
            else { /* cueBall.IsBouncing = false; */ }
        }

        private void SpaceManBallCollision(CueBall cueBall, BoundingRectangle boxManHitBox)
        {
            if (cueBall.HitBox.Collides(boxManHitBox))
            {
                boxManHitBox.IsColliding = true;

                // Makes sure it doesn't bounce multiple times while colliding
                if (cueBall.HitBox.IsColliding == false)
                {
                    bounce.Play();

                    Bounce(cueBall, boxMan.HitBox);

                    cueBall.HitBox.IsColliding = true;
                }
            }
            else
            {
                boxManHitBox.IsColliding = false;
                cueBall.HitBox.IsColliding = false;
            }
        }

        private void SpaceManFireBallCollision(FireBall fBall, BoundingRectangle boxManHitBox)
        {
            if (fBall.HitBox.Collides(boxManHitBox))
            {
                boxManHitBox.IsColliding = true;

                // Makes sure it doesn't bounce multiple times while colliding
                if (fBall.HitBox.IsColliding == false)
                {
                    bounce.Play();

                    FireBallBounce(fBall, boxMan.HitBox);

                    fBall.HitBox.IsColliding = true;
                }
            }
            else
            {
                boxManHitBox.IsColliding = false;
                fBall.HitBox.IsColliding = false;
            }
        }

        private void UpdateRecordTime()
        {
            /*
            if (gameMode == GameMode.Coin)
            {
                if (game.RecordMinutes * 60 + game.RecordSeconds > minutes * 60 + seconds || game.RecordMinutes == -1)
                {
                    game.RecordMinutes = minutes;
                    game.RecordSeconds = (int)seconds;
                }
            }
            */
            if (game.RecordMinutes * 60 + game.RecordSeconds < minutes * 60 + seconds || game.RecordMinutes == -1)
            {
                game.RecordMinutes = minutes;
                game.RecordSeconds = (int)seconds;
            }
        }
    }
}
