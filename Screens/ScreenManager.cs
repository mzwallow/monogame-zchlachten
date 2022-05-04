using Microsoft.Xna.Framework;

namespace Zchlachten.Screens
{
    public class ScreenManager : IGameComponent
    {
        private Screen _activeScreen;

        public ScreenManager() { }

        public void LoadScreen(Screen screen)
        {
            _activeScreen?.UnloadContent();
            _activeScreen?.Dispose();

            screen.ScreenManager = this;

            screen.Initialize();

            screen.LoadContent();

            _activeScreen = screen;
        }

        public void Initialize()
        {
            _activeScreen?.Initialize();
        }

        protected void LoadContent()
        {
            _activeScreen?.LoadContent();
        }

        protected void UnloadContent()
        {
            _activeScreen?.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            _activeScreen?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _activeScreen?.Draw(gameTime);
        }
    }
}