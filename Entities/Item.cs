using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class Item : IGameEntity
    {
        private Texture2D _texture;
        private Vector2 _position;

        public Item(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}