using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1.Screens
{
    public class InfoScreen
    {
        private SpriteFont font;

        private string infoString = "- The goal is to survive for as long as possible\n" +
                                    "- Use the keys 'W, A, S, D' to move around and bounce the Cue Ball\n" +
                                    "- Use the Cue Ball to collect coins and destroy Aliens\n" +
                                    "- If you have more than five coins, you can shoot a fire ball with 'Space'\n" +
                                    "- You can aim the fireball using the arrow keys\n" +
                                    "- Good Luck!";

        public InfoScreen(SpriteFont sfont)
        {
            font = sfont;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, "  How To Play: ", new Vector2(320, 100), Color.Gold);
            sb.DrawString(font, infoString, new Vector2(320, 200), Color.Gold);
            sb.DrawString(font, "  Press H to go back to the Menu", new Vector2(320, 600), Color.Gold);
        }
    }
}
