using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zchlachten.Screens;

namespace Zchlachten
{
    public class Zchlachten : Game
    {
        public GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;

        public Zchlachten()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Globals.ScreenManager = new ScreenManager();
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = Globals.SCREEN_WIDTH;
            Graphics.PreferredBackBufferHeight = Globals.SCREEN_HEIGHT;
            Graphics.ApplyChanges();

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

            // * Update logic
            Globals.PreviousMouseState = Globals.CurrentMouseState;
            Globals.CurrentMouseState = Mouse.GetState();
            Globals.PreviousKeyboardState = Globals.CurrentKeyboardState;
            Globals.CurrentKeyboardState = Keyboard.GetState();

            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.O)) // Press 'O' to go to play screen
                LoadPlayScreen();
            else if (Keyboard.GetState().IsKeyDown(Keys.P)) // Press 'P' to go to menu screen
                LoadMenuScreen();
            // * End update logic

            Globals.ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            Globals.ScreenManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void LoadMenuScreen()
        {

            Globals.ScreenManager.LoadScreen(new MenuScreen(this));
        }

        private void LoadPlayScreen()
        {
            Globals.ScreenManager.LoadScreen(new PlayScreen(this));
        }
    }
}
