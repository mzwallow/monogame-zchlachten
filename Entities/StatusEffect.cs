using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public abstract class StatusEffect : IGameEntity
    {
        private readonly World _world;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        private Body _body;

        public StatusEffect(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            _body = _world.CreateBody(position, 0f, BodyType.Static);

            var statusEffectFixture = _body.CreateCircle(_texture.Width, 1f);
            statusEffectFixture.Tag = "statusEffects";
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _body.Position,
                null,
                Color.White,
                _body.Rotation,
                _textureOrigin,
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}