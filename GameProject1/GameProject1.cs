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
        private bool CoinColliding = false;
        private Texture2D box;
        private SpriteFont font;
        private int coinsCollected = 0;
        private BoxCharacter boxMan;

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
            box = Content.Load<Texture2D>("CoinSprite");
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
                // FINISH if (cueBall.HitBox.Center.X >)
                Colliding = true;

                Vector2 collisionAxis = cueBall.HitBox.Center - character.HitBox.Center;
                collisionAxis.Normalize();
                float angle = (float)System.Math.Acos(Vector2.Dot(collisionAxis, Vector2.UnitX));

                Vector2 u0 = Vector2.Transform(cueBall.Velocity, Matrix.CreateRotationZ(-angle));
                Vector2 u1 = new Vector2(400, 400);

                Vector2 v0;

                v0.X = u1.X;
                v0.Y = u0.Y;

                cueBall.Velocity = Vector2.Transform(v0, Matrix.CreateRotationZ(angle));
                cueBall.Velocity.Normalize();
                cueBall.Velocity *= 400;

            }
            else
            {
                Colliding = false;
            }

            if (cueBall.HitBox.Collides(coin.HitBox))
            {
                CoinColliding = true;
                coin = new Coin();
                coin.LoadContent(Content);
                coinsCollected++;
            }
            else
            {
                CoinColliding = false;
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
            character.Draw(gameTime, spriteBatch, Colliding);
            coin.Draw(gameTime, spriteBatch);
            cueBall.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Coins Collected: " + coinsCollected, new Vector2(0, 0), Color.White);
            int time = gameTime.TotalGameTime.Seconds;
            spriteBatch.DrawString(font, "Time: " + time, new Vector2(0, 460), Color.White);
            //spriteBatch.Draw(box, character.HitBox.Center, null, Color.White, 0f, new Vector2(48, 48), new Vector2(1, 1), SpriteEffects.None, 0);
            //spriteBatch.Draw(box, cueBall.HitBox.Center, null, Color.White, 0f, new Vector2(32, 32), new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}