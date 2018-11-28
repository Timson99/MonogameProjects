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
    class Enemy
    {
        public static Texture2D snake;
        public static Texture2D eye;
        public Vector2 position;
        protected int health;
        public int Health { get { return health; } set { health = value; } }
        protected int speed;
        public int Speed { get { return speed; } }
        protected int radius;
        //public int Radius { get { return radius; } }
        public static List<Enemy> enemies = new List<Enemy>();

        public Enemy(Vector2 position) { this.position = position; }
        public static void LoadContent()
        {
            snake = GameServices.Content.Load<Texture2D>("Assets/Enemies/snakeEnemy");
            eye = GameServices.Content.Load<Texture2D>("Assets/Enemies/eyeEnemy");
        }
        public void Update(Vector2 playerPos)
        {
            float dt = (float)GameServices.dt;
            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();
            position += moveDir * dt * speed;
        }

        public virtual void Draw() { }

    }

    class Snake : Enemy
    {
        public Snake(Vector2 position) : base(position) { radius = 42; speed = 160; health = 100; }

        public override void Draw() { GameServices.spriteBatch.Draw(snake, position - new Vector2(radius, radius), Color.White); }
    }
    class Eye : Enemy
    {
        public Eye(Vector2 position) : base(position) { radius = 45; speed = 80; health = 100; }

        public override void Draw() { GameServices.spriteBatch.Draw(eye, position - new Vector2(radius, radius), Color.White); }
     }

    
}
