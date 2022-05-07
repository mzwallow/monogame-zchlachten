using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class Ground : IGameEntity
    {
        public Vector2 Position;

        private Texture2D _texture;

        public Ground(Texture2D texture)
        {
            _texture = texture;
            Position = new Vector2(0, Globals.SCREEN_HEIGHT - texture.Height);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}