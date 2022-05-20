using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class Items : IGameEntity
    {
        private readonly World _world;
        private Texture2D _texture;
        private Vector2 _textureOrigin;
        private Fixture _itemsFixture;
        public Body Body;
        private Vector2 _size;
        private Vector2 _scale;

        public Items(World world,Texture2D texture, Vector2 position)
        {
            _world = world;
            _texture = texture;
            
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            _size = new Vector2(_texture.Width*0.0234375f, _texture.Height*0.0234375f)*1.25f;
            _scale = _size / new Vector2(_texture.Width, _texture.Height);
            
            Body = _world.CreateBody(position);

            _itemsFixture = Body.CreateCircle(_size.X / 2, 1f);
            _itemsFixture.Tag = "items";
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}