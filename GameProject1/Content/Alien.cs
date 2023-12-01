using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1.Content
{
    public class Alien
    {
        private Texture2D texture;
        private Texture2D hitBoxTexture;
        private Vector2 position;
        private Random numGenerator;

        public BoundingRectangle HitBox;
        public float Speed = 0.5f;

        public Alien(Vector2 playerPosition)
        {
            numGenerator = new Random();
            HitBox = new BoundingRectangle(Vector2.Zero, 48, 48);
            NewPosition(playerPosition);
        }

        public void LoadContent(ContentManager content) 
        { 
            texture = content.Load<Texture2D>("Alien");
            hitBoxTexture = content.Load<Texture2D>("Circle");
        }

        public void Update(Vector2 playerPosition)
        {
            // Alien is Left of the Player
            if (position.X < playerPosition.X) { position += new Vector2(Speed, 0); HitBox.X += Speed; }
            // Alien is Right of the Player
            else if (position.X >= playerPosition.X) { position += new Vector2(-Speed, 0); HitBox.X -= Speed; }
            // Alien is Above the Player
            if (position.Y < playerPosition.Y) { position += new Vector2(0, Speed); HitBox.Y += Speed; }
            // Alien is Below the Player
            else if (position.Y >= playerPosition.Y) { position += new Vector2(0, -Speed); HitBox.Y -= Speed; }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void NewPosition(Vector2 playerPosition)
        {
            Vector2 newPosition = new Vector2();

            // Player is on Right Side of Screen
            if (playerPosition.X >= 640) { newPosition.X = numGenerator.NextInt64(-48, 688); }
            // Player is on Left Side of Screen
            else { newPosition.X = numGenerator.NextInt64(688, 1328); }
            // Player is on Top Half of Screen
            if (playerPosition.Y <= 480) { newPosition.Y = numGenerator.NextInt64(528, 1008); }
            // Player is on Bottom Half of Screen
            else { newPosition.Y = numGenerator.NextInt64(-48, 528); }

            HitBox.X = newPosition.X;
            HitBox.Y = newPosition.Y;

            position = newPosition;
        }
    }
}
