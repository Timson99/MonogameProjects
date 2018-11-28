using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RPG
{
    class Projectile
    {
        private static Texture2D bullet;
        private Vector2 position;
        public Vector2 Position { get { return position; } }
        private int radius = 15;
        public int Radius { get { return radius; } }
        private int speed = 800;
        private Dir direction;
        public static List<Projectile> projectiles = new List<Projectile>();

        public Projectile(Vector2 position, Dir direction)
        {
            this.position = position;
            this.direction = direction;
        }

        public void Update() { 
        float dt = (float)GameServices.dt;
        
            switch (direction)
            {
                case Dir.Down:
                    position.Y += speed * dt;
                    break;
                case Dir.Up:
                    position.Y -= speed * dt;
                    break;
                case Dir.Left:
                    position.X -= speed * dt;
                    break;
                case Dir.Right:
                    position.X += speed * dt;
                    break;
                default:
                    break;
            }
        }
        public static void LoadContent()
        {
            bullet = GameServices.Content.Load<Texture2D>("Assets/Misc/bullet");
        }
        public void Draw()
        {
            GameServices.spriteBatch.Draw(bullet, new Vector2(position.X - radius, position.Y - radius), Color.White);
        }
    }
}
