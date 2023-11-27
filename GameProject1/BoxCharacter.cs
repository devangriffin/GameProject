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
        private Vector2 position = new Vector2(0, 0);

        public BoundingRectangle HitBox;

        public const int SPEED = 6;

        public BoxCharacter()
        {
            HitBox = new BoundingRectangle(position, 96, 96);
        }

        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="content">The content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("BoxMan");
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) { position += new Vector2(0, -SPEED); HitBox.Y += -SPEED; } // * (float)gameTime.ElapsedGameTime.TotalSeconds}
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S)) { position += new Vector2(0, SPEED); HitBox.Y += SPEED; }
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A)) { position += new Vector2(-SPEED, 0); HitBox.X += -SPEED; }
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) { position += new Vector2(SPEED, 0); HitBox.X += SPEED; }
        }

        /// <summary>
        /// Draws the sprites in the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool Colliding)
        {
            Color color = Color.White;
            if (Colliding) { color = Color.Red; }
            spriteBatch.Draw(texture, position, color);
        }
    }
}
