using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Zchlachten.Components;

namespace Zchlachten.Screens
{
    public class MenuScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _buttonFont;
        private Song song;
        private Texture2D _buttonTexture;
        private Texture2D _backgroundTxr;
        private List<Component> _menuComponents;
        private Button _newGameButton, _tutorialButton, _optionButton, _quitButton;

        public MenuScreen(Zchlachten game) : base(game) { }
        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // rectangle button
            _buttonTexture = base.Content.Load<Texture2D>("Controls/Button");
            _buttonFont = base.Content.Load<SpriteFont>("Fonts/Text");
            
            _backgroundTxr = base.Content.Load<Texture2D>("Environments/BG_Menu");

            // Create button each button is 20 pixel aprt
            _newGameButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2)),
                Text = "Start",
            };
            _newGameButton.Click += newGameButtonClick;

            _tutorialButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 80),
                Text = "Tutorial",
            };
            _tutorialButton.Click += tutorialButtonClick;

            _optionButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 160),
                Text = "Option",
            };
            _optionButton.Click += optiopnButtonClick;

            _quitButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 240),
                Text = "Quit",
            };
            _quitButton.Click += quitButtonClick;

            // add menu component into list
            _menuComponents = new List<Component>()
            {
              _newGameButton,
              _tutorialButton,
              _optionButton,
              _quitButton

            };

            

        }

        public override void LoadContent()
        {
            this.song = Content.Load<Song>("Sound/BGM");
            if (MediaPlayer.State != MediaState.Playing)
                MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Globals.MusicVolume;

            base.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            foreach (var component in _menuComponents)
                component.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            //base.GraphicsDevice.Clear(Color.DarkSlateBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTxr,new Vector2(0,0),Color.White);

            //if _showOption is false show object in _menuComponents

            //spriteBatch.Draw(_menu_bg, Vector2.Zero, Color.White);

            foreach (var component in _menuComponents)
                component.Draw(_spriteBatch);


            _spriteBatch.End();


        }

        private void newGameButtonClick(object sender, EventArgs e)
        {
            Globals.ScreenManager.LoadScreen(new PrePlayScreen(base.Game));

        }
        private void tutorialButtonClick(object sender, EventArgs e)
        {
            Globals.ScreenManager.LoadScreen(new TutorialScreen(base.Game));

        }
        private void optiopnButtonClick(object sender, EventArgs e)
        {
            //Globals._bgmInstance.Pause();
            //MediaPlayer.Stop();
            Globals.ScreenManager.LoadScreen(new OptionScreen(base.Game));

        }
        private void quitButtonClick(object sender, EventArgs e)
        {
            Game.Exit();

        }
    }
}