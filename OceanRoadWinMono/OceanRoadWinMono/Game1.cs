#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace OceanRoadWinMono
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
        {
        enum EGameState
        {
            GAME,
            PAUSE
        }
        EGameState gameState;

        GraphicsDeviceManager graphics;
        SpriteFont gameFont;
        SpriteFont gameFontLarge;
        SpriteBatch spriteBatch;
        Random rand = new Random();

        int windowWidth = 960;
        int windowHeight = 640;

        bool slowMo;
        float maxSpeedMulti = 1.02f;

        //time delay for multiCombo and controls at respawn
        float delayTime = 1.5f;
        float timeDelay;
        float showControlsDelay;
       

        //spawn
        float spawn = 0;
        float spawnMultiplier;

        //road details
        List<Road> roadList = new List<Road>();
        Texture2D roadTexture;
        float roadStartY = 120f;
        Vector2 roadSize = new Vector2(/*width*/ 1000, /*height*/ 80);
        Color roadColor1 = new Color(116, 178, 236);
        Color roadColor2 = new Color(162, 242, 237);

        //car details
        List<Car> carList = new List<Car>();
        int lastCarCreated = 0;
        int lastColorCreated = 0;

        //sedan
        Texture2D sedanTexture;
        Texture2D sedanShadow;
        Texture2D sedanWindow;
        Vector2 sedanSize = new Vector2(/*width*/ 110, /*height*/ 54);

        //vw
        Texture2D vwTexture;
        Texture2D vwShadow;
        Texture2D vwWindow;
        Vector2 vwSize = new Vector2(/*width*/ 105, /*height*/ 56);

        //mclaren
        Texture2D mcLarenTexture;
        Texture2D mcLarenShadow;
        Texture2D mcLarenWindow;
        Vector2 mcLarenSize = new Vector2(/*width*/ 120, /*height*/ 60);

        //mustang
        Texture2D mustangTexture;
        Texture2D mustangShadow;
        Texture2D mustangWindow;
        Vector2 mustangSize = new Vector2(/*width*/ 120, /*height*/ 48);

        //porsche
        Texture2D porscheTexture;
        Texture2D porscheShadow;
        Texture2D porscheWindow;
        Vector2 porscheSize = new Vector2(/*width*/ 127, /*height*/ 50);

        //peurgeot
        Texture2D peugeotTexture;
        Texture2D peugeotShadow;
        Texture2D peugeotWindow;
        Vector2 peugeotSize = new Vector2(/*width*/ 127, /*height*/ 50);
        
        
        //nearMissAnimation nearMissList animation list
        Animation nearMissAnimationTop;
        Animation nearMissAnimationBottom;
        Animation nearMissAnimationFront;


        List<Animation> nearMissAnimationList = new List<Animation>();
        //scoreboard declarations LOGIC
        int nearMissTotal;
        int comboBonusPoints;
        float timeCounter;
        int timeScore;
        int comboMultiplierTimedReset;
        int nosDivide;
        int highestNearMissEver;
        int highestNearMissRound;
        //scoreboard results
        int scoreUpdate;
        int highScore;
        int lastScore;


        //player info
        Player player;
        Color playerColor = Color.White;
        Vector2 playerPosition = new Vector2(200, 320);
        Vector2 playerSize = new Vector2(/*width*/ 80, /*height*/ 30);
        Texture2D playerStraightTexture;
        Texture2D playerDownTexture;
        Texture2D playerUpTexture;
        Texture2D playerStraightShadowTexture;
        Texture2D playerDownShadowTexture;
        Texture2D playerUpShadowTexture;

        //energyBar
        Texture2D energyBarTexture;
        Texture2D energyTexture;
        Rectangle energyBarRectangle;
        Rectangle energyRectangle;
        Rectangle energySourceRectangle;
        float energyBarPercentage;
        //counterBar
        Texture2D counterBarTexture;
        Texture2D counterTexture;
        Rectangle counterBarRectangle;
        Rectangle counterRectangle;
        Rectangle counterSourceRectangle;
        float counterBarPercentage;


        //background 
        Texture2D backgroundPic;
        Texture2D backgroundPic2;
        Texture2D backgroundPic3;
        Texture2D backgroundPic4;
        Texture2D backgroundPic5;

        Rectangle backgroundRec;
        Rectangle backgroundRec2;
        Rectangle backgroundRec3;
        Rectangle backgroundRec4;
        Rectangle backgroundRec5;

        Vector2 backgroundRecPos;
        Vector2 backgroundRecPos2;
        Vector2 backgroundRecPos3;
        Vector2 backgroundRecPos4;
        Vector2 backgroundRecPos5;

        float bgScroll = 20f;

        //background beach scroll animation
        AnimationBeach beachAnimation1;
        AnimationBeach beachAnimation2;
        AnimationBeach beachAnimation3;

        Vector2 beachAnimationPosition1;
        Vector2 beachAnimationPosition2;
        Vector2 beachAnimationPosition3;

        //sound
        //SoundEffect bgMusic;
        SoundEffect nearMissBeepSFX;


        //title
        Texture2D titleOceanRoadTexture;
        Texture2D titleOceanRoadTextureBlue;
        Texture2D titleOceanRoadTextureGreen;
        Texture2D titleOceanRoadTextureOrange;
        Rectangle titleRectangle;
        bool titleSwitch;
        int titleCount = 0;
        Vector2 titlePosition = new Vector2(30, -250);



        //pause
        bool pauseLastPress;
        Keys pauseKey = Keys.P;

        
        


        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameState = EGameState.GAME;
            base.Initialize();
           

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //energyBar
            //energyBarLogoTexture = Content.Load<Texture2D>("gui/energyLogo");
            energyBarTexture = Content.Load<Texture2D>("gui/GlassBar");
            energyTexture = Content.Load<Texture2D>("gui/energyWhite");

            counterBarTexture = Content.Load<Texture2D>("gui/energyWhite");
            counterTexture = Content.Load<Texture2D>("gui/energyWhite");
            


            //player info
            playerStraightTexture = Content.Load<Texture2D>("player/riderStraight");
            playerUpTexture = Content.Load<Texture2D>("player/riderUp");
            playerDownTexture = Content.Load<Texture2D>("player/riderDown");
            playerStraightShadowTexture = Content.Load<Texture2D>("player/riderStraightShadow");
            playerUpShadowTexture = Content.Load<Texture2D>("player/riderUpShadow");
            playerDownShadowTexture = Content.Load<Texture2D>("player/riderDownShadow");

            player = new Player(this, graphics, spriteBatch, playerStraightTexture, playerUpTexture, playerDownTexture, playerStraightShadowTexture,
                playerUpShadowTexture, playerDownShadowTexture, playerColor, playerPosition, (int)playerSize.Y, (int)playerSize.X);

            player.nearRecTexTop = Content.Load<Texture2D>("square");
            player.nearRecTexBottom = Content.Load<Texture2D>("square");
            player.nearRecTexFront = Content.Load<Texture2D>("square");

            //sedan info
            sedanTexture = Content.Load<Texture2D>("cars/sedan");
            sedanWindow = Content.Load<Texture2D>("cars/sedanWindow");
            sedanShadow = Content.Load<Texture2D>("cars/sedanShadow");

            //vw info
            vwTexture = Content.Load<Texture2D>("cars/vw");
            vwWindow = Content.Load<Texture2D>("cars/vwWindow");
            vwShadow = Content.Load<Texture2D>("cars/vwShadow");

            //mcLaren info
            mcLarenTexture = Content.Load<Texture2D>("cars/mclaren");
            mcLarenWindow = Content.Load<Texture2D>("cars/mclarenWindow");
            mcLarenShadow = Content.Load<Texture2D>("cars/mclarenShadow");

            //mustang info
            mustangTexture = Content.Load<Texture2D>("cars/mustang");
            mustangWindow = Content.Load<Texture2D>("cars/mustangWindow");
            mustangShadow = Content.Load<Texture2D>("cars/mustangShadow");

            //porsche info
            porscheTexture = Content.Load<Texture2D>("cars/porsche");
            porscheWindow = Content.Load<Texture2D>("cars/porscheWindow");
            porscheShadow = Content.Load<Texture2D>("cars/porscheShadow");

            //peurgeot info
            peugeotTexture = Content.Load<Texture2D>("cars/peugeot");
            peugeotWindow = Content.Load<Texture2D>("cars/peugeotWindow");
            peugeotShadow = Content.Load<Texture2D>("cars/peugeotShadow");

            //game font
            gameFont = Content.Load<SpriteFont>("gameFont");
            gameFontLarge = Content.Load<SpriteFont>("gameFontLarge");
            //gameTitle load images
                titleOceanRoadTextureBlue = Content.Load<Texture2D>("title/blueTitle");
                titleOceanRoadTextureGreen = Content.Load<Texture2D>("title/greenTitle");
                titleOceanRoadTextureOrange = Content.Load<Texture2D>("title/orangeTitle2");


          
            
            

            //road
            roadTexture = Content.Load<Texture2D>("road");

            GenerateRoad();


            //background Code
            backgroundPic = Content.Load<Texture2D>("bg/background1");
            backgroundPic2 = Content.Load<Texture2D>("bg/background2");
            backgroundPic3 = Content.Load<Texture2D>("bg/background3");
            backgroundPic4 = Content.Load<Texture2D>("bg/background4");
            backgroundPic5 = Content.Load<Texture2D>("bg/background5");

            backgroundRec = new Rectangle((int)backgroundRecPos.X, (int)backgroundRecPos.Y, 1200, 640);

            backgroundRecPos2 = new Vector2(backgroundRecPos.X + backgroundRec.Width, 0);
            backgroundRec2 = new Rectangle((int)backgroundRecPos2.X, (int)backgroundRecPos2.Y, 1200, 640);

            backgroundRecPos3 = new Vector2(backgroundRecPos2.X + backgroundRec.Width, 0);
            backgroundRec3 = new Rectangle((int)backgroundRecPos3.X, (int)backgroundRecPos3.Y, 1200, 640);

            backgroundRecPos4 = new Vector2(backgroundRecPos3.X + backgroundRec.Width, 0);
            backgroundRec4 = new Rectangle((int)backgroundRecPos4.X, (int)backgroundRecPos4.Y, 1200, 640);

            backgroundRecPos5 = new Vector2(backgroundRecPos4.X + backgroundRec.Width, 0);
            backgroundRec5 = new Rectangle((int)backgroundRecPos5.X, (int)backgroundRecPos5.Y, 1200, 640);


            //background beach
            beachAnimationPosition1 = new Vector2(0f, getRoadBottomY());
            beachAnimation1 = new AnimationBeach(Content, "beachSpriteSheet2", beachAnimationPosition1, graphics.PreferredBackBufferWidth, 80, 120f, new Vector2(7,4), true);

            beachAnimationPosition2 = new Vector2(beachAnimationPosition1.X + beachAnimation1.frameWidth, beachAnimationPosition1.Y);
            beachAnimation2 = new AnimationBeach(Content, "beachSpriteSheet2", beachAnimationPosition2, graphics.PreferredBackBufferWidth, 80, 120f, new Vector2(7, 4), true);

            beachAnimationPosition3 = new Vector2(beachAnimationPosition2.X + beachAnimation1.frameWidth, beachAnimationPosition1.Y);
            beachAnimation3 = new AnimationBeach(Content, "beachSpriteSheet2", beachAnimationPosition3, graphics.PreferredBackBufferWidth, 80, 120f, new Vector2(7, 4), true);

            //bgMusic = Content.Load<SoundEffect>("sound/californiaBG");
            nearMissBeepSFX = Content.Load<SoundEffect>("sound/NearMiss");

            
            
        }

        void GenerateRoad()
        {
            Road tempRoad;
            Color tempRoadColor;
            Vector2 tempRoadPosition = new Vector2(0, roadStartY);
            //lanes, lane numbers, how many lanes
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                    tempRoadColor = roadColor1;
                else
                    tempRoadColor = roadColor2;

                tempRoad = new Road(spriteBatch, roadTexture, tempRoadColor, tempRoadPosition, (int)roadSize.X, (int)roadSize.Y);
                roadList.Add(tempRoad);
                tempRoadPosition.Y += roadSize.Y;
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case EGameState.GAME:
                    UpdateGame(gameTime);
                    break;
                case EGameState.PAUSE:
                    UpdatePause(gameTime);
                    break;
                default:
                    break;
            }
        }

        void UpdateGame(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            UpdateScoreboard(gameTime);
            player.Update(gameTime);
            UpdateControl();
            UpdatePauseControl();
            TimeDelays(gameTime);
            SpawnCars(gameTime);
            CheckNearMiss();
            ChangeTitle();

            if (!titleSwitch)
            {
                if (titlePosition.Y < 150)
                {
                    titlePosition.Y += 10;
                }
                
                if (titlePosition.Y > 150)
                {
                    titlePosition.Y = 150;
                }
            }

            energyBarPercentage = player.energy / 2000f * 100f;

            if (energyBarPercentage >= 100)
                energyBarPercentage = 100;

            //energy Bar Rectangle
           // energyBarLogoRectangle = new Rectangle(30, 20, energyBarLogoTexture.Width, energyBarLogoTexture.Height);
            energyBarRectangle = new Rectangle(30, 20, energyBarTexture.Width, energyBarTexture.Height);
            energySourceRectangle = new Rectangle(0, 0, (int)(3.41 * energyBarPercentage), 32);
            energyRectangle = new Rectangle(30, 20, energySourceRectangle.Width, energyTexture.Height);


            counterBarPercentage = timeDelay / delayTime * 100;
            //sulo
            counterBarRectangle = new Rectangle(30, 69, energyBarTexture.Width, 9);
            counterSourceRectangle = new Rectangle(0, 0, (int)(3.41 * counterBarPercentage), 5);
            counterRectangle = new Rectangle(30, 71, counterSourceRectangle.Width, 5);

            //test
            foreach (Animation nearMissAnim in nearMissAnimationList)
                nearMissAnim.Update(gameTime);


            for (int i = 0; i < nearMissAnimationList.Count; i++)
            {
                if (nearMissAnimationList[i].currentFrame >= nearMissAnimationList[i].numberOfFrames - 1)
                {
                    nearMissAnimationList.RemoveAt(i);
                }
            }
            
            //endTest

            foreach (Car car in carList)
                car.Update(gameTime);

            foreach (Road road in roadList)
                road.Update();

            CheckCollisions();
            CheckEnemies();
            CarToCarCollision();

            UpdateBackground(gameTime);
            UpdateScoreboard(gameTime);

            base.Update(gameTime);
        }

        void UpdatePause(GameTime gameTime)
        {
            UpdatePauseControl();
        }

        void UpdatePauseControl()
        {
            if (Keyboard.GetState().IsKeyUp(pauseKey))
            {
                pauseLastPress = false;
            }

            if (!pauseLastPress && Keyboard.GetState().IsKeyDown(pauseKey))
            {
                if (gameState == EGameState.GAME)
                    gameState = EGameState.PAUSE;
                else if (gameState == EGameState.PAUSE)
                    gameState = EGameState.GAME;

                pauseLastPress = true;
            }
        }

        void UpdateControl()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                player.energy += 1000;
                //spawnMultiplier = 15;
                comboMultiplierTimedReset++;
                timeDelay = delayTime;
                
            }

            //revive
            if (!player.isAlive && Keyboard.GetState().IsKeyDown(Keys.E))
            {
                player.Revive();
                timeCounter = 0;
                nearMissTotal = 0;
                comboMultiplierTimedReset = 1;
                showControlsDelay = 1.7f;
                comboBonusPoints = 0;
                highestNearMissRound = 0;
                titleSwitch = true;

  

                carList.Clear();

            }

            if (player.isAlive)
            {
                //slowmo
                if (player.energy > 0 && Keyboard.GetState().IsKeyDown(Keys.A) || player.energy > 0 && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    if (player.canSlowMo)
                    {
                        foreach (Car car in carList)
                            if (car.moveSpeedMultiplier >= 0.7f)
                            {
                                car.moveSpeedMultiplier *= 0.95f;
                            }
                        player.GoSlowMo();
                        slowMo = true;
                    }
                    //background scroll SLOW
                    if (bgScroll >= 4)
                    {
                        bgScroll *= 0.95f;
                    }
                }

                //fastmo
                else if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    if (player.canFastMo == true)
                    {
                        foreach (Car car in carList)
                            car.moveSpeedMultiplier *= maxSpeedMulti;
                        player.GoFastMo();
                        slowMo = false;
                    }
                    //background scroll FAST
                    if (bgScroll <= 32)
                    {
                        bgScroll *= 1.7f;
                    }
                }
                //regular speed

                else
                {
                    foreach (Car car in carList)
                        car.moveSpeedMultiplier = 2f;
                    //background scroll DEFAULT
                    bgScroll = 20f;
                    slowMo = false;
                    player.GoRegularSpeed();
                }
            }
        }

        void UpdateBackground(GameTime gameTime)
        {
            beachAnimation1.Update(gameTime);
            beachAnimation2.Update(gameTime);
            beachAnimation3.Update(gameTime);
            
            //background Code
            backgroundRec = new Rectangle((int)backgroundRecPos.X, (int)backgroundRecPos.Y, 1200, 640);
            backgroundRec2 = new Rectangle((int)backgroundRecPos2.X, (int)backgroundRecPos2.Y, 1200, 640);
            backgroundRec3 = new Rectangle((int)backgroundRecPos3.X, (int)backgroundRecPos3.Y, 1200, 640);
            backgroundRec4 = new Rectangle((int)backgroundRecPos4.X, (int)backgroundRecPos4.Y, 1200, 640);
            backgroundRec5 = new Rectangle((int)backgroundRecPos5.X, (int)backgroundRecPos5.Y, 1200, 640);

            

            backgroundRecPos.X -= bgScroll;
            backgroundRecPos2.X -= bgScroll;
            backgroundRecPos3.X -= bgScroll;
            backgroundRecPos4.X -= bgScroll;
            backgroundRecPos5.X -= bgScroll;

            //background beach scroll
            beachAnimation1.position.X -= bgScroll;
            beachAnimation2.position.X -= bgScroll;
            beachAnimation3.position.X -= bgScroll;

            
            //background code  LOOP 
            if (backgroundRecPos.X <= -1600)
                backgroundRecPos.X = backgroundRecPos5.X + backgroundRec.Width;

            if (backgroundRecPos2.X <= -1600)
                backgroundRecPos2.X = backgroundRecPos.X + backgroundRec.Width;

            if (backgroundRecPos3.X <= -1600)
                backgroundRecPos3.X = backgroundRecPos2.X + backgroundRec.Width;

            if (backgroundRecPos4.X <= -1600)
                backgroundRecPos4.X = backgroundRecPos3.X + backgroundRec.Width;

            if (backgroundRecPos5.X <= -1600)
                backgroundRecPos5.X = backgroundRecPos4.X + backgroundRec.Width;

            //background beachAnimation Loop
            if (beachAnimation1.position.X <= -1200)
                beachAnimation1.position.X = beachAnimation3.position.X + beachAnimation1.rectangle.Width;
            if (beachAnimation2.position.X <= -1200)
                beachAnimation2.position.X = beachAnimation1.position.X + beachAnimation1.rectangle.Width;
            if (beachAnimation3.position.X <= -1200)
                beachAnimation3.position.X = beachAnimation2.position.X + beachAnimation1.rectangle.Width;

        }


        void UpdateScoreboard(GameTime gameTime)
        {
            

            if (player.isAlive == true)
                timeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            timeScore = (((int)timeCounter * 10) + nearMissTotal + comboBonusPoints) ;


            nosDivide = player.nosBonusPoints / 5;
            

            if (timeDelay <= 0)
            {
                comboMultiplierTimedReset = 0;
            }
            
            scoreUpdate = timeScore + nosDivide;
            //scoreUpdate = comboBonusPoints;

            
            if (scoreUpdate > highScore)
                highScore = scoreUpdate;

            if (scoreUpdate > 0)
            {
                lastScore = scoreUpdate;
            }

            //nearMiss highscore and round near miss highscore
            if (comboMultiplierTimedReset > highestNearMissRound)
                highestNearMissRound = comboMultiplierTimedReset;

            if (highestNearMissRound > highestNearMissEver)
                highestNearMissEver = highestNearMissRound;




        }

        void ChangeTitle()
        {
            if (titleCount == 0)
            {
                titleOceanRoadTexture = titleOceanRoadTextureBlue;

            }
            if (titleCount == 1)
            {
                titleOceanRoadTexture = titleOceanRoadTextureGreen;

            }
            //titleOceanRoadTexture = Content.Load<Texture2D>("title/orangeTitle");
            if (titleCount == 2)
            {
                titleOceanRoadTexture = titleOceanRoadTextureOrange;

            }

          
          
            if (titleSwitch)
            {
                titleRectangle = new Rectangle(20, 30, titleOceanRoadTexture.Width / 3, titleOceanRoadTexture.Height / 3);
            }

            //titleswitch first title movedown rectangle
            if (!titleSwitch)
            {
                titleRectangle = new Rectangle((int)titlePosition.X, (int)titlePosition.Y, titleOceanRoadTexture.Width, titleOceanRoadTexture.Height);
            }

          

        }
        void SpawnCars(GameTime gameTime)
        {
            if (player.isAlive == true && slowMo == false)
            {
                if (carList.Count < 7)
                {
                    spawnMultiplier += ((float)gameTime.ElapsedGameTime.TotalSeconds / 2);
                }
                if (carList.Count >= 7)
                {
                    spawnMultiplier += ((float)gameTime.ElapsedGameTime.TotalSeconds / 4);
                }
                if (carList.Count >= 14)
                {
                    spawnMultiplier += ((float)gameTime.ElapsedGameTime.TotalSeconds / 8);
                }
                if (carList.Count >= 18)
                {
                    spawnMultiplier += ((float)gameTime.ElapsedGameTime.TotalSeconds / 10);
                }
            }

            spawn += spawnMultiplier;

            if (player.isAlive && spawn >= 1)
            {
                spawn = 0;              //SPAWN COUNT [ MAX SPAWN ENEMY] COLOR CHANGER NUMBER
                if ((carList.Count < spawnMultiplier) && (carList.Count < 22))
                {
                    RandomizeCar(rand.Next(1, 7), rand.Next(1, 31));
                }
            }
        }

        void RandomizeCar(int car, int color)
        {
            if (car == lastCarCreated)
            {
                car = rand.Next(1, 7);
                lastCarCreated = car;
            }

            if (color == lastColorCreated)
            {
                color = rand.Next(1, 25);
                lastColorCreated = color;
            }

            Texture2D tempCarTexture;
            Texture2D tempCarShadow;
            Texture2D tempCarWindow;
            Color tempCarColor;
            Vector2 tempCarSize;

            switch (color)
            {
                case 1:
                    tempCarColor = Color.Crimson*1.5f;
                    break;
                case 2:
                    tempCarColor = Color.Chartreuse * 1.5f;
                    break;
                case 3:
                    tempCarColor = Color.Goldenrod * 1.5f;
                    break;
                case 4:
                    tempCarColor = Color.Orange * 1.5f;
                    break;
                case 5:
                    tempCarColor = Color.Snow * 1.5f;
                    break;
                case 6:
                    tempCarColor = Color.Yellow * 1.5f;
                    break;
                case 7:
                    tempCarColor = Color.DodgerBlue * 1.5f;
                    break;
                case 8:
                    tempCarColor = Color.BlueViolet * 1.5f;
                    break;
                case 9:
                    tempCarColor = Color.OrangeRed * 1.5f;
                    break;
               case 10:
                    tempCarColor = Color.DeepPink * 1.5f;
                    break;
                case 11:
                    tempCarColor = Color.Magenta * 1.5f;
                    break;
                case 12:
                    tempCarColor = Color.Maroon * 1.5f;
                    break;
                case 13:
                    tempCarColor = Color.DarkSlateGray * 1.5f;
                    break;
                case 14:
                    tempCarColor = Color.Indigo * 1.5f;
                    break;
                case 15:
                    tempCarColor = Color.LightBlue * 1.5f;
                    break;
                case 16:
                    tempCarColor = Color.LightSlateGray * 1.5f;
                    break;
                case 17:
                    tempCarColor = Color.MediumSpringGreen * 1.5f;
                    break;
                case 18:
                    tempCarColor = Color.OldLace * 1.5f;
                    break;
                case 19:
                    tempCarColor = Color.Sienna * 1.5f;
                    break;
                case 20:
                    tempCarColor = Color.NavajoWhite * 1.5f;
                    break;
                case 21:
                    tempCarColor = Color.LemonChiffon * 1.5f;
                    break;
                case 22:
                    tempCarColor = Color.DarkKhaki * 1.5f;
                    break;
                case 23:
                    tempCarColor = Color.Azure * 1.5f;
                    break;
                case 24:
                    tempCarColor = Color.Chartreuse * 1.5f;
                    break;
                case 25:
                    tempCarColor = Color.Yellow * 1.5f;
                    break;
                case 26:
                    tempCarColor = Color.Orange * 1.5f;
                    break;
                case 27:
                    tempCarColor = Color.DarkSlateGray * 1.5f;
                    break;
                case 28:
                    tempCarColor = Color.Crimson * 1.5f;
                    break;
                case 29:
                    tempCarColor = Color.Teal * 1.5f;
                    break;
                   
                    //im actually trying to set up something for the near miss, were thinking of doing it like an enemy in its own list
                    //play a spritesheet at the position of the player near miss at the time and have it moving very slightly on the x as it fades;il show you
                default:
                    tempCarColor = Color.White * 1.5f;
                    break;
            }

            switch (car)
            {
                case 1:
                    tempCarTexture = sedanTexture;
                    tempCarShadow = sedanShadow;
                    tempCarWindow = sedanWindow;
                    tempCarSize = sedanSize;
                    break;
                case 2:
                    tempCarTexture = vwTexture;
                    tempCarShadow = vwShadow;
                    tempCarWindow = vwWindow;
                    tempCarSize = vwSize;
                    break;
                case 3:
                    tempCarTexture = mcLarenTexture;
                    tempCarShadow = mcLarenShadow;
                    tempCarWindow = mcLarenWindow;
                    tempCarSize = mcLarenSize;
                    break;
                case 4:
                    tempCarTexture = mustangTexture;
                    tempCarShadow = mustangShadow;
                    tempCarWindow = mustangWindow;
                    tempCarSize = mustangSize;
                    break;
                case 5:
                    tempCarTexture = porscheTexture;
                    tempCarShadow = porscheShadow;
                    tempCarWindow = porscheWindow;
                    tempCarSize = porscheSize;
                    break;
                case 6:
                    tempCarTexture = peugeotTexture;
                    tempCarShadow = peugeotShadow;
                    tempCarWindow = peugeotWindow;
                    tempCarSize = peugeotSize;
                    break;
                default:
                    tempCarTexture = sedanTexture;
                    tempCarShadow = sedanShadow;
                    tempCarWindow = sedanWindow;
                    tempCarSize = sedanSize;
                    break;
            }

            GenerateCar(tempCarTexture, tempCarShadow, tempCarWindow, tempCarSize, tempCarColor);
        }

        void GenerateCar(Texture2D carTexture, Texture2D shadowTexture, Texture2D windowTexture, Vector2 carSize, Color carColor)
        {
            int laneOffset = rand.Next(-15, 16);

            float laneY = ConvertLaneNumberToYValue(rand.Next(0, 6));
            laneY += ((roadSize.Y - carSize.Y) / 2) + laneOffset;
            // car speeds - enemy speed, 
            int randSpeedX = rand.Next(-7, -5);
            Car tempCar = new Car(spriteBatch, carTexture, shadowTexture, windowTexture, carColor, new Vector2(graphics.PreferredBackBufferWidth + 500, laneY), (int)carSize.Y, (int)carSize.X, new Vector2(randSpeedX, 0));
            carList.Add(tempCar);
        }

        float ConvertLaneNumberToYValue(int laneNumber)
        {
            return roadList[laneNumber].rectangle.Y;
        }

        void CheckCollisions()
        {
            foreach (Car car in carList)
            {
                if (player.intersectRectangle.Intersects(car.intersectRectangle))
                {
                    player.Hit();
                    spawnMultiplier = 0;
                    scoreUpdate = 0;
                    //title Changer
                    if (player.isAlive)
                    {
                        titleCount++;
                        if (titleCount >= 3)
                            titleCount = 0;
                    }
                }
            }
        }

        void CarToCarCollision()
        {
            //car to car collision
            foreach (Car car in carList)
            {
                foreach (Car otherCar in carList)
                {


                    if (car != otherCar && car.GetRectangle().Intersects(otherCar.GetRectangle()))
                    {
                        //enemy.SetPosition(enemy.GetPosition() + new Vector2(-2, 0));
                        if (car.position.X >= otherCar.position.X)
                        {

                            if (otherCar.velocity.X <= -55)
                                otherCar.velocity.X -= 1;


                            car.position.X += 3;
                            otherCar.position.X -= 1;

                            //car.velocity.X = otherCar.velocity.X;
                            otherCar.velocity.X = car.velocity.X;


                        }

                        if (car.position.X <= otherCar.position.X)
                        {
                            car.position.X -= 3;
                            otherCar.position.X += 1;
                            //otherCar.velocity.X = car.velocity.X;
                            car.velocity.X = otherCar.velocity.X;

                            //otherEnemy.velocity.X += -1f;
                        }

                    }
                }
            }

        }

        void CheckEnemies()
        {
            for (int i = 0; i < carList.Count; i++)
            {
                if (!carList[i].isOnScreen())
                {
                    carList.RemoveAt(i);
                }
            }
        }

        void CheckNearMiss()
        {
            foreach (Car e in carList)
            {
                if (player.nearMissRecTop.Intersects(e.rectangle))
                {
                    CheckNearMissTop(e);
                    
                }

                if (player.nearMissRecBottom.Intersects(e.rectangle))
                {
                    CheckNearMissBottom(e);
                }

                if (player.nearMissRecFront.Intersects(e.rectangle))
                {
                    CheckNearMissFront(e);
                }
            }
        }

        void CheckNearMissTop(Car e)
        {
            if (player.isAlive && !player.isHit)
            {
                player.nearMissColorTop = Color.Orange;
                if (!e.comboCounted)
                {
                    NearMiss();
                    e.comboCounted = true;
                    if (e.timeDelay <= 0)
                    {
                        //test
                        //nearMissAnimation
                        nearMissAnimationTop = new Animation(Content, "nearMiss22fr", new Vector2(player.nearRecPosTop.X - 10,
                            player.nearRecPosTop.Y - 70), 149*3/4, 199*3/4, 50f, 21, false);
                        nearMissAnimationList.Add(nearMissAnimationTop);
                        nearMissBeepSFX.Play();
                        //endTest
                        

                        comboMultiplierTimedReset++;
                        //comboBonusPoints combo bonus tiers
                        if ((comboMultiplierTimedReset >= 10) && (comboMultiplierTimedReset <= 19))
                            comboBonusPoints++;
                        if ((comboMultiplierTimedReset >= 20) && (comboMultiplierTimedReset <= 29))
                            comboBonusPoints += 2;
                        if ((comboMultiplierTimedReset >= 30) && (comboMultiplierTimedReset <= 39))
                            comboBonusPoints += 4;
                        if ((comboMultiplierTimedReset >= 40) && (comboMultiplierTimedReset <= 49))
                            comboBonusPoints += 6;
                        if ((comboMultiplierTimedReset >= 50) && (comboMultiplierTimedReset <= 59))
                            comboBonusPoints += 8;
                        if ((comboMultiplierTimedReset >= 60) && (comboMultiplierTimedReset <= 69))
                            comboBonusPoints += 10;
                        if ((comboMultiplierTimedReset >= 70) && (comboMultiplierTimedReset <= 79))
                            comboBonusPoints += 12;
                        if ((comboMultiplierTimedReset >= 80) && (comboMultiplierTimedReset <= 89))
                            comboBonusPoints += 14;
                        if ((comboMultiplierTimedReset >= 90) && (comboMultiplierTimedReset <= 99))
                            comboBonusPoints += 16;
                        if ((comboMultiplierTimedReset >= 100) && (comboMultiplierTimedReset <= 109))
                            comboBonusPoints += 20;
                        if ((comboMultiplierTimedReset >= 110) && (comboMultiplierTimedReset <= 119))
                            comboBonusPoints += 30;
                        if ((comboMultiplierTimedReset >= 120) && (comboMultiplierTimedReset <= 129))
                            comboBonusPoints += 40;
                        if ((comboMultiplierTimedReset >= 130) && (comboMultiplierTimedReset <= 139))
                            comboBonusPoints += 50;
                        if ((comboMultiplierTimedReset >= 140) && (comboMultiplierTimedReset <= 149))
                            comboBonusPoints += 60;
                        if ((comboMultiplierTimedReset >= 150) && (comboMultiplierTimedReset <= 159))
                            comboBonusPoints += 70;
                        if ((comboMultiplierTimedReset >= 160) && (comboMultiplierTimedReset <= 169))
                            comboBonusPoints += 80;
                        if ((comboMultiplierTimedReset >= 170) && (comboMultiplierTimedReset <= 179))
                            comboBonusPoints += 90;
                        if ((comboMultiplierTimedReset >= 180) && (comboMultiplierTimedReset <= 189))
                            comboBonusPoints += 100;
                        if ((comboMultiplierTimedReset >= 190) && (comboMultiplierTimedReset <= 199))
                            comboBonusPoints += 150;
                        if ((comboMultiplierTimedReset >= 200))
                            comboBonusPoints += 200;
                       
                        player.topNearMissTriggered = true;
                    }
                    else
                    {
                        player.topNearMissTriggered = false;
                    }
                }
            }
        }

        void CheckNearMissBottom(Car e)
        {
            if (player.isAlive && !player.isHit)
            {
                player.nearMissColorBottom = Color.Orange;
                if (!e.comboCounted)
                {
                    NearMiss();
                    e.comboCounted = true;
                    if (e.timeDelay <= 0)
                    {
                        //test
                        nearMissAnimationBottom = new Animation(Content, "nearMiss22fr", new Vector2(player.nearRecPosBottom.X - 10,
                            player.nearRecPosBottom.Y - 80), 149*3/4, 199*3/4, 50f, 21, false);
                        nearMissAnimationList.Add(nearMissAnimationBottom);
                        nearMissBeepSFX.Play();
                        //endtest

                        comboMultiplierTimedReset += 1;
                        //comboBonusPoints combo bonus tiers
                        if ((comboMultiplierTimedReset >= 10) && (comboMultiplierTimedReset <= 19))
                            comboBonusPoints++;
                        if ((comboMultiplierTimedReset >= 20) && (comboMultiplierTimedReset <= 29))
                            comboBonusPoints += 2;
                        if ((comboMultiplierTimedReset >= 30) && (comboMultiplierTimedReset <= 39))
                            comboBonusPoints += 4;
                        if ((comboMultiplierTimedReset >= 40) && (comboMultiplierTimedReset <= 49))
                            comboBonusPoints += 6;
                        if ((comboMultiplierTimedReset >= 50) && (comboMultiplierTimedReset <= 59))
                            comboBonusPoints += 8;
                        if ((comboMultiplierTimedReset >= 60) && (comboMultiplierTimedReset <= 69))
                            comboBonusPoints += 10;
                        if ((comboMultiplierTimedReset >= 70) && (comboMultiplierTimedReset <= 79))
                            comboBonusPoints += 12;
                        if ((comboMultiplierTimedReset >= 80) && (comboMultiplierTimedReset <= 89))
                            comboBonusPoints += 14;
                        if ((comboMultiplierTimedReset >= 90) && (comboMultiplierTimedReset <= 99))
                            comboBonusPoints += 16;
                        if ((comboMultiplierTimedReset >= 100) && (comboMultiplierTimedReset <= 109))
                            comboBonusPoints += 20;
                        if ((comboMultiplierTimedReset >= 110) && (comboMultiplierTimedReset <= 119))
                            comboBonusPoints += 30;
                        if ((comboMultiplierTimedReset >= 120) && (comboMultiplierTimedReset <= 129))
                            comboBonusPoints += 40;
                        if ((comboMultiplierTimedReset >= 130) && (comboMultiplierTimedReset <= 139))
                            comboBonusPoints += 50;
                        if ((comboMultiplierTimedReset >= 140) && (comboMultiplierTimedReset <= 149))
                            comboBonusPoints += 60;
                        if ((comboMultiplierTimedReset >= 150) && (comboMultiplierTimedReset <= 159))
                            comboBonusPoints += 70;
                        if ((comboMultiplierTimedReset >= 160) && (comboMultiplierTimedReset <= 169))
                            comboBonusPoints += 80;
                        if ((comboMultiplierTimedReset >= 170) && (comboMultiplierTimedReset <= 179))
                            comboBonusPoints += 90;
                        if ((comboMultiplierTimedReset >= 180) && (comboMultiplierTimedReset <= 189))
                            comboBonusPoints += 100;
                        if ((comboMultiplierTimedReset >= 190) && (comboMultiplierTimedReset <= 199))
                            comboBonusPoints += 150;
                        if ((comboMultiplierTimedReset >= 200))
                            comboBonusPoints += 200;

                        player.bottomNearMissTriggered = true;
                    }
                    else
                    {
                        player.bottomNearMissTriggered = false;
                    }
                }
            }
        }

        void CheckNearMissFront(Car e)
        {
            if (player.isAlive && !player.isHit)
            {
                if (!e.comboCounted)
                {
                    player.nearMissColorFront = Color.Orange;
                    NearMiss();
                    e.comboCounted = true;
                    if (e.timeDelay <= 0)
                    {
                        //test
                        nearMissAnimationFront = new Animation(Content, "nearMiss22fr", new Vector2(player.nearRecPosFront.X + 10,
                            player.nearRecPosFront.Y -95), 149*3/4, 199*3/4, 50f, 21, false);
                        nearMissAnimationList.Add(nearMissAnimationFront);
                        nearMissBeepSFX.Play();

                        //endtest

                        comboMultiplierTimedReset += 1;
                        //comboBonusPoints combo bonus tiers
                        if ((comboMultiplierTimedReset >= 10) && (comboMultiplierTimedReset <= 19))
                            comboBonusPoints++;
                        if ((comboMultiplierTimedReset >= 20) && (comboMultiplierTimedReset <= 29))
                            comboBonusPoints += 2;
                        if ((comboMultiplierTimedReset >= 30) && (comboMultiplierTimedReset <= 39))
                            comboBonusPoints += 4;
                        if ((comboMultiplierTimedReset >= 40) && (comboMultiplierTimedReset <= 49))
                            comboBonusPoints += 6;
                        if ((comboMultiplierTimedReset >= 50) && (comboMultiplierTimedReset <= 59))
                            comboBonusPoints += 8;
                        if ((comboMultiplierTimedReset >= 60) && (comboMultiplierTimedReset <= 69))
                            comboBonusPoints += 10;
                        if ((comboMultiplierTimedReset >= 70) && (comboMultiplierTimedReset <= 79))
                            comboBonusPoints += 12;
                        if ((comboMultiplierTimedReset >= 80) && (comboMultiplierTimedReset <= 89))
                            comboBonusPoints += 14;
                        if ((comboMultiplierTimedReset >= 90) && (comboMultiplierTimedReset <= 99))
                            comboBonusPoints += 16;
                        if ((comboMultiplierTimedReset >= 100) && (comboMultiplierTimedReset <= 109))
                            comboBonusPoints += 20;
                        if ((comboMultiplierTimedReset >= 110) && (comboMultiplierTimedReset <= 119))
                            comboBonusPoints += 30;
                        if ((comboMultiplierTimedReset >= 120) && (comboMultiplierTimedReset <= 129))
                            comboBonusPoints += 40;
                        if ((comboMultiplierTimedReset >= 130) && (comboMultiplierTimedReset <= 139))
                            comboBonusPoints += 50;
                        if ((comboMultiplierTimedReset >= 140) && (comboMultiplierTimedReset <= 149))
                            comboBonusPoints += 60;
                        if ((comboMultiplierTimedReset >= 150) && (comboMultiplierTimedReset <= 159))
                            comboBonusPoints += 70;
                        if ((comboMultiplierTimedReset >= 160) && (comboMultiplierTimedReset <= 169))
                            comboBonusPoints += 80;
                        if ((comboMultiplierTimedReset >= 170) && (comboMultiplierTimedReset <= 179))
                            comboBonusPoints += 90;
                        if ((comboMultiplierTimedReset >= 180) && (comboMultiplierTimedReset <= 189))
                            comboBonusPoints += 100;
                        if ((comboMultiplierTimedReset >= 190) && (comboMultiplierTimedReset <= 199))
                            comboBonusPoints += 150;
                        if ((comboMultiplierTimedReset >= 200))
                            comboBonusPoints += 200;


                        player.frontNearMissTriggered = true;
                    }
                    else
                    {
                        player.frontNearMissTriggered = false;
                    }
                }
            }
        }
        
        void NearMiss()
        {
            player.NearMiss();
            nearMissTotal++;
            timeDelay = delayTime;
           
        }

        //time delay for multiplier and controls show at respawn
        void TimeDelays(GameTime gameTime)
        {
            if (timeDelay > 0)
            {
                timeDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeDelay <= 0)
                {
                    timeDelay = 0;
                }
            }

            if (showControlsDelay > 0)
            {
                showControlsDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (showControlsDelay <= 0)
                {
                    showControlsDelay = 0;

                }
            }

        

        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.LawnGreen);

            spriteBatch.Begin();
            DrawBackground();
            beachAnimation1.Draw(spriteBatch);
            beachAnimation2.Draw(spriteBatch);
            beachAnimation3.Draw(spriteBatch);

            foreach (Road road in roadList)
                road.Draw();

            switch (gameState)
            {
                case EGameState.GAME:
                    DrawGame(gameTime);
                    break;
                case EGameState.PAUSE:
                    DrawPause(gameTime);
                    break;
                default:
                    break;
            }

            DrawScoreBoard(gameTime);
            
            DrawRespawnControls();
            
            


            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawGame(GameTime gameTime)
        {
            //player
            player.Draw();

            //enemies
            foreach (Car car in carList)
                car.Draw();

          
            //test
            foreach (Animation nearMissAnim in nearMissAnimationList)
                nearMissAnim.Draw(spriteBatch);
            //endTest

            if(player.isAlive)
            DrawEnergyMeter();


            
        }

        void DrawPause(GameTime gameTime)
        {

        }

        void DrawBackground()
        {
            //background Code
            spriteBatch.Draw(backgroundPic, backgroundRec, Color.White);
            spriteBatch.Draw(backgroundPic2, backgroundRec2, Color.White);
            spriteBatch.Draw(backgroundPic3, backgroundRec3, Color.White);
            spriteBatch.Draw(backgroundPic4, backgroundRec4, Color.White);
            spriteBatch.Draw(backgroundPic5, backgroundRec5, Color.White);


        }

        void DrawEnergyMeter()
        {
            spriteBatch.Draw(energyTexture, energyRectangle, energySourceRectangle, Color.Crimson * 1.5f);
            spriteBatch.Draw(energyBarTexture, energyBarRectangle, Color.White * 1.5f);

            spriteBatch.Draw(counterBarTexture, counterBarRectangle, Color.Black * 1.5f);
            spriteBatch.Draw(counterTexture, counterRectangle, counterSourceRectangle, Color.Chartreuse * 1.5f);
           
            
            //spriteBatch.Draw(energyBarLogoTexture, energyBarLogoRectangle, Color.White);

        }

        void DrawRespawnControls()
        {
            //Respawn Controls
            if (player.isAlive == true && showControlsDelay > 0)
            {
                string showControlsScreen = @"'W' or 'S' TO MOVE";
                spriteBatch.DrawString(gameFont, showControlsScreen, new Vector2(300, 200), Color.Black * 1.5f);

                string slowControlsScreen = @"'A' TO USE SLOW MO";
                spriteBatch.DrawString(gameFont, slowControlsScreen, new Vector2(300, 250), Color.DodgerBlue * 1f);

                string nosControlsScreen = @" 'D' TO USE NOS";
                spriteBatch.DrawString(gameFont, nosControlsScreen, new Vector2(300, 300), Color.Crimson * 1.5f);

            }

            //press E to respawn Prompt
            if (player.isAlive == false && titleSwitch)
            {

                string ePromptScreen = @" TAP 'E' TO RESTART ";
                spriteBatch.DrawString(gameFont, ePromptScreen, new Vector2(283, 513), Color.Black * 1.5f);
                string ePromptScreenShadow = @" TAP 'E' TO RESTART ";
                spriteBatch.DrawString(gameFont, ePromptScreenShadow, new Vector2(280, 510), Color.White * 1.5f);

                
                string lastScoreScreenShadow = "LAST SCORE = " + lastScore;
                spriteBatch.DrawString(gameFont, lastScoreScreenShadow, new Vector2(357, 230), Color.Black * 1.5f);

                string lastNearMissScreenShadow = "LAST COMBO = " + highestNearMissRound;
                spriteBatch.DrawString(gameFont, lastNearMissScreenShadow, new Vector2(346, 275), Color.Black * 1.5f);

                string highScoreScoreBoardShadow = "HIGHSCORE = " + highScore;
                spriteBatch.DrawString(gameFont, highScoreScoreBoardShadow, new Vector2(380, 320), Color.Black * 1.5f);

                string highestNearMissScreenShadow = "HIGHEST COMBO = " + highestNearMissEver;
                spriteBatch.DrawString(gameFont, highestNearMissScreenShadow, new Vector2(284, 365), Color.Black * 1.5f);

                if (lastScore == highScore)
                {
                    string lastScoreScreen = "LAST SCORE = " + lastScore;
                    spriteBatch.DrawString(gameFont, lastScoreScreen, new Vector2(355, 228), Color.Chartreuse * 1.2f);

                    string highScoreScoreBoard = "HIGHSCORE = " + highScore;
                    spriteBatch.DrawString(gameFont, highScoreScoreBoard, new Vector2(378, 318), Color.Chartreuse * 1.2f);

                }
                else
                {
                    string lastScoreScreen = "LAST SCORE = " + lastScore;
                    spriteBatch.DrawString(gameFont, lastScoreScreen, new Vector2(355, 228), Color.Crimson * 1.5f);

                    string highScoreScoreBoard = "HIGHSCORE = " + highScore;
                    spriteBatch.DrawString(gameFont, highScoreScoreBoard, new Vector2(378, 318), Color.Orange * 1.5f);
                }

                if(highestNearMissRound == highestNearMissEver)
                {
                    string lastNearMissScreen = "LAST COMBO = " + highestNearMissRound;
                    spriteBatch.DrawString(gameFont, lastNearMissScreen, new Vector2(344, 273), Color.Chartreuse * 1f);

                    string highestNearMissScreen = "HIGHEST COMBO = " + highestNearMissEver;
                    spriteBatch.DrawString(gameFont, highestNearMissScreen, new Vector2(282, 363), Color.Chartreuse * 1f);
                }
                else
                {
                    string lastNearMissScreen = "LAST COMBO = " + highestNearMissRound;
                    spriteBatch.DrawString(gameFont, lastNearMissScreen, new Vector2(344, 273), Color.Crimson * 1.5f);

                    string highestNearMissScreen = "HIGHEST COMBO = " + highestNearMissEver;
                    spriteBatch.DrawString(gameFont, highestNearMissScreen, new Vector2(282, 363), Color.Orange * 1.5f);
                }








                spriteBatch.Draw(titleOceanRoadTexture, titleRectangle, Color.White * 1.5f);
                
            }
            if (!player.isAlive && !titleSwitch)
            {
                spriteBatch.Draw(titleOceanRoadTexture, titleRectangle, Color.White * 1.5f);

                string ePromptScreen = @" TAP 'E' TO RESTART ";
                spriteBatch.DrawString(gameFont, ePromptScreen, new Vector2(282, 312), Color.Black * 1.5f);

                string ePromptScreenShadow = @" TAP 'E' TO RESTART ";
                spriteBatch.DrawString(gameFont, ePromptScreenShadow, new Vector2(280, 310), Color.White * 1.5f);
            }

        }


        void DrawScoreBoard(GameTime gameTime)
        {
            //score Variables while alive
            if (player.isAlive == true)
            {
                //string nearMissCounterScoreBoard = " Near Miss Counter = " + nearMissTotal;
                //spriteBatch.DrawString(gameFont, nearMissCounterScoreBoard, new Vector2(400, 70), Color.Black);
                ////score Algo
                //string scoreMultiplierScoreBoard = " NM Combo Multiplier = " + comboMultiplierTimedReset;
                //spriteBatch.DrawString(gameFont, scoreMultiplierScoreBoard, new Vector2(400, 85), Color.Black);
                string scoreUpdateScoreBoard = "" + scoreUpdate;
                spriteBatch.DrawString(gameFont, scoreUpdateScoreBoard, new Vector2(630, 30), Color.Black*1.5f);
                if (scoreUpdate == highScore)
                {
                    string scoreUpdateScoreBoardNewHigh = "" + scoreUpdate;
                    spriteBatch.DrawString(gameFont, scoreUpdateScoreBoardNewHigh, new Vector2(628, 28), Color.Chartreuse*1.5f);
                }
                else
                {
                    string scoreUpdateScoreBoardNewHigh = "" + scoreUpdate;
                    spriteBatch.DrawString(gameFont, scoreUpdateScoreBoardNewHigh, new Vector2(628, 28), Color.White*1.5f);
                }

                if (comboMultiplierTimedReset > 2)
                {
                    //nearMissComboScreen Combo display near miss counterScreen
                    string nearMissComboScreenShadow = "" + comboMultiplierTimedReset;
                    spriteBatch.DrawString(gameFontLarge, nearMissComboScreenShadow, new Vector2(734, 19), Color.Black * 1.5f);
                    string nearMissComboScreen = "" + comboMultiplierTimedReset;
                    spriteBatch.DrawString(gameFontLarge, nearMissComboScreen, new Vector2(730, 15), Color.Crimson*1.5f);
                }
            }

            //string carCountScoreBoard = "Car Count = " + carList.Count();
            //spriteBatch.DrawString(gameFont, carCountScoreBoard, new Vector2(10, 20), Color.Black);

            //string spawnMultiplierScoreBoard = "Car Spawn Multiplier = " + spawnMultiplier;
            //spriteBatch.DrawString(gameFont, spawnMultiplierScoreBoard, new Vector2(10, 30), Color.Black);

            //string spawnScoreBoard = "Car Spawn = " + spawn;
            //spriteBatch.DrawString(gameFont, spawnScoreBoard, new Vector2(10, 40), Color.Black);

            //string energyScoreBoard = "Energy = " + player.energy;
            //spriteBatch.DrawString(gameFont, energyScoreBoard, new Vector2(50, 100), Color.Black);

            //string gameTimeDisplay = "Current Game Time = " + (float)gameTime.TotalGameTime.Minutes + "M  " +
            //    (float)gameTime.TotalGameTime.Seconds + "S  " + (float)gameTime.TotalGameTime.Milliseconds + "Ms";
            //spriteBatch.DrawString(gameFont, gameTimeDisplay, new Vector2(10, 50), Color.Black);

            //string timeDelayDisplay = "Spawn delay = " + timeDelay;
            //spriteBatch.DrawString(gameFont, timeDelayDisplay, new Vector2(400, 30), Color.Black);

           

            

        }

        public float getRoadTopY()
        {
            return roadList[0].rectangle.Top;
        }

        public float getRoadBottomY()
        {
            return roadList[roadList.Count - 1].rectangle.Bottom;
        }
    }
}
