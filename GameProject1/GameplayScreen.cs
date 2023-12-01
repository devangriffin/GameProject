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
using GameProject1.Content;

namespace GameProject1
{
    public class GameplayScreen
    {
        private GraphicsDeviceManager graphics;

        private List<Coin> coins;
        private List<Alien> aliens;
        private CueBall coinBall;
        private CueBall alienBall;
        private BoxCharacter boxMan;

        private Texture2D background;
        private SpriteFont font;
        private SoundEffect coinPickup;
        private SoundEffect bounce;
        private SoundEffect alienSound;
        private Firework firework;
        private Song music;

        private bool GameOver = false;

        private int coinsCollected = 0;
        private int endAmount;
        private int CoinCount = 3;
        private int maxAlienCount = 20;
        private int alienCount = 10;
        private int level = 1;

        public float seconds = 0;
        public int minutes = 0;       

        private GameProject1 game;

        public GameplayScreen(GraphicsDeviceManager graphics, SpriteFont font)
        {
            this.graphics = graphics;
            this.font = font;
        }

        public void Initialize(int endAmount, GameProject1 game)
        {
            boxMan = new BoxCharacter();
            coins = new List<Coin>();
            aliens = new List<Alien>();
            for (int i = 0; i < CoinCount; i++) { coins.Add(new Coin(graphics)); }
            for (int i = 0; i < maxAlienCount; i++) { aliens.Add(new Alien(boxMan.Position)); }
            coinBall = new CueBall(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Color.White);
            alienBall = new CueBall(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Color.Red);

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

            boxMan.LoadContent(c);
            foreach (Coin coin in coins) { coin.LoadContent(c); }
            foreach (Alien alien in aliens) { alien.LoadContent(c); }
            coinBall.LoadContent(c, bounce);
            alienBall.LoadContent(c, bounce);

            music = c.Load<Song>("SpaceMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
        }

        public void Update(GameTime gameTime)
        {
            boxMan.Update(gameTime);

            for (int i = 0; i < alienCount; i++)
            {
                aliens[i].Update(boxMan.Position);
                if (alienBall.HitBox.Collides(aliens[i].HitBox))
                {
                    if (aliens[i].HitBox.IsColliding == false)
                    {
                        Bounce(alienBall, aliens[i].HitBox);
                        aliens[i].NewPosition(boxMan.Position);
                        alienSound.Play();
                    }

                    aliens[i].HitBox.IsColliding = true;
                }
                else { aliens[i].HitBox.IsColliding = false; }

                if (boxMan.HitBox.Collides(aliens[i].HitBox))
                {
                    ResetGamePlay();
                }
            }
            
            if (!GameOver) { coinBall.Update(gameTime); alienBall.Update(gameTime); }

            SpaceManBallCollision(coinBall, boxMan.HitBox);
            SpaceManBallCollision(alienBall, boxMan.HitBox);

            foreach (Coin coin in coins)
            {
                if (coinBall.HitBox.Collides(coin.HitBox))
                {
                    coinPickup.Play();
                    firework.PlaceFirework(coin.Position);
                    coin.NewCoinPosition();
                    coinsCollected++;
                }
            }
        }

        public bool Draw(GameTime gameTime, SpriteBatch sb)
        {
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > 60) 
            { 
                minutes++;
                seconds -= 60;            
            }
            
            if (coinsCollected < endAmount)
            {
                //sb.Draw(background, new Rectangle(300, 300, 400, 300), new Rectangle(0, 0, 300, 300), Color.White);
                sb.Draw(background, new Vector2(0, 0), Color.White);
                boxMan.Draw(gameTime, sb);

                foreach (Coin coin in coins) { coin.Draw(gameTime, sb); }
                for (int i = 0; i < alienCount; i++) { aliens[i].Draw(sb); } 
                coinBall.Draw(gameTime, sb);
                alienBall.Draw(gameTime, sb);

                sb.DrawString(font, "Coins Collected: " + coinsCollected, new Vector2(0, 0), Color.Gold);
                if (seconds < 10) { sb.DrawString(font, "Time: " + minutes + ":0" + (int)seconds, new Vector2(0, graphics.PreferredBackBufferHeight - 30), Color.Gold); }
                else { sb.DrawString(font, "Time: " + minutes + ":" + (int)seconds, new Vector2(0, graphics.PreferredBackBufferHeight - 30), Color.Gold); }
                sb.DrawString(font, "Level " + level, new Vector2(graphics.PreferredBackBufferWidth - 60, 0), Color.Gold);

                return true;
            }
            else
            {
                if ((game.RecordMinutes * 60) + game.RecordSeconds > (minutes * 60) + seconds || game.RecordMinutes == -1)
                {
                    game.RecordMinutes = minutes;
                    game.RecordSeconds = (int)seconds;
                }
                return false;
            }
        }

        public void ResetGamePlay()
        {
            coinsCollected = 0;
            minutes = 0;
            seconds = 0;

            boxMan.ResetPosition();
            foreach (Alien alien in aliens) { alien.NewPosition(boxMan.Position); }
            foreach (Coin coin in coins) { }
        }

        private void Bounce(CueBall cueBall, BoundingRectangle squareHitBox)
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
    }
}
