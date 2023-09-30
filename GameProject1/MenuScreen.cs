using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProject1
{
    public class MenuScreen
    {
        private Texture2D spaceManText;
        private Texture2D startText;

        public MenuScreen() { }

        public void Initialize()
        {

        }
        public bool Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) { return true; }
            else { return false; }
        }

        public void Load(ContentManager c)
        {
            spaceManText = c.Load<Texture2D>("SpaceManText");
            startText = c.Load<Texture2D>("StartText");
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(spaceManText, new Vector2(36, 48), Color.White);
            sb.Draw(startText, new Vector2(104, 168), Color.Red);
        }

    }
}
