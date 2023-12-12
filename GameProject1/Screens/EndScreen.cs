using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject1.Enums;

namespace GameProject1.Screens
{
    public class EndScreen
    {
        private SpriteFont font;

        private int endAmount;
        private int endSeconds;
        private int endMinutes;

        GameProject1 game;

        public EndScreen(SpriteFont font, int endAmount, GameProject1 game)
        {
            this.font = font;
            this.endAmount = endAmount;

            this.game = game;
        }

        public void Load(GameplayScreen screen)
        {
            endMinutes = screen.minutes;
            endSeconds = (int)screen.seconds;
        }

        public bool Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) { return true; }

            sb.DrawString(font, "Press Space to Restart", new Vector2(380, 340), Color.Red);

            if (game.RecordSeconds < 10) { sb.DrawString(font, "Best Time - " + game.RecordMinutes + ":0" + game.RecordSeconds, new Vector2(0, 0), Color.White); }
            else { sb.DrawString(font, "Best Time - " + game.RecordMinutes + ":" + game.RecordSeconds, new Vector2(0, 0), Color.White); }

            /*
                case GameMode.Coin:
                    if (endSeconds < 10) { sb.DrawString(font, "Collected " + endAmount + " Coins in " + endMinutes + ":0" + endSeconds + "!", new Vector2(240, 240), Color.Gold, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0); }
                    else { sb.DrawString(font, "Collected " + endAmount + " Coins in " + endMinutes + ":" + endSeconds + "!", new Vector2(240, 240), Color.Gold, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0); }
                    break;
            */

            if (endSeconds < 10) { sb.DrawString(font, "You Survived " + endMinutes + ":0" + endSeconds + "!", new Vector2(320, 240), Color.Gold, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0); }
            else { sb.DrawString(font, "You Survived " + endMinutes + ":" + endSeconds + "!", new Vector2(320, 240), Color.Gold, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0); }

            return false;
        }
    }
}
