using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class GameProject1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Character character;
        private Coin coin;
        private CueBall cueBall;

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
            character = new Character();
            coin = new Coin();
            cueBall = new CueBall(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            character.LoadContent(Content);
            coin.LoadContent(Content);
            cueBall.LoadContent(Content);
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            character.Update(gameTime);
            cueBall.Update(gameTime);


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
            character.Draw(gameTime, spriteBatch);
            coin.Draw(gameTime, spriteBatch);
            cueBall.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}