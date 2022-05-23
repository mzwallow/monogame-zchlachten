using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public abstract class Items : IGameEntity
    {
        private readonly World _world;
        public Texture2D Texture;
        private Vector2 _textureOrigin;
        public Body Body;
        private readonly EntityManager _entityManager;
        // private Fixture _itemsFixture;
        private Vector2 _size;
        private Vector2 _scale;
        
        //private ItemType _itemType;

        protected Items(World world, Texture2D texture, Vector2 position)
        {
            _world = world;
            Texture = texture;

            _textureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            _size = new Vector2(Texture.Width  * 0.0234375f, Texture.Height  * 0.0234375f);
            _scale = _size / new Vector2(Texture.Width, Texture.Height);

            
            //_itemsFixture.Tag = "items";
        }
        protected Items(World world, Texture2D texture)
        {
            _world = world;
            Texture = texture;

            
            //_itemsFixture.Tag = "items";
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(
            // Texture,
            // Body.Position,
            // null,
            // Color.White,
            // Body.Rotation,
            // _textureOrigin,
            // _scale,
            // SpriteEffects.FlipVertically,
            // 0f
        //);
        }

        

    }
}