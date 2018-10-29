using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace Spaceship
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch; 
        public static Microsoft.Xna.Framework.Content.ContentManager Assets; //Set in Constructor
        public static GameTime gameTime; //Set in Update
        public static Controller gameController = new Controller();

        SpriteFont spaceFont;
        SpriteFont timerFont;
        Texture2D sBackground;

        
        public static Ship player;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Assets = Content;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            player = new Ship();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Asteroid.Load();
            sBackground = Content.Load<Texture2D>("Assets/space");
            player.Load();
            timerFont = Content.Load<SpriteFont>("Assets/timerFont");
            spaceFont = Content.Load<SpriteFont>("Assets/spaceFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gT)
        {
            gameTime = gT;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            gameController.UpdateAsteroids();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(sBackground,new Vector2(0,0),Color.White);
            if(!gameController.inGame)
            {
                string menuMessage = "Press Enter to Begin!";
                Vector2 sizeOfText = spaceFont.MeasureString(menuMessage);
                spriteBatch.DrawString(spaceFont, menuMessage, 
                    new Vector2(graphics.PreferredBackBufferWidth/2 - sizeOfText.X/2, 200), Color.White);
            }
            
            player.Draw();
            gameController.DrawAsteroids();

            spriteBatch.DrawString(timerFont, "Time: " + Math.Floor(gameController.totalTime).ToString() ,new Vector2(3, 3), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
