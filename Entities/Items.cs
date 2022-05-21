using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public abstract class Items : IGameEntity
    {
        private readonly World _world;
        private Texture2D _texture;
        private Vector2 _textureOrigin;
        public Body Body;
        // private Fixture _itemsFixture;
        private Vector2 _size;
        private Vector2 _scale;

        protected Items(World world, Texture2D texture, Vector2 position)
        {
            _world = world;
            _texture = texture;

            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            _size = new Vector2(_texture.Width  * 0.0234375f, _texture.Height  * 0.0234375f);
            _scale = _size / new Vector2(_texture.Width, _texture.Height);


            //_itemsFixture.Tag = "items";
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
            _texture,
            Body.Position,
            null,
            Color.White,
            Body.Rotation,
            _textureOrigin,
            _scale,
            SpriteEffects.FlipVertically,
            0f
        );
        }

        

    }
}