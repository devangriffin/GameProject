using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject1
{
    public class Cube
    {
        VertexBuffer vertices;

        IndexBuffer indices;

        BasicEffect effect;

        Game game;

        public Cube(Game game)
        {
            this.game = game;
            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
        }

        public void Update(GameTime gameTime)
        {
            float angle = (float)gameTime.TotalGameTime.TotalSeconds;
            // Original (0, 5, -10)
            effect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(new Vector3(0, 7, -20),
                new Vector3(0, 0, 15), Vector3.Up);
        }

        public void Draw()
        {
            effect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.SetVertexBuffer(vertices);
            game.GraphicsDevice.Indices = indices;
            game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);
        }

        public void InitializeVertices()
        {
            var vertexData = new VertexPositionColor[]
            {
                new VertexPositionColor() { Position = new Vector3(-3, 3, -3), Color = Color.Red },
                new VertexPositionColor() { Position = new Vector3(3, 3, -3), Color = Color.Orange },
                new VertexPositionColor() { Position = new Vector3(-3, -3, -3), Color = Color.Yellow },
                new VertexPositionColor() { Position = new Vector3(3, -3, -3), Color = Color.Green },
                new VertexPositionColor() { Position = new Vector3(-3, 3, 3), Color = Color.Blue },
                new VertexPositionColor() { Position = new Vector3(3, 3, 3), Color = Color.Indigo },
                new VertexPositionColor() { Position = new Vector3(-3, -3, 3), Color = Color.Violet },
                new VertexPositionColor() { Position = new Vector3(3, -3, 3), Color = Color.Pink },
            };
            vertices = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.None);
            vertices.SetData<VertexPositionColor>(vertexData);
        }

        public void InitializeIndices()
        {
            var indexData = new short[]
            {
                0, 1, 2, // Side 0
                2, 1, 3,
                4, 0, 6, // Side 1
                6, 0, 2,
                7, 5, 6, // Side 2
                6, 5, 4,
                3, 1, 7, // Side 3
                7, 1, 5,
                4, 5, 0, // Side 4
                0, 5, 1,
                3, 7, 2, // Side 5
                2, 7, 6
            };
            indices = new IndexBuffer(game.GraphicsDevice, IndexElementSize.SixteenBits, 36, BufferUsage.None);
            indices.SetData<short>(indexData);
        }

        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            // Original (0, 0, 4)
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 4), new Vector3(0, 0, 0), Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                game.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100.0f);
            effect.VertexColorEnabled = true;
        }
    }
}
