using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Graphics
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 TextureOrigin { get; set; }

        public float Width => Texture.Width * Globals.Camera.Scale * _defaultScale.X;
        public float Height => Texture.Height * Globals.Camera.Scale * _defaultScale.Y;

        private Vector2 _defaultScale = Vector2.One;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public Sprite(Texture2D texture, Vector2 scale)
        {
            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            _defaultScale = scale;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(
                Texture,
                position,
                null,
                Color.White,
                0f,
                TextureOrigin,
                Globals.Camera.Scale * _defaultScale,
                SpriteEffects.FlipVertically,
                0f
            );
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 scale)
        {
            spriteBatch.Draw(
                Texture,
                position,
                null,
                Color.White,
                0f,
                TextureOrigin,
                Globals.Camera.Scale * _defaultScale * scale,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}