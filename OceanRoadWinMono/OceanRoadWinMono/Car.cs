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
    class Car : Sprite
    {
        Sprite shadow;
        Sprite window;

        //movement details
        public Vector2 velocity;
        public float moveSpeedMultiplier = 1.0f;
        public float timeDelay = 0.8f;

        public Rectangle intersectRectangle;

        public bool comboCounted = false;

        public Car(SpriteBatch sp, Texture2D carTex, Texture2D shadowTex, Texture2D windowTex, Color color, Vector2 pos, int height, int width, Vector2 velocity)
            : base(sp, carTex, color, pos, height, width)
        {
            shadow = new Sprite(sp, shadowTex, Color.White, pos, height, width);
            window = new Sprite(sp, windowTex, Color.White, pos, height, width);
            this.velocity = velocity;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity * moveSpeedMultiplier;
            UpdateIntersectRectangle();
            UpdateOverlay();
           


            //if (position.X < 0 - enemyRect.Width)
            //    isVisible = false;
            //if (position.Y < 0 - enemyRect.Height || position.Y > graphics.Viewport.Height)
            //    isVisible = false;

            if (timeDelay > 0)
            {
                timeDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeDelay <= 0)
                {
                    timeDelay = 0;
                }
            }

            base.Update();
        }
        

        void UpdateOverlay()
        {
            shadow.position = new Vector2(position.X - (rectangle.Width / 12), position.Y - (rectangle.Height / 8));
            shadow.Update();
            window.position = position;
            window.Update();
        }

        void UpdateIntersectRectangle()
        {
            intersectRectangle = new Rectangle((int)(rectangle.X + (rectangle.Width * 0.1)), (int)(rectangle.Y + (rectangle.Height * 0.1)), (int)(rectangle.Width * 0.8), (int)(rectangle.Height * 0.8));
        }

        public bool isOnScreen()
        {
            if (position.X < 0 - rectangle.Width)
                return false;

            return true;
        }

        public override void Draw()
        {
            shadow.spriteBatch.Draw(shadow.texture, shadow.rectangle, color*0.3f);
            base.Draw();
            window.Draw();
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 a_position)
        {
            position = a_position;
        }

    }
}
