using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using tainicom.Aether.Physics2D.Dynamics;

using Zchlachten.Graphics;

namespace Zchlachten.Entities
{
    public abstract class Items : IGameEntity
    {
        private readonly World _world;

        public Texture2D Texture => _sprite.Texture;
        public float Width => _sprite.Width;
        public float Height => _sprite.Height;

        private Sprite _sprite;

        protected Items(World world, Texture2D texture)
        {
            _world = world;

            _sprite = new Sprite(texture);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
    }
}