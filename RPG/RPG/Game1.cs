using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Game1 : Game
    {
        public static int gameWidth = 1280;
        public static int gameHeight = 720;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player = new Player();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = gameWidth;
            graphics.PreferredBackBufferHeight = gameHeight;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            GameServices.AddService<GraphicsDevice>(GraphicsDevice);
            GameServices.AddService<GraphicsDeviceManager>(graphics);
           

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameServices.AddService<SpriteBatch>(spriteBatch);
            GameServices.AddService<ContentManager>(Content);
            player.LoadContent();
        }

        protected override void UnloadContent()
        {
   
        }

        protected override void Update(GameTime gameTime)
        {
            GameServices.RemoveService<GameTime>();
            GameServices.AddService<GameTime>(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            //Animated Drawings

            player.Draw();

            //Animated Drawing

            //Non Animed Drawings
            spriteBatch.Begin();

            spriteBatch.End();
            //Non Animed Drawings

            base.Draw(gameTime);
        }
    }
}
