using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{   
    public class Ship
    {
        public static Vector2 defaultPos = new Vector2(Game1.graphics.PreferredBackBufferWidth / 2, Game1.graphics.PreferredBackBufferHeight / 2);
        public Vector2 position = defaultPos;
        private Texture2D sShip;
        private int speed = 10 * 60;
        private int height = 100;
        private int width = 68;

        public Ship()
        {

        }
        public void Load()
        {
            sShip = Game1.Assets.Load<Texture2D>("Assets/ship");
        }
        public void Update()
        {
            KeyboardState kState = Keyboard.GetState();
            GamePadState gpState = GamePad.GetState(0);
            float dt = (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
            if (Game1.gameController.inGame)
            {
                if (kState.IsKeyDown(Keys.Up) || gpState.IsButtonDown(Buttons.DPadUp))
                {
                    position.Y -= speed * dt;
                }
                if (kState.IsKeyDown(Keys.Down) || gpState.IsButtonDown(Buttons.DPadDown))
                {
                    position.Y += speed * dt;
                }
                if (kState.IsKeyDown(Keys.Right) || gpState.IsButtonDown(Buttons.DPadRight))
                {
                    position.X += speed * dt;
                }
                if (kState.IsKeyDown(Keys.Left) || gpState.IsButtonDown(Buttons.DPadLeft))
                {
                    position.X -= speed * dt;
                }
            }
            position.Y = Math.Max(position.Y, 50);
            position.Y = Math.Min(position.Y, Game1.graphics.PreferredBackBufferHeight + 1 - 50);
            position.X = Math.Max(position.X, 34);
            position.X = Math.Min(position.X, Game1.graphics.PreferredBackBufferWidth + 1 - 34);
        }
        public void Draw()
        {
            Game1.spriteBatch.Draw(sShip, new Vector2(position.X - width/2, position.Y - height/2), Color.White);
        }


       
    }
}
