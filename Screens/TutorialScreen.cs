using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Zchlachten.Components;

namespace Zchlachten.Screens
{
    public class TutorialScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _buttonFont, _storyFont;

        private Texture2D _buttonTexture, _blessingOfDevil, _guide, _blessingOfGod,_blessingOfWorld,_eyeBall;
        private List<Component> _menuComponents;
        private List<Component> _menuComponents2;

        private Button _backToMenuButton;
        private Button _nextToPageButton;

        private Button _backToPageButton;
        private bool _isHovering;

        private int _pages;

        private MouseState _currentMouse, _previousMouse;
        private Rectangle _reactBlessDevil,Rectangle,_reactBlessGod,_reactBlessWorld,_reactEyeBall;


        private String _infoGuide = "คลิ้กและกดค้างเอาไว้ที่ตรงตัวละครของตัวละครที่เลือกในตาของตัวเอง\n    - หลอดพลังจะเพิ่มขึ้นจนกระทั้งปล่อย\n    - ดูทิศทางของเริ่มเพื่อนำมาใช้การประเมิณในการโจมตี\n    - เมื่อปล่อยเมาท์จะโจมตีหรือใช้ไอเทมที่เลือก\nคลิ้กสัญลักษณ์ข้างล่างเพื่อดูว่าไอเทมแต่ละชิ้นให้ความสามารถอะไร";

        public TutorialScreen(Zchlachten game) : base(game) { }

        public override void Initialize()
        {
            _pages = 0;

            _buttonTexture = base.Content.Load<Texture2D>("Controls/Button");
            _buttonFont = base.Content.Load<SpriteFont>("Fonts/Text");
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);


            _backToMenuButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2 - 500, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 270),
                Text = "Back To Menu"

            };
            _backToMenuButton.Click += backToMenuButtonClick;

           
            
                _nextToPageButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 + _buttonTexture.Width / 2 + 100, (Globals.SCREEN_HEIGHT / 2 + _buttonTexture.Height / 2) + 160),
                Text = "Next Page"
            };
                _backToPageButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2 - 500, (Globals.SCREEN_HEIGHT / 2 + _buttonTexture.Height / 2) + 160),
                Text = "Back Page"
            };
              
            _nextToPageButton.Click += nextPageButtonClick;
            _backToPageButton.Click += nextPageButtonClick;

            _guide = new Texture2D(base.GraphicsDevice, 1, 1);
            _guide.SetData(new[] { Color.White });



            _menuComponents = new List<Component>(){
                _backToMenuButton,
                _nextToPageButton
            };

            _menuComponents2 = new List<Component>(){
                _backToMenuButton,
                _backToPageButton
            };

            base.Initialize();
        }
        public override void LoadContent()
        {

            _storyFont = base.Content.Load<SpriteFont>("Fonts/StoryText2");
            _blessingOfDevil = base.Content.Load<Texture2D>("Controls/blessing_of_devil");
            _blessingOfGod = base.Content.Load<Texture2D>("Controls/blessing_of_god");
            _blessingOfWorld = base.Content.Load<Texture2D>("Controls/blessing_of_world_tree");
            _eyeBall = base.Content.Load<Texture2D>("Controls/eyeBall");
            _reactBlessDevil = new Rectangle((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 450, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 150,_blessingOfGod.Width * 2,  _blessingOfGod.Height * 2);
            _reactBlessGod = new Rectangle((Globals.SCREEN_WIDTH / 2) -  _blessingOfGod.Width / 2 - 400, (Globals.SCREEN_HEIGHT / 2) -  _blessingOfGod.Height / 2 + 200,  _blessingOfGod.Width * 2,  _blessingOfGod.Height * 2);
            _reactBlessWorld = new Rectangle((Globals.SCREEN_WIDTH / 2) -  _blessingOfWorld.Width / 2 - 300, (Globals.SCREEN_HEIGHT / 2) -  _blessingOfWorld.Height / 2 + 200,  _blessingOfWorld.Width * 2,  _blessingOfWorld.Height * 2);
            _reactEyeBall = new Rectangle((Globals.SCREEN_WIDTH / 2) -  _eyeBall.Width / 2 - 200, (Globals.SCREEN_HEIGHT / 2) -  _eyeBall.Height / 2 + 200,  _eyeBall.Width * 2,  _eyeBall.Height * 2);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _menuComponents)
                component.Update(gameTime);

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                // if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                // {
                //     Click?.Invoke(this, new EventArgs());
                // }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.DarkOrange);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_guide, new Rectangle((Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2) - 200, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 270, 950, _buttonTexture.Height), Color.White);
            _spriteBatch.DrawString(_buttonFont, "Guide", new Vector2(((Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2) + 275) - _buttonFont.MeasureString("Guide").X / 2, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 240 - _buttonFont.MeasureString("Guide").Y / 2), Color.Black);

            if (_pages == 0)
            {
            _spriteBatch.DrawString(_storyFont, _infoGuide, new Vector2((Globals.SCREEN_WIDTH / 2) - _storyFont.MeasureString("คลิ้กและกดค้างเอาไว้ที่ตรงตัวละครของตัวละครที่เลือกในตาของตัวเอง").X / 2, 200), Color.Black);
            
            foreach (var component in _menuComponents)
                component.Draw(_spriteBatch);
            }
            else if (_pages == 1)
            {
            _spriteBatch.Draw( _blessingOfDevil, _reactBlessDevil, Color.White);
            // _spriteBatch.Draw(_blessingOfGod,_reactBlessGod,Color.White);
            // _spriteBatch.Draw(_blessingOfWorld,_reactBlessWorld,Color.White);
            // _spriteBatch.Draw(_eyeBall,_reactEyeBall,Color.White);
                foreach (var component in _menuComponents2)
                component.Draw(_spriteBatch);
            }

            

            _spriteBatch.End();
        }

        private void backToMenuButtonClick(object sender, EventArgs e)
        {
            Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
        }

        private void nextPageButtonClick(object sender, EventArgs e)
        {
            if (_pages == 0)
            {
                _pages = 1;
            }
            else if (_pages == 1)
            {
                _pages = 0;
            }
        }


    }
}