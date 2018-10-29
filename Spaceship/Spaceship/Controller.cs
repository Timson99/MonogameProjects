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
    public class Controller
    {
        private List<Asteroid> asteroids = new List<Asteroid>();
        private double maxTime = 2D;
        private double minTime = 0.5D;
        //private double minTime = 0;
        private double timer = 2D;
        private double timeDecrease = 0.1D;
        //private double timeDecrease = 0.2D;
        private int nextSpeed = 240;
        private int maxSpeed = 720;
        //private int maxSpeed = 7200;
        private int speedIncrease = 4;
        //private int speedIncrease = 16;
        public float totalTime = 0F; 

        public bool inGame = false;


        public Controller() {
        }
        public void UpdateAsteroids() {
            if (inGame)
            {
                timer -= Game1.gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)Game1.gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if(kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    totalTime = 0f;
                    timer = 2D;
                    maxTime = 2D;
                    nextSpeed = 240;
                }
            }
            if(timer <= 0)
            {
                asteroids.Add(new Asteroid(nextSpeed));
                timer = maxTime; 
                if(maxTime > minTime)
                    maxTime -= timeDecrease;
                if (nextSpeed < maxSpeed) 
                    nextSpeed += speedIncrease;
              
            }
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Update();
                if (asteroids[i].position.X < 0 - asteroids[i].diameter / 2)
                    asteroids[i].offScreen = true;


                int sum = asteroids[i].diameter / 2 + 30;
                if(Vector2.Distance(asteroids[i].position, Game1.player.position) < sum)
                {
                    inGame = false;
                    Game1.player.position = Ship.defaultPos;
                    asteroids.Clear();
                    break;
                }
            }

            asteroids.RemoveAll(a => a.offScreen == true);
        }
        public void DrawAsteroids() 
        {
            for(int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Draw();
            }   
        }

    }
}
