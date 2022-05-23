using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Graphics
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 TextureOrigin { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width/2, Texture.Height/2);
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
                Globals.Camera.Scale,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}