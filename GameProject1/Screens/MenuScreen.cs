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

namespace GameProject1.Screens
{
    public class MenuScreen
    {
        private Texture2D spaceBounceText;
        private Texture2D startText;

        private Cube cube;
        private SpriteFont font;

        float timer = 0;

        public MenuScreen() { }

        public void Initialize(Game game, SpriteFont sfont)
        {
            cube = new Cube(game);
            font = sfont;
        }

        public void Load(ContentManager c)
        {
            spaceBounceText = c.Load<Texture2D>("SpaceBounceText");
            startText = c.Load<Texture2D>("StartText");
        }

        public bool Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) { return true; }
            else
            {
                cube.Update(gameTime);
                return false;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(spaceBounceText, new Vector2(280, 100), Color.White);

            sb.Draw(startText, new Vector2(320, 240), Color.Red);

            sb.DrawString(font, "Collect " + GameProject1.COINENDAMOUNT + " Coins To Win!", new Vector2(520, 340), Color.Gold);
            /*
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0.5f)
            {
                sb.Draw(startText, new Vector2(104, 168), Color.Red);
                if (timer > 1f)
                {
                    timer -= 1f;
                }
            }
            */

            cube.Draw();
        }

    }
}
