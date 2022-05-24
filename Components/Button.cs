using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Zchlachten.Components
{
    public class Button : Component
    {
        private MouseState _currentMouse, _previousMouse;

        private SpriteFont _font;
        private Vector2 _scale;
        private bool _isHovering;
        private Texture2D _texture;
        public event EventHandler Click;

        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 scale)
        {
            var colour = Color.White;
            _scale = scale;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), null, colour, 0f, new Vector2(_texture.Width / 2, _texture.Height / 2), _scale, SpriteEffects.FlipVertically, 0f);

            if (!string.IsNullOrEmpty(Text))
            {
                // var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                // var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                var x = Position.X;
                var y = Position.Y;

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour, 0f, new Vector2(_font.MeasureString(Text).X / 2, _font.MeasureString(Text).Y / 2), _scale, SpriteEffects.FlipVertically, 0f);
            }

        }



        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }



        }
        public void UpdateIngame(GameTime gameTime)
        {
            Vector2 relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);
            // _previousMouse = _currentMouse;
            // _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle((int)relativeMousePosition.X, (int)relativeMousePosition.Y, (int)(1*_scale.X), (int)(1*_scale.Y));

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (Globals.CurrentMouseState.LeftButton == ButtonState.Released && Globals.PreviousMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
         
    }
    
}