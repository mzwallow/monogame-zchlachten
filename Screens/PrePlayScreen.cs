using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using Zchlachten.Components;

namespace Zchlachten.Screens
{
    public class PrePlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _buttonFont, _storyFont;
        private Song song;
        private Texture2D _buttonTexture;
        private List<Component> _menuComponents;
        private Button _playButton, _backToMenuButton, _guideButton;
        public PrePlayScreen(Zchlachten game) : base(game) { }

        private String _story = "   ในโลกแฟนตาซีแห่งหนึ่งมีความขัดแย้งของสองเผ่าพันธุ์มาอย่างยาวนานโดยทั้ง 2 เผ่า\nก็คือเผ่ามนุษย์และเผ่ามารความขัดแย้งนี้ทั้งยืดเยื้อและสร้างความเสียหายให้ทั้งสองเผ่า\nอย่างใหญ่หลวงทั้ง 2 เผ่าจึงใช้วิธีการอัญเชิญสิ่งมีชีวิตมาจากต่างมิติ โดยเผ่ามารได้อัญเชิญ\nสิ่งมีชีวิตที่เรียกตนว่าจอมมารส่วนเผ่ามนุษย์ได้อัญเชิญมนุษย์มา โดยต่อมาก็ถูกขนานนามว่า\nผู้กล้า โดยจะเป็นศึกครั้งนี้จะเป็นตัวตัดสินแพ้ชนะของทั้งสองเผ่าพันธุ์";

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);
            _buttonTexture = base.Content.Load<Texture2D>("Controls/Button");
            _buttonFont = base.Content.Load<SpriteFont>("Fonts/Text");

            //play button
            _playButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2) + 270 + 128, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 260),
                Text = "Let's Play"

            };
            _playButton.Click += playButtonClick;

            //guide button
            _guideButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2), (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 260),
                Text = "Guide"

            };
            _guideButton.Click += guideButtonClick;


            // back button
            _backToMenuButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2) - 270 - 128, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 260),
                Text = "Back To Menu"

            };
            _backToMenuButton.Click += backToMenuButtonClick;



            _menuComponents = new List<Component>(){
                _playButton,
                _guideButton,
                _backToMenuButton,
            };


            base.Initialize();
        }
        public override void LoadContent()
        {
            _storyFont = base.Content.Load<SpriteFont>("Fonts/StoryText");
            this.song = Content.Load<Song>("Sound/Win");
            MediaPlayer.Play(song);
            //  MediaPlayer.ActiveSongChanged += MediaPlayer_ActiveSongChanged;
            // //MediaPlayer.Play(song);


            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _menuComponents)
                component.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.DarkViolet);
            _spriteBatch.Begin();

            _spriteBatch.DrawString(_storyFont, _story, new Vector2((Globals.SCREEN_WIDTH / 2) - _storyFont.MeasureString("   ในโลกแฟนตาซีแห่งหนึ่งมีความขัดแย้งของสองเผ่าพันธุ์มาอย่างยาวนานโดยทั้ง 2 เผ่า").X / 2, 200), Color.DarkGray);

            foreach (var component in _menuComponents)
                component.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        void MediaPlayer_ActiveSongChanged(object sender, System.
                                           EventArgs e)
        {
            this.song = Content.Load<Song>("Sound/Win");
            MediaPlayer.Play(song);
        }

        private void backToMenuButtonClick(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
        }
        private void playButtonClick(object sender, EventArgs e)
        {
            Globals.ScreenManager.LoadScreen(new PlayScreen(base.Game));
        }
        private void guideButtonClick(object sender, EventArgs e)
        {
            Globals.ScreenManager.LoadScreen(new TutorialScreen(base.Game));
        }


    }
}