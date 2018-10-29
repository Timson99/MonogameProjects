using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Nez;

namespace shootingGallery
{
    public class Game1 : Game
    {
        //Built in variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Target Variables
        const int TARGET_RADIUS = 45;
        const int CROSSHAIRS_RADIUS = 25;
        Vector2 targetPos = new Vector2(300,300);

        //Sprites
        Texture2D targetSprite;
        Texture2D backgroundSprite;
        Texture2D crosshairsSprite;
        SpriteFont gameFont;

        //Scoring and Mouse 
        MouseState mState;
        bool mReleased = true;
        int score = 0;
        double distFromTarget;

        //Timer
        double timer = 10d;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Makes Mouse visible on app screen
            //IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Loads Content into Texture2D sprite fields
            targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");

        }

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Only decreases Time if greater than 0
            if(timer > 0)
                timer -= gameTime.ElapsedGameTime.TotalSeconds;

            //Increments Score When Mouse Click a single time on a target
            distFromTarget = Vector2.Distance(targetPos,new Vector2(mState.X,mState.Y));
            mState = Mouse.GetState();
            if(mState.LeftButton == ButtonState.Pressed & mReleased) {
                if(distFromTarget < TARGET_RADIUS && timer > 0) {
                    score++;
                    Random rand = new Random();
                    targetPos.X = rand.Next(TARGET_RADIUS, graphics.PreferredBackBufferWidth + 1 - TARGET_RADIUS);
                    targetPos.Y = rand.Next(TARGET_RADIUS, graphics.PreferredBackBufferHeight + 1 - TARGET_RADIUS);
                }
                mReleased = false;
            }
            if (mState.LeftButton == ButtonState.Released) {
                mReleased = true;
            }

            Console.WriteLine(score);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Processes Sprite Batch and prints 2DTextures
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(3,3), Color.White);
            spriteBatch.DrawString(gameFont, "Time: " + Math.Ceiling(timer).ToString(), new Vector2(3, 40), Color.White);
            if (timer > 0)
                spriteBatch.Draw(targetSprite, new Vector2(targetPos.X - TARGET_RADIUS, targetPos.Y - TARGET_RADIUS), Color.White);
            spriteBatch.Draw(crosshairsSprite, new Vector2(mState.X -CROSSHAIRS_RADIUS,mState.Y-CROSSHAIRS_RADIUS), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
