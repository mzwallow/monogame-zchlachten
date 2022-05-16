using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public abstract class Weapon : IGameEntity
    {
        private readonly World _world;

        public WeaponType WeaponType { get; protected set; }
        public int Damage { get; protected set; }
        public Body Body { get; set; }

        private Texture2D _texture;
        private Vector2 _textureOrigin;

        protected Weapon(
            World world,
            Texture2D texture,
            Vector2 position,
            WeaponType weaponType,
            int damage
        )
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Body = _world.CreateBody(position, 0f, BodyType.Dynamic);
            var weaponFixture = Body.CreateCircle(_texture.Width / 2f, 1f);
            weaponFixture.Restitution = 0.3f;
            weaponFixture.Friction = 0.5f;

            WeaponType = weaponType;
            Damage = damage;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Body.Position, null, Color.White, Body.Rotation, _textureOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}