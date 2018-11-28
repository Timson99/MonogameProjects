using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    public class Game1 : Game
    {
        bool fullscreen = true;
        private static Rectangle screenDimensions = new Rectangle(0, 0, 0, 0);
        private static Rectangle windowDimensions = new Rectangle(0, 0, 0, 0);
        public static int screenWidth { get { return screenDimensions.Width; } }
        public static int screenHeight { get { return screenDimensions.Height; } }
        public static int windowWidth { get { return windowDimensions.Width; } }
        public static int windowHeight { get { return windowDimensions.Height; } }
        public const int gameWidth = 1280;
        public const int gameHeight = 720;
        RenderTarget2D rt;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player = new Player();



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ////////*Set Conditions
            graphics.IsFullScreen = fullscreen;
            //IsMouseVisible = true;

            ///////*Set correct Aspect Ratio to match computer size
            screenDimensions.Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenDimensions.Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;


            int idealWidth;
            int idealHeight = gameHeight;
            float aspectRatio = ((float)screenWidth / (float)screenHeight);
            idealWidth = (int)Math.Round(idealHeight * aspectRatio);
            if (idealWidth % 2 != 0)
                idealWidth++;


            windowDimensions.Width = (!fullscreen ? idealWidth : screenWidth);
            windowDimensions.Height = (!fullscreen ? idealHeight : screenHeight);
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;

           
        }

        protected override void Initialize()
        {
            base.Initialize();
            rt = new RenderTarget2D(GraphicsDevice, gameWidth, gameHeight);
            GameServices.AddService<GraphicsDevice>(GraphicsDevice);
            GameServices.AddService<GraphicsDeviceManager>(graphics);
            

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameServices.AddService<SpriteBatch>(spriteBatch);
            GameServices.AddService<ContentManager>(Content);
            player.LoadContent();
            Projectile.LoadContent();
            Enemy.LoadContent();
            Enemy.enemies.Add(new Snake(new Vector2(2, 2)));
        }

        protected override void UnloadContent()
        {
   
        }

        protected override void Update(GameTime gameTime)
        {
            GameServices.RemoveService<GameTime>();
            GameServices.AddService<GameTime>(gameTime);
            
            //Update calls
            player.Update();
            foreach (Projectile proj in Projectile.projectiles)
                proj.Update();
            foreach (Enemy enemy in Enemy.enemies)
                enemy.Update(player.Position);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(rt);
            GraphicsDevice.Clear(Color.ForestGreen);

            
            spriteBatch.Begin();
            player.Draw();
            foreach (Projectile proj in Projectile.projectiles)
                proj.Draw();
            foreach (Enemy enemy in Enemy.enemies)
                enemy.Draw();
            spriteBatch.End();

            //End of Drawing
            //Scaling of Render Target
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(rt, windowDimensions, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
