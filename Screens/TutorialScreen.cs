using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Zchlachten.Components;

namespace Zchlachten.Screens
{
    public class TutorialScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _buttonFont;

        private Texture2D _buttonTexture;
        private List<Component> _menuComponents;
        private Button _backToMenuButton;
        public TutorialScreen(Zchlachten game) : base(game) { }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);
             _buttonTexture = base.Content.Load<Texture2D>("Controls/Button");
            _buttonFont = base.Content.Load<SpriteFont>("Fonts/Text");

            _backToMenuButton = new Button(_buttonTexture,_buttonFont){
               Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 320),
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
             foreach (var component in _menuComponents)
                component.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.DarkOrange);

            _spriteBatch.Begin();

            foreach (var component in _menuComponents)
                component.Draw(_spriteBatch);

            
            _spriteBatch.End();
        }

        private void backToMenuButtonClick(object sender, EventArgs e){
                Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
        }
    }
}