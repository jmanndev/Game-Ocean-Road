using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OceanRoadWinMono
{
    class Sprite
    {
        public SpriteBatch spriteBatch;
        public Texture2D texture;
        public Vector2 position;
        int height;
        int width;
        public Rectangle rectangle;
        public Color color;

        public Sprite(SpriteBatch sp, Texture2D tex, Color color, Vector2 pos, int height, int width)
        {
            this.spriteBatch = sp;
            this.texture = tex;
            this.position = pos;
            this.height = height;
            this.width = width;
            this.color = color;


            UpdateRectangle();
        }

        void UpdateRectangle()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public virtual void Update()
        {
            UpdateRectangle();
        }

        public virtual void Draw()
        {
            spriteBatch.Draw(texture, rectangle, color);
        }

    }
}
