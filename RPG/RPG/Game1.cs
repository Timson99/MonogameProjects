﻿using System;
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
        private static Rectangle gameDimensions = new Rectangle(0, 0, 0, 0);
        public static int screenWidth { get { return screenDimensions.Width; } }
        public static int screenHeight { get { return screenDimensions.Height; } }
        public static int gameWidth { get { return gameDimensions.Width; } }
        public static int gameHeight { get { return gameDimensions.Height; } }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //SelfDefined Fields
        Player player = new Player();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ////////*Set Conditions
            graphics.IsFullScreen = fullscreen;
            IsMouseVisible = true;

            ///////*Set correct Aspect Ratio to match computer
            screenDimensions.Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenDimensions.Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int idealWidth;
            int idealHeight = 720;
            float aspectRatio = ((float)screenWidth / (float)screenHeight);

            idealWidth = (int)Math.Round(idealHeight * aspectRatio);
            
            if (idealWidth % 2 != 0)
                idealWidth++;

            gameDimensions.Width = (!fullscreen ? idealWidth : screenWidth);
            gameDimensions.Height = (!fullscreen ? idealHeight : screenHeight);
            graphics.PreferredBackBufferWidth = gameWidth;
            graphics.PreferredBackBufferHeight = gameHeight;
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
            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale((float)gameWidth/1280));
            spriteBatch.Begin();

            spriteBatch.End();
            //Non Animated Drawings

            base.Draw(gameTime);

        }
    }
}
