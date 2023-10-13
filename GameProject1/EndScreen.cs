using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1
{
    public class EndScreen
    {
        private SpriteFont font;

        private int endAmount;
        private int endSeconds;
        private int endMinutes;

        public EndScreen(SpriteFont font, int endAmount) 
        { 
            this.font = font;
            this.endAmount = endAmount;
        }

        public void Load(GameTime gameTime)
        {
            endMinutes = (int)gameTime.TotalGameTime.TotalMinutes;
            endSeconds = (int)gameTime.TotalGameTime.TotalSeconds - (endMinutes * 60);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //if (GameOver == false) { endSeconds = gameTime.TotalGameTime.Seconds; endMinutes = gameTime.TotalGameTime.Minutes; }
            //GameOver = true;

            if (endSeconds < 10) { sb.DrawString(font, "Collected " + endAmount + " Coins in " + endMinutes + ":0" + endSeconds + "!", new Vector2(80, 140), Color.Gold, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0); }
            else { sb.DrawString(font, "Collected " + endAmount + " Coins in " + endMinutes + ":" + endSeconds + "!", new Vector2(80, 140), Color.Gold, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0); }
        }
    }
}
