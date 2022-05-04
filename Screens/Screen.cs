using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Screens
{
    public abstract class Screen
    {
        public ScreenManager ScreenManager { get; internal set; }
        public Zchlachten Game;
        public ContentManager Content => Game.Content;
        public GraphicsDevice GraphicsDevice => Game.GraphicsDevice;

        protected Screen(Zchlachten game)
        {
            Game = game;
        }

        public virtual void Dispose() { }
        public virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}