using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class GameProject1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //private Character character;
        private Coin coin;
        private CueBall cueBall;
        private bool Colliding = false;
        private bool CueColliding = false;
        private bool GameOver = false;
        private Texture2D background;
        private SpriteFont font;
        private int coinsCollected = 0;
        private int endSeconds = 0;
        private int endMinutes = 0;
        private BoxCharacter boxMan;
        private float collideTimer = 0;

        private const int COINENDAMOUNT = 20;

        public GameProject1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            //character = new Character();
            boxMan = new BoxCharacter();
            coin = new Coin();
            cueBall = new CueBall(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            base.Initialize();
        }
         
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //character.LoadContent(Content);
            boxMan.LoadContent(Content);
            coin.LoadContent(Content);
            cueBall.LoadContent(Content);
            background = Content.Load<Texture2D>("Space1");
            font = Content.Load<SpriteFont>("Bangers");
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            boxMan.Update(gameTime);
            cueBall.Update(gameTime);

            if (cueBall.HitBox.Collides(boxMan.HitBox))
            {
                Colliding = true;

                // Makes sure it doesn't bounce multiple times while colliding
                if (CueColliding == false)
                {
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
                coin = new Coin();
                coin.LoadContent(Content);
                coinsCollected++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws sprites
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            int minutes = gameTime.TotalGameTime.Minutes;
            int seconds = gameTime.TotalGameTime.Seconds;
            if (coinsCollected < COINENDAMOUNT)
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                boxMan.Draw(gameTime, spriteBatch, Colliding);
                coin.Draw(gameTime, spriteBatch);
                cueBall.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(font, "Coins Collected: " + coinsCollected, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "Time: " + minutes + ":" + seconds, new Vector2(0, 456), Color.White);
            }
            else
            {
                if (GameOver == false) { endSeconds = gameTime.TotalGameTime.Seconds; endMinutes = gameTime.TotalGameTime.Minutes; }
                GameOver = true;
                spriteBatch.DrawString(font, "Collected " + COINENDAMOUNT + " Coins in " + endMinutes + ":" + endSeconds + "!", new Vector2(140, 200), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}