using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1
{
    public class Character
    {
        private KeyboardState keyState;
        private Texture2D texture;
        private Vector2 position = new Vector2(0, 0);  

        public int Radius = 48;
        public BoundingCircle HitBox;

        public Character()
        {
            HitBox = new BoundingCircle(new Vector2(position.X + Radius, position.Y + Radius), Radius);
        }

        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="content">The content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Yellow");
        }

        /// <summary>
        /// Updates the game
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            int speed = 3;

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) { position += new Vector2(0, -speed); HitBox.Center += new Vector2(0, -speed); } // * (float)gameTime.ElapsedGameTime.TotalSeconds}
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S)) { position += new Vector2(0, speed); HitBox.Center += new Vector2(0, speed); }
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A)) { position += new Vector2(-speed, 0); HitBox.Center += new Vector2(-speed, 0); }
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) { position += new Vector2(speed, 0); HitBox.Center += new Vector2(speed, 0); }
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
