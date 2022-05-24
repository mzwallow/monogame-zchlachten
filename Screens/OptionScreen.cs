using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Zchlachten.Components;

namespace Zchlachten.Screens
{
    public class OptionScreen : Screen
    {
        private MouseState _currentMouse;
        private SpriteBatch _spriteBatch;
        private SpriteFont _buttonFont;
        private Texture2D _buttonTexture, _volumeRect;
        private Texture2D _backgroundTxr;
        private Rectangle _volumeBar;
        private List<Component> _menuComponents;
        private Button _backToMenuButton;
        private Vector2 _musicPosition, _soundPosition;
        public OptionScreen(Zchlachten game) : base(game) { }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);
            _buttonTexture = base.Content.Load<Texture2D>("Controls/Button");
            _buttonFont = base.Content.Load<SpriteFont>("Fonts/Text");
            _backgroundTxr = base.Content.Load<Texture2D>("Environments/BG_Option");


            //Volume option Rectangle
            _volumeRect = new Texture2D(base.GraphicsDevice, 1, 1);
            _volumeRect.SetData(new[] { Color.White });
            _volumeBar = new Rectangle(Globals.SCREEN_WIDTH / 2 - 640 / 2, Globals.SCREEN_HEIGHT / 2 - 20, 640, 10);

            _musicPosition = new Vector2((Globals.MusicVolume * 10 + 5) * 64, (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) - 25);
            _soundPosition = new Vector2((Globals.MusicVolume * 10 + 5) * 64, (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) + 110);

            _backToMenuButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 220),
                Text = "Back To Menu"

            };
            _backToMenuButton.Click += backToMenuButtonClick;

            _menuComponents = new List<Component>(){
                _backToMenuButton
            };



            base.Initialize();
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _currentMouse = Mouse.GetState();
            //draw Button
            foreach (var component in _menuComponents)
                component.Update(gameTime);

            //Adjust Volume
            if (_currentMouse.LeftButton == ButtonState.Pressed)
            {
                if (_currentMouse.X > (Globals.SCREEN_WIDTH / 2 - _volumeBar.Width / 2) - 5 && _currentMouse.X < (Globals.SCREEN_WIDTH / 2 + _volumeBar.Width / 2)
                && _currentMouse.Y > (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) - 45 && _currentMouse.Y < (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) + 15)
                {
                    //Adjust Music Volume
                    Globals.MusicVolume = (_currentMouse.X / 64f - 5) * 0.1f;
                    _musicPosition.X = (Globals.MusicVolume * 10 + 5) * 64;
                    MediaPlayer.Volume = Globals.MusicVolume;
                    

                }
                else if (_currentMouse.X > Globals.SCREEN_WIDTH / 2 - ((_volumeBar.Width / 2) + 5) && _currentMouse.X < Globals.SCREEN_WIDTH / 2 + _volumeBar.Width / 2
                && _currentMouse.Y > (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) + 90 && _currentMouse.Y < (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) + 150)
                {
                    //Adjust SoundFX Volume
                    Globals.SoundVolume = (_currentMouse.X / 64f - 5) * 0.1f;
                    _soundPosition.X = (Globals.SoundVolume * 10 + 5) * 64;
                    SoundEffect.MasterVolume = Globals.SoundVolume;
                    Globals.soundFX.Play();
                   
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundTxr,new Vector2(0,0),Color.White);
            //draw button
            foreach (var component in _menuComponents)
                component.Draw(_spriteBatch);

            //Music Option
            _spriteBatch.DrawString(_buttonFont, "MUSIC", new Vector2((Globals.SCREEN_WIDTH / 2) - _buttonFont.MeasureString("MUSIC").X / 2, 240), Color.DarkGray);
            _spriteBatch.Draw(_volumeRect, new Rectangle(Globals.SCREEN_WIDTH / 2 - _volumeBar.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) - 15, _volumeBar.Width, _volumeBar.Height), Color.White);
            _spriteBatch.Draw(_volumeRect, new Rectangle((int)_musicPosition.X, (int)_musicPosition.Y, 20, 30), Color.Silver);
            //SoundRffect Option
            _spriteBatch.DrawString(_buttonFont, "SOUNDTRACK", new Vector2((Globals.SCREEN_WIDTH / 2) - _buttonFont.MeasureString("SOUNDTRACK").X / 2, Globals.SCREEN_HEIGHT / 2 + 30), Color.DarkGray);
            _spriteBatch.Draw(_volumeRect, new Rectangle(Globals.SCREEN_WIDTH / 2 - _volumeBar.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _volumeBar.Height / 2) + 120, _volumeBar.Width, _volumeBar.Height), Color.White);
            _spriteBatch.Draw(_volumeRect, new Rectangle((int)_soundPosition.X, (int)_soundPosition.Y, 20, 30), Color.Silver);

            _spriteBatch.End();
        }

        private void backToMenuButtonClick(object sender, EventArgs e)
        {

            Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
        }
    }
}