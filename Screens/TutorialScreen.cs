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

        private Texture2D _buttonTexture, _goldenSerpantBile, _guide, _blessingOfGod, _Slime, _fireDragonBlood, _background, _wolrdTreeBlessing, _shield, _holyWater, _cursedEye, _demonEye, _lightChakra, _lightSword, _mead;
        private List<Component> _menuComponents, _menuComponentsMid, _menuComponentsEnd;

        private Button _backToMenuButton, _nextToPageButton, _backToPageButton;

        private bool _isHovering;

        private int _pages, _maxPages;

        private MouseState _currentMouse, _previousMouse;
        private Rectangle _reactitem1, Rectangle, _reactitem2, _reactitem3, _reactitem4;
        private Vector2 _reactTextitem1, _reactTextitem2, _reactTextitem4, _reactTextitem3;


        private String _infoGuide = "คลิ้กและกดค้างเอาไว้ที่ตรงตัวละครของตัวละครที่เลือกในตาของตัวเอง\n    - หลอดพลังจะเพิ่มขึ้นจนกระทั้งปล่อย\n    - ดูทิศทางของเริ่มเพื่อนำมาใช้การประเมิณในการโจมตี\n    - เมื่อปล่อยเมาท์จะโจมตีหรือใช้ไอเทมที่เลือก\nคลิ้กสัญลักษณ์ข้างล่างเพื่อดูว่าไอเทมแต่ละชิ้นให้ความสามารถอะไร";
        private String _textGdBlessing = "พรของพระเจ้า ซึ่งเป็นของขวัญจากเหล่าเทพที่มอบให้แก่ผู้เล่นเพื่อเพิ่มความแข็งแกร่ง\nทำให้ผู้กล้าโจมตีแรงขึ้น 25% เป็นเวลา 2 เทิร์น ";
        private String _textfireDragonBlood = "เลือดมังกรเพลิง ซึ่งเป็นเลือดของมังกรเพลิงที่ถูกกำจัดแล้วนำมันใส่ขวด\nทำให้ผู้ที่โดนเลือดลดเทิร์นล่ะ10 หน่วย เป็นเวลา 3 เทิร์น ";
        private String _textSlime = "เมือกสไลม์ เป็นเมือกที่เหล่าสไลม์ปล่อยออกมา ซึ่งมีความเหนียวเป็นอย่างมาก\nทำให้ผู้ที่โดนโจมตีเบาลง 20% เป็นเวลา 2 เทิร์น ";
        private String _textGoldenSerpantBile = "น้ำดีอสรพิษทองคำ ซึ่งเป็นพิษที่มีความรุนแรงสูงทำให้พอโดนแล้วอาจตายได้ในไม่กี่นาที\nทำให้ผู้ที่โดนเลือดลด 50% โอกาศติด 20% ";
        private String _textWolrdTreeBlessing = "พรของต้นไม้โลก เป็นพรที่ได้จากต้นไม้ศักดิ์สิทธิ์ใจกลางสงครามที่จะค่อยช่วยเหลือผู้คนที่เข้าใกล้ \nทำให้ผู้ใช้มันเลือดเพิ่มขึ้น 30% ของ เลือดเต็มหลอด ";
        private String _textShield = "โล่ เป็นโล่ที่ถูกตีขึ้นมาจากเผาคนแคระ ซึ่งเป็นเผ่าพันธุ์ที่กลางในสงครามนี้ ทำให้มีใช้ทั้ง2ฝ่าย \nสามารถป้องกันความเสียหายได้ 1 ครั้ง แต่ถ้าไม่โดนโจมตีเลยจะหายไปหลังผ่านไป 3 เทิร์น";
        private String _textHolyWater = "น้ำศักดิ์สิทธิ์ น้ำที่ได้จากทะเลสาบใกล้กับต้นไม้โลก ถ้ามีความสามารถในการลบสถานะผิดปกติทั้งหมด \nสามาถลบสถานะทั้งหมดทั้งที่ดีและไม่ดี";
        private String _textCursedEye = "เนตรต้องสาบ ท่าไม้ตายของจอมมาร เป็นการเรียกลูกตาปีศาลต้องสาบออกมาซึ่งรุนแรงกว่า\nลูกตาธรรมดามากเป็นท่าของจอมมารทำดาเมท 20 หน่วย";
        private String _textDemonEye = "ลูกตาปีศาล พลังพิเศษของจอมมารที่จะเรียกมอนสเตอร์ลูกตกปีศาลออกมาพู่ชนศัตรู \nเป็นการโจมตีปกติของจอมมารโดยจะทำดาเมท 10 หน่วย";
        private String _textLightChakra = "กงจักรแห่งแสง ท่าไม้ตายของผู้กล้าเรียกกงจักรแห่งแสง เป็นการเรียกกงจักรสีทองอร่าม\nปาใส่ศัตรู ซึ่งแรงกว่าดาบแห่งแสงเป็นท่าของผู้กล้าทำดาเมท 20 หน่วย";
        private String _textLightSword = "ดาบแห่งแสง พลังพิเศษของผู้กล้าที่จะเสกดาบแห่งแสงของผู้กล้าเพื่อปาใส่ศัตรู \nเป็นการโจมตีปกติของผู้กล้าโดยจะทำดาเมท 10 หน่วย";
        private String _textMeadDemon = "บัตเตอร์เบียร์ เบียร์ที่เผ่ามารเข้าซื้อมาจากพวกแม่มด ซึ่งเป็นเครื่องดื่มที่นิยมมากใน\nเหล่าพ่อมดแม่มดวัยเยาร์เป็นการมอบเหล้าให้ศัตรูทำให้ความแรงในการยิงลดเหลือครึ่งเดียว";
        private String _textMeadBrave = "พอร์ทเทอร์&สเตาท์ เบียร์ขึ้นชื่อของเผ่ามนุษย์ ที่นำมอลด์มาคั่วจนไหม้แล้วนำมาหมัก \nทำให้ได้เบียร์สีดำรสเข้มข้นแต่นุ่มนวลเป็นการมอบเหล้าให้ศัตรูทำให้ความแรงในการยิงลดเหลือครึ่งเดียว";
        public TutorialScreen(Zchlachten game) : base(game) { }

        public override void Initialize()
        {
            _pages = 1;
            _maxPages = 5;
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
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 + _buttonTexture.Width / 2 + 100, (Globals.SCREEN_HEIGHT / 2 + _buttonTexture.Height / 2) + 230),
                Text = "Next Page"
            };
            _backToPageButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - _buttonTexture.Width / 2 - 500, (Globals.SCREEN_HEIGHT / 2 + _buttonTexture.Height / 2) + 230),
                Text = "Back Page"
            };

            _nextToPageButton.Click += nextPageButtonClick;
            _backToPageButton.Click += backPageButtonClick;

            _guide = new Texture2D(base.GraphicsDevice, 1, 1);
            _guide.SetData(new[] { Color.White });



            _menuComponents = new List<Component>(){
                _backToMenuButton,
                _nextToPageButton
            };

            _menuComponentsMid = new List<Component>(){
                _backToMenuButton,
                _nextToPageButton,
                _backToPageButton
            };

            _menuComponentsEnd = new List<Component>(){
                _backToMenuButton,
                _backToPageButton
            };

            base.Initialize();
        }
        public override void LoadContent()
        {
            _background = base.Content.Load<Texture2D>("Environments/BG_Guide");
            _storyFont = base.Content.Load<SpriteFont>("Fonts/StoryText");
            _fireDragonBlood = base.Content.Load<Texture2D>("Controls/fire_dragon_blood");
            _blessingOfGod = base.Content.Load<Texture2D>("Controls/blessing_of_god");
            _Slime = base.Content.Load<Texture2D>("Controls/Slime");
            _goldenSerpantBile = base.Content.Load<Texture2D>("Controls/GoldenSerpantBile");
            _wolrdTreeBlessing = base.Content.Load<Texture2D>("Controls/blessing_of_world_tree");
            _shield = base.Content.Load<Texture2D>("Controls/shield");
            _holyWater = base.Content.Load<Texture2D>("Controls/mystery_potion");
            _cursedEye = base.Content.Load<Texture2D>("Weapons/CursedEye");
            _demonEye = base.Content.Load<Texture2D>("Weapons/DemonEye");
            _lightChakra = base.Content.Load<Texture2D>("Weapons/LightChakra");
            _lightSword = base.Content.Load<Texture2D>("Weapons/LightSword");
            _mead = base.Content.Load<Texture2D>("Weapons/Mead");
            _reactitem1 = new Rectangle((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 400, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 130, _blessingOfGod.Width * 2, _blessingOfGod.Height * 2);
            _reactitem2 = new Rectangle((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 400, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 20, _blessingOfGod.Width * 2, _blessingOfGod.Height * 2);
            _reactitem3 = new Rectangle((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 400, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 100, _blessingOfGod.Width * 2, _blessingOfGod.Height * 2);
            _reactitem4 = new Rectangle((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 400, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 210, _blessingOfGod.Width * 2, _blessingOfGod.Height * 2);
            _reactTextitem1 = new Vector2((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 300, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 140);
            _reactTextitem2 = new Vector2((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 300, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) - 30);
            _reactTextitem3 = new Vector2((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 300, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 90);
            _reactTextitem4 = new Vector2((Globals.SCREEN_WIDTH / 2) - _buttonTexture.Width / 2 - 300, (Globals.SCREEN_HEIGHT / 2 - _buttonTexture.Height / 2) + 200);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_pages == 1)
            {
                foreach (var component in _menuComponents)
                    component.Update(gameTime);
            }
            else if (_pages < _maxPages)
            {
                foreach (var component in _menuComponentsMid)
                    component.Update(gameTime);
            }
            else if (_pages == _maxPages)
            {
                foreach (var component in _menuComponentsEnd)
                    component.Update(gameTime);
            }
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
            _spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
            if (_pages == 1)
            {
                _spriteBatch.DrawString(_storyFont, _infoGuide, new Vector2((Globals.SCREEN_WIDTH / 2) - _storyFont.MeasureString("คลิ้กและกดค้างเอาไว้ที่ตรงตัวละครของตัวละครที่เลือกในตาของตัวเอง").X / 2, 200), Color.White);

                DrawComponent(_pages, _maxPages);
            }
            else if (_pages == 2)
            {
                blockDrawItem(_demonEye, _reactitem1, _textDemonEye, _reactTextitem1,"ลูกตาปีศาล");
                blockDrawItem(_cursedEye, _reactitem2, _textCursedEye, _reactTextitem2,"เนตรต้องสาบ");
                blockDrawItem(_mead, _reactitem3, _textMeadDemon, _reactTextitem3,"บัตเตอร์เบียร์");
                DrawComponent(_pages, _maxPages);
            }
            else if (_pages == 3)
            {
                blockDrawItem(_lightSword, _reactitem1, _textLightSword, _reactTextitem1,"ดาบแห่งแสง");
                blockDrawItem(_lightChakra, _reactitem2, _textLightChakra, _reactTextitem2,"กงจักรแห่งแสง");
                blockDrawItem(_mead, _reactitem3, _textMeadBrave, _reactTextitem3,"พอร์ทเทอร์&สเตาท์");
                DrawComponent(_pages, _maxPages);
            }
            else if (_pages == 4)
            {
                blockDrawItem(_blessingOfGod, _reactitem1, _textGdBlessing, _reactTextitem1,"พรของพระเจ้า");
                blockDrawItem(_fireDragonBlood, _reactitem2, _textfireDragonBlood, _reactTextitem2,"เลือดมังกรเพลิง");
                blockDrawItem(_Slime, _reactitem3, _textSlime, _reactTextitem3,"เมือกสไลม์");
                blockDrawItem(_goldenSerpantBile, _reactitem4, _textGoldenSerpantBile, _reactTextitem4,"น้ำดีอสรพิษทองคำ");
                DrawComponent(_pages, _maxPages);
            }
            else if (_pages == 5)
            {
                blockDrawItem(_wolrdTreeBlessing, _reactitem1, _textWolrdTreeBlessing, _reactTextitem1,"พรของต้นไม้โลก");
                blockDrawItem(_shield, _reactitem2, _textShield, _reactTextitem2,"โล่");
                blockDrawItem(_holyWater, _reactitem3, _textHolyWater, _reactTextitem3,"น้ำศักดิ์สิทธิ์");
                DrawComponent(_pages, _maxPages);
            }



            _spriteBatch.End();
        }

        private void backToMenuButtonClick(object sender, EventArgs e)
        {
            Globals.ScreenManager.LoadScreen(new MenuScreen(base.Game));
        }

        private void nextPageButtonClick(object sender, EventArgs e)
        {
            _pages += 1;
        }
        private void backPageButtonClick(object sender, EventArgs e)
        {
            _pages -= 1;
        }
        private void blockDrawItem(Texture2D image, Rectangle positionImage, string text, Vector2 positionText,string itemname)
        {
            _spriteBatch.Draw(image, positionImage, Color.White);
            _spriteBatch.DrawString(_storyFont, text, positionText, Color.White);
            _spriteBatch.DrawString(_storyFont, itemname, positionText, Color.Gold);
        }
        private void DrawComponent(int pages, int maxPages)
        {
            if (pages == 1)
            {
                foreach (var component in _menuComponents)
                    component.Draw(_spriteBatch);
            }
            else if (pages < maxPages)
            {
                foreach (var component in _menuComponentsMid)
                    component.Draw(_spriteBatch);
            }
            else if (pages == maxPages)
            {
                foreach (var component in _menuComponentsEnd)
                    component.Draw(_spriteBatch);
            }
        }


    }
}