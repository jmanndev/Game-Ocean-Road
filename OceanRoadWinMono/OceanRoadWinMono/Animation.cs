using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OceanRoadWinMono
{
    class Animation
    {
        float frameTime;
        float elapsed;
        Rectangle frameSourceRectangle;
        Texture2D animationTexture;
        int width;
        int height;
        public int numberOfFrames;
        public int currentFrame;
        public int frameWidth;
        int frameHeight;
        bool looping;

        public Rectangle rectangle;
        public Vector2 position;


        public Animation(ContentManager Content, string asset, Vector2 position,
            int width, int height, float frameSpeed, int numOfFrames, bool looping)
        {
            this.frameTime = frameSpeed;
            this.numberOfFrames = numOfFrames;
            this.animationTexture = Content.Load<Texture2D>(asset);
            frameWidth = ((animationTexture.Width) / numberOfFrames);
            frameHeight = animationTexture.Height;
            this.position = position;
            this.looping = looping;
            this.width = width;
            this.height = height;

            UpdateRectangle();
        }

        public void Update(GameTime gameTime)
        {
            PlayAnim(gameTime);
            UpdateRectangle();
            
        }

        public void PlayAnim(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            frameSourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            if (elapsed >= frameTime)
            {
                if (currentFrame >= numberOfFrames - 1)
                {
                    if (looping)
                        currentFrame = 1;
                }
                else
                {
                    currentFrame++;
                }

                elapsed = 0;
            }

        }
        void UpdateRectangle()
        {
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
            position.X -= 3;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationTexture, rectangle, frameSourceRectangle, Color.Chartreuse * 1.5f);
        }


    }



}

