using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zchlachten.Screens;

namespace Zchlachten
{
    public class Zchlachten : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;

        public Zchlachten()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = new ScreenManager();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Globals.SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = Globals.SCREEN_HEIGHT;
            _graphics.ApplyChanges();

            base.Initialize();

            LoadMenuScreen();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.O)) // Press 'O' to go to play screen
                LoadPlayScreen();
            else if (Keyboard.GetState().IsKeyDown(Keys.P)) // Press 'P' to go to menu screen
                LoadMenuScreen();

            _screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _screenManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void LoadMenuScreen()
        {
            _screenManager.LoadScreen(new MenuScreen(this));
        }

        private void LoadPlayScreen()
        {
            _screenManager.LoadScreen(new PlayScreen(this));
        }
    }
}
