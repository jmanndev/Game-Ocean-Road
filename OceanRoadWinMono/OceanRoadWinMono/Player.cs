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
    class Player : Sprite
    {
        GraphicsDeviceManager graphics;
        Game game;

        Vector2 spawnPosition = new Vector2(200, 320);
        float runSpeed;

        public Rectangle intersectRectangle;

        public bool isAlive = false;
        public bool isHit = false;
        public bool canSlowMo;
        public bool canFastMo;


        //player stats
        int maxEnergy = 2000;
        public int energy = 1000;
        int energyInc = 50;

        //player changing textures
        Texture2D playerStraight;
        Texture2D playerDown;
        Texture2D playerUp;
        //player shadow changing textures
        Texture2D shadowTexture;
        Texture2D playerStraightShadow;
        Texture2D playerDownShadow;
        Texture2D playerUpShadow;

        
        
        //scoreboard [point increase if energy is full and player uses Nos(fastMo)
        public int nosBonusPoints;

        //player outer shell (combo box)
        public Color nearMissColorTop = Color.Blue;
        public Rectangle nearMissRecTop;
        public Vector2 nearRecPosTop;
        public Texture2D nearRecTexTop;

        public Color nearMissColorBottom = Color.Blue;
        public Rectangle nearMissRecBottom;
        public Vector2 nearRecPosBottom;
        public Texture2D nearRecTexBottom;

        public Color nearMissColorFront = Color.Blue;
        public Rectangle nearMissRecFront;
        public Vector2 nearRecPosFront;
        public Texture2D nearRecTexFront;

        public bool topNearMissTriggered = false;
        public bool bottomNearMissTriggered = false;
        public bool frontNearMissTriggered = false;

        public bool tightSqueeze = false;
        public bool deathDefying = false;
        public bool closeShave = false;
        public bool nearMiss = false;

        public Player(Game game, GraphicsDeviceManager graphics, SpriteBatch sp, Texture2D tex, Texture2D texUp, Texture2D texDown, 
            Texture2D texShadow, Texture2D texUpShadow, Texture2D TexDownShadow, Color color, Vector2 pos, int height, int width)
            : base(sp, tex, color, pos, height, width)
        {
            this.game = game;
            this.graphics = graphics;
            this.playerUp = texUp;
            this.playerDown = texDown;
            this.playerStraight = tex;
            this.playerStraightShadow = texShadow;
            this.playerUpShadow = texUpShadow;
            this.playerDownShadow = TexDownShadow;
            this.shadowTexture = texShadow;
        }

        public void Update(GameTime gameTime)
        {
            base.Update();
            UpdateNearMissBoxes();
            CheckEdgeCollision();
            CheckNearMiss();
            UpdateIntersectRectangle();

            if (isAlive)
            {
                UpdatePlayerControls();
            }

            CheckEnergy();
            CheckDeath();

        }

        void UpdateIntersectRectangle()
        {
            intersectRectangle = new Rectangle((int)(rectangle.X + (rectangle.Width * 0.2)), (int)(rectangle.Y + (rectangle.Height * 0.2)), (int) (rectangle.Width * 0.6), (int)(rectangle.Height * 0.6));
        }

        void CheckEdgeCollision()
        {
            if (position.X <= 0)
                position.X = 0;
            if (position.Y <= game.getRoadTopY())
                position.Y = game.getRoadTopY();
            if (position.Y >= game.getRoadBottomY() - rectangle.Height)
                position.Y = game.getRoadBottomY() - rectangle.Height;
            if (position.X >= graphics.PreferredBackBufferWidth - rectangle.Height)
                position.X = graphics.PreferredBackBufferWidth - rectangle.Width;

        }

        void UpdateNearMissBoxes()
        {
            //Top box rectangle
            nearRecPosTop = new Vector2(position.X, position.Y - nearMissRecTop.Height);
            nearMissRecTop = new Rectangle((int)nearRecPosTop.X, (int)nearRecPosTop.Y, 28, 35);

            //Bottom box
            nearRecPosBottom = new Vector2(position.X, position.Y + rectangle.Height);
            nearMissRecBottom = new Rectangle((int)nearRecPosBottom.X, (int)nearRecPosBottom.Y, 28, 35);

            //Front box
            nearRecPosFront = new Vector2(position.X + rectangle.Width, position.Y + (rectangle.Height/2) - (nearMissRecFront.Height / 2));
            nearMissRecFront = new Rectangle((int)nearRecPosFront.X, (int)nearRecPosFront.Y, 40, 28);

            NearMissBoxColor();
        }

        void CheckEnergy()
        {
            if (energy <= 0)
            {
                canSlowMo = false;
                energy = 0;
            }
            else if (energy > 0)
            {
                canSlowMo = true;
                canFastMo = true;
            }

            if (energy >= maxEnergy)
            {
                nosBonusPoints++;
                energy = maxEnergy - 1;
            }
        }

        void CheckDeath()
        {
            if (isHit == true && isAlive == true)
            {
                position = spawnPosition;
                isHit = false;
                isAlive = false;
                texture = playerStraight;
                shadowTexture = playerStraightShadow;
            }
        }

        public void Revive()
        {
            isAlive = true;
            isHit = false;
            energy = 1000;
            nosBonusPoints = 0;
        }

        void UpdatePlayerControls()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {

                position.Y -= runSpeed;
                base.texture = playerUp;
                shadowTexture = playerUpShadow;
                
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += runSpeed;
                base.texture = playerDown;
                shadowTexture = playerDownShadow;
            }
            else
            {
                base.texture = playerStraight;
                shadowTexture = playerStraightShadow;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (position.X >= 200)
                    position.X -= 1f;
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (position.X <= 210)
                    position.X += 1;
            }

           
            
        }

        public void GoSlowMo()
        {
            runSpeed = 3f;
            energy -= 3;
        }

        public void GoFastMo()
        {
            energy += 3;
        }

        public void GoRegularSpeed()
        {
            runSpeed = 5f;
        }

        public void Hit()
        {
            runSpeed = 1f;
            isHit = true;
            energy = 0;
        }

        public void NearMiss()
        {
            energy += energyInc;
        }

        void NearMissBoxColor()
        {
            if (topNearMissTriggered)
                nearMissColorTop = Color.Orange;
            else
                nearMissColorTop = Color.Blue;

            if (bottomNearMissTriggered)
                nearMissColorBottom = Color.Orange;
            else
                nearMissColorBottom = Color.Blue;

            if (frontNearMissTriggered)
                nearMissColorFront = Color.Orange;
            else
                nearMissColorFront = Color.Blue;
        }

        public void CheckNearMiss()
        {
            if (bottomNearMissTriggered)
            {
                if (topNearMissTriggered)
                {
                    if (frontNearMissTriggered) //bottom = true, top = true, front = true
                    {
                        nearMiss = false;
                        closeShave = false;
                        tightSqueeze = false;
                        deathDefying = true;
                    }
                    else //bottom = true, top = true, front = false
                    {
                        tightSqueeze = true;
                        nearMiss = false;
                    }
                }
                else
                {
                    if (frontNearMissTriggered) //bottom = true, top = false, front = true
                    {
                        closeShave = true;
                        nearMiss = false;
                        tightSqueeze = false;
                    }
                    else //bottom = true, top = false, front = false
                    {
                        nearMiss = true;
                    }
                }
            }
            else
            {
                if (topNearMissTriggered)
                {
                    if (frontNearMissTriggered) //bottom = false, top = true, front = true
                    {
                        nearMiss = false;
                        tightSqueeze = false;
                        deathDefying = false;
                        closeShave = true;
                    }
                    else //bottom = false, top = true, front = false
                    {
                        nearMiss = true;
                    }
                }
                else
                {
                    if (frontNearMissTriggered) //bottom = false, top = false, front = true
                    {
                        nearMiss = true;
                    }
                }
            }

            
        }

        public void ComboReset()
        {
            nearMiss = false;
            closeShave = false;
            tightSqueeze = false;
            deathDefying = false;
            topNearMissTriggered = false;
            bottomNearMissTriggered = false;
            frontNearMissTriggered = false;
        }

        public override void Draw()
        {
           //spriteBatch.Draw(nearRecTexTop, nearMissRecTop, nearMissColorTop * 0.1f);
           // spriteBatch.Draw(nearRecTexBottom, nearMissRecBottom, nearMissColorBottom * 0.1f);
           // spriteBatch.Draw(nearRecTexFront, nearMissRecFront, nearMissColorFront * 0.1f);
            spriteBatch.Draw(shadowTexture, new Rectangle(rectangle.X - (rectangle.Width / 12), rectangle.Y - (rectangle.Height / 8),
                rectangle.Width, rectangle.Height), Color.White*0.4f);


            base.Draw();
        }
    }
}
