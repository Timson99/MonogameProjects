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
    /*
     * GameServices.AddService<GraphicsDevice>(device); 
       myDevice = GameServices.GetService<GraphicsDevice>(); // GameServices.graphics...
       GameServices.RemoveService<GraphicsDevice>()
    */
    public static class GameServices
    {
        private static GameServiceContainer container;
        public static GameServiceContainer Instance
        {
            get
            {
                if (container == null)
                {
                    container = new GameServiceContainer();
                }
                return container;
            }
        }

        public static SpriteBatch spriteBatch {get{return GetService<SpriteBatch>();}}
        public static ContentManager Content { get { return GetService<ContentManager>(); } }
        public static GraphicsDeviceManager graphics { get { return GetService<GraphicsDeviceManager>(); } }
        public static GameTime gameTime { get { return GetService<GameTime>(); } }
        public static double dt { get { return GetService<GameTime>().ElapsedGameTime.TotalSeconds; } }
        public static GraphicsDevice graphicsDevice { get { return GetService<GraphicsDevice>(); } }

        public static T GetService<T>()
        {
            return (T)Instance.GetService(typeof(T));
        }

        public static void AddService<T>(T service)
        {
            Instance.AddService(typeof(T), service);
        }

        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof(T));
        }
    }
}
