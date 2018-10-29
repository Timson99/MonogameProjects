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
    enum Dir {
        Down,
        Up,
        Left,
        Right
    }

    class Player
    {
        private Texture2D playerSprite;
        private Texture2D playerDownSprite;
        private Texture2D playerUpSprite;
        private Texture2D playerLeftSprite;
        private Texture2D playerRightSprite;

        private int width = 96;
        private int height = 96;


        private Vector2 position = new Vector2(Game1.gameWidth / 2, Game1.gameHeight / 2);
        private int health = 3;
        public int Health { get { return health; } set { health = value; } }
        public Vector2 Position { get { return position; } }
        private bool isMoving = false;
        private Dir direction = Dir.Down;

        private int speed = 200;
        private double animSpeed = 0.1D;
        private AnimatedSprite[] animations = new AnimatedSprite[4];
        private AnimatedSprite currentAnim;

        public Player()
        {

        }

        public void setX(float newX)
        {
            position.X = newX;
        }
        public void setY(float newY)
        {
            position.X = newY;
        }

        public void Update()
        {
            currentAnim = animations[(int)direction];
            if (isMoving)
                currentAnim.Update();
            else
                currentAnim.setCurrentFrame(1);
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)GameServices.dt;

            isMoving = false;
            if(kState.IsKeyDown(Keys.Down)) {
                direction = Dir.Down;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Up)) {
                direction = Dir.Up;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Left)) {
                direction = Dir.Left;
                isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Right)){
                direction = Dir.Right;
                isMoving = true;
            }
            if(isMoving) {
                switch(direction)
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

        }


        public void Draw()
        {
            currentAnim.Draw(GameServices.spriteBatch, position);
        }

        public void LoadContent()
        {
            playerSprite = GameServices.Content.Load<Texture2D>("Player/player");
            playerDownSprite = GameServices.Content.Load<Texture2D>("Player/playerDown");
            playerUpSprite = GameServices.Content.Load<Texture2D>("Player/playerUp");
            playerLeftSprite = GameServices.Content.Load<Texture2D>("Player/playerLeft");
            playerRightSprite = GameServices.Content.Load<Texture2D>("Player/playerRight");

            animations[0] = new AnimatedSprite(playerDownSprite, 1, 4, animSpeed);
            animations[1] = new AnimatedSprite(playerUpSprite, 1, 4, animSpeed);
            animations[2] = new AnimatedSprite(playerLeftSprite, 1, 4, animSpeed);
            animations[3] = new AnimatedSprite(playerRightSprite, 1, 4, animSpeed);
            

        }
    }
}
