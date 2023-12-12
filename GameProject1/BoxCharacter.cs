using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1
{
    public class BoxCharacter
    {
        private KeyboardState keyState;
        private Texture2D texture;
        private GraphicsDeviceManager graphics;
        
        public Vector2 Position { get; private set; }
        public BoundingRectangle HitBox;
        public float CharacterSpeed = 6f;

        public BoxCharacter(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            Position = new Vector2(0, 0);
            HitBox = new BoundingRectangle(Position, 96, 96);           
        }

        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="content">The content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("SpaceMan2");
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W)) 
            {
                if (Position.Y > -48) { Position += new Vector2(0, -CharacterSpeed); HitBox.Y += -CharacterSpeed; }
            } // * (float)gameTime.ElapsedGameTime.TotalSeconds}
            if (keyState.IsKeyDown(Keys.S)) 
            {
                if (Position.Y < graphics.PreferredBackBufferHeight - 48) { Position += new Vector2(0, CharacterSpeed); HitBox.Y += CharacterSpeed; }
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                if (Position.X > -48) { Position += new Vector2(-CharacterSpeed, 0); HitBox.X += -CharacterSpeed; }
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                if (Position.X < graphics.PreferredBackBufferWidth - 48) { Position += new Vector2(CharacterSpeed, 0); HitBox.X += CharacterSpeed; }
            }
        }

        /// <summary>
        /// Draws the sprites in the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public void ResetPosition() { Position = Vector2.Zero; HitBox.X = 0; HitBox.Y = 0; }
    }
}
