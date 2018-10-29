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
    class Asteroid
    {
        private static Texture2D sAsteroid;
        private int speed;
        public int diameter = 118;
        public Vector2 position;
        private static Random rand = new Random();
        public bool offScreen = false;


        public Asteroid(int speed)
        {
            this.speed = speed;
            position = new Vector2(Game1.graphics.PreferredBackBufferWidth + diameter/2, rand.Next(0,Game1.graphics.PreferredBackBufferHeight+1));
        }
        public static void Load()
        {
            sAsteroid = Game1.Assets.Load<Texture2D>("Assets/asteroid");
        }
        public void Update()
        {
            float dt = (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
        public void Draw()
        {
            Game1.spriteBatch.Draw(sAsteroid, new Vector2(position.X-diameter/2,position.Y-diameter/2), Color.White);
        }
    }
}
