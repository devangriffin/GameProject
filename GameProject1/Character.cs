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
        private Vector2 position = new Vector2(200, 200);
        
        public int Radius = 48;

        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="content">The content manager</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Yellow");
        }

        public void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W)) { position += new Vector2(0, -1); } // * (float)gameTime.ElapsedGameTime.TotalSeconds}
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S)) { position += new Vector2(0, 1); }
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A)) { position += new Vector2(-1, 0); }
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D)) { position += new Vector2(1, 0); }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
