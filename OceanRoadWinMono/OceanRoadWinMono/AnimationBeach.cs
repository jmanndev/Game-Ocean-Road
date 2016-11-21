using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OceanRoadWinMono
{


    class AnimationBeach
    {
        float frameTime;
        float elapsed;
        Rectangle frameSourceRectangle;
        Texture2D animationTexture;
        Vector2 numberOfFrames;
        //int currentFrame;
        Vector2 currentFramePos = Vector2.Zero;
        public int frameWidth;
        int frameHeight;
        bool looping;


        public Rectangle rectangle;
        public Vector2 position;
        int height;
        int width;

        public AnimationBeach(ContentManager Content, string asset, Vector2 position, int width, int height, float frameSpeed, Vector2 numOfFrames, bool looping)
        {
            this.frameTime = frameSpeed;
            this.numberOfFrames = numOfFrames;
            this.animationTexture = Content.Load<Texture2D>(asset);
            frameWidth = animationTexture.Width / (int)numberOfFrames.X;
            frameHeight = animationTexture.Height / (int)numberOfFrames.Y;
            this.position = position;
            this.width = width;
            this.height = height;
            this.looping = looping;

            UpdateRectangle();
        }

        public void Update(GameTime gameTime)
        {
            PlayAnimBeach(gameTime);
            UpdateRectangle();
        }

        void UpdateRectangle()
        {
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        //public void PlayAnim(GameTime gameTime)
        //{
        //    elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        //    frameSourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

        //    if (elapsed >= frameTime)
        //    {
        //        if (currentFrame >= numberOfFrames - 1)
        //        {
        //            if (looping)
        //                currentFrame = 1;
        //        }
        //        else
        //        {
        //            currentFrame++;
        //        }

        //        elapsed = 0;
        //    }
        //}

        public void PlayAnimBeach(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            frameSourceRectangle = new Rectangle((int)currentFramePos.X * frameWidth, (int)currentFramePos.Y * frameHeight, frameWidth, frameHeight);

            if (elapsed >= frameTime)
            {
                if (looping && currentFramePos.Y >= 3 && currentFramePos.X >= 4)
                {
                    currentFramePos = Vector2.Zero;
                }
                else if (currentFramePos.X >= 6)
                {
                    currentFramePos.Y++;
                    currentFramePos.X = 0;
                }
                else
                {
                    currentFramePos.X++;
                }

                elapsed = 0;
            }
            
        }

            public void Draw(SpriteBatch spriteBatch)
            {
            spriteBatch.Draw(animationTexture,rectangle, frameSourceRectangle, Color.White);
            }


        }


       
    }

