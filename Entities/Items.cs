using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class Items : IGameEntity
    {
        private readonly World _world;
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _textureOrigin;
        public Body Body;
        private Vector2 _size;
        private Vector2 _scale;

        public Items(World world,Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;

            _size = new Vector2(_texture.Width*0.0234375f, _texture.Height*0.0234375f)*1.25f;
            _scale = _size / new Vector2(_texture.Width, _texture.Height);
            
            Body = _world.CreateBody(position);

            var statusEffectFixture = Body.CreateCircle(_texture.Width, 1f);
            statusEffectFixture.Tag = "Items";
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}