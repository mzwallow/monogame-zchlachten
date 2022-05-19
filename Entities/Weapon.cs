using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace Zchlachten.Entities
{
    public abstract class Weapon : IGameEntity
    {
        private readonly World _world;
        private readonly Player _player;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        public Body Body;
        private Fixture _fixture;

        public WeaponType WeaponType;
        public int Damage;

        public bool HasCollided = false;

        protected Weapon(World world, Player player, Texture2D texture, Vector2 position)
        {
            _world = world;
            _player = player;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Body = _world.CreateBody(position, 0f, BodyType.Dynamic);

            _fixture = Body.CreateCircle(_texture.Width / 2f, 1f);
            _fixture.Restitution = 0.3f;
            _fixture.Friction = 0.5f;
            _fixture.Tag = "weapons";

            _fixture.OnCollision = OnCollisionEventHandler;
        }

        public virtual void Update(GameTime gameTime) 
        { 
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture, 
                Body.Position, 
                null, 
                Color.White, 
                Body.Rotation, 
                _textureOrigin, 
                1f, 
                SpriteEffects.None, 0f
            );
        }

        private bool OnCollisionEventHandler(Fixture sender, Fixture other, Contact contact)
        {
            HasCollided = true;
            
            if ((string)other.Tag == "players")
                _player.Hit(Damage);

            return true;
        }
    }
}