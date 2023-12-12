using GameProject1.Enums;
using GameProject1.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleSystemExample;
using System;
using System.Collections;
using System.Threading;

namespace GameProject1
{
    public class GameProject1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private CurrentScreen currentScreen = CurrentScreen.Menu;
        // public GameMode GameMode = GameMode.Survival;

        private GameplayScreen gameplayScreen;
        private MenuScreen menuScreen;
        private EndScreen endScreen;

        private SpriteFont font;

        public const int COINENDAMOUNT = 10;
        public int CoinsCollected = 0;

        private Song music;

        public int RecordSeconds = -1;
        public int RecordMinutes = -1;

        public GameProject1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 960;
            graphics.PreferredBackBufferWidth = 1280;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            font = Content.Load<SpriteFont>("Bangers");

            gameplayScreen = new GameplayScreen(graphics, font);
            menuScreen = new MenuScreen();
            endScreen = new EndScreen(font, COINENDAMOUNT, this);

            gameplayScreen.Initialize(COINENDAMOUNT, this);
            menuScreen.Initialize(this, font);

            base.Initialize();
        }
         
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuScreen.Load(Content);

            music = Content.Load<Song>("SpaceMusic");
            MediaPlayer.IsRepeating = true;
            // MediaPlayer.Play(music);
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentScreen)
            {
                case CurrentScreen.Menu:
                    if (menuScreen.Update(gameTime))
                    {
                        MediaPlayer.Play(music);
                        currentScreen = CurrentScreen.GamePlay;
                        gameplayScreen.ResetGamePlay();
                        gameplayScreen.Load(Content);
                    }
                    break;
                case CurrentScreen.GamePlay:
                    if (gameplayScreen.Update(gameTime)) { endScreen.Load(gameplayScreen); currentScreen = CurrentScreen.EndScreen; }
                    break;             
                case CurrentScreen.EndScreen:
                    MediaPlayer.Stop();
                    break;
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

            if (currentScreen == CurrentScreen.EndScreen) { spriteBatch.Begin(transformMatrix: Matrix.CreateScale(1.25f)); }
            else { spriteBatch.Begin(); }

            switch (currentScreen)
            {
                case CurrentScreen.GamePlay:
                    if (!gameplayScreen.Draw(gameTime, spriteBatch)) { endScreen.Load(gameplayScreen); currentScreen = CurrentScreen.EndScreen; }
                    break;
                case CurrentScreen.Menu:
                    menuScreen.Draw(gameTime, spriteBatch);
                    break;
                case CurrentScreen.EndScreen:
                    if (endScreen.Draw(gameTime, spriteBatch)) { gameplayScreen.ResetGamePlay(); currentScreen = CurrentScreen.Menu; }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}