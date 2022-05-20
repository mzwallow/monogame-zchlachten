using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using tainicom.Aether.Physics2D.Common;

namespace Zchlachten.Entities
{
    public abstract class Weapon : IGameEntity
    {
        private readonly World _world;
        private readonly Player _player, _enemy;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        public Body Body;
        public Fixture _weaponFixture;
        private Vector2 _size;
        private Vector2 _scale;
        public Vector2 Position;

        public WeaponType Type;
        public int Damage;

        public bool HasCollided = false;

        protected Weapon(World world, Player player, Player enemy, Texture2D texture)
        {
            _world = world;
            _player = player;
            _enemy = enemy;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        protected Weapon(World world, Player player, Player enemy, Texture2D texture, Vector2 position)
        {
            _world = world;
            _player = player;
            _enemy = enemy;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            _size = new Vector2(_texture.Width * 0.0234375f, _texture.Height * 0.0234375f);
            _scale = _size / new Vector2(_texture.Width, _texture.Height);

            Position = position;

            Body = _world.CreateBody(position, 0f, BodyType.Dynamic);
            _weaponFixture = Body.CreateCircle(_size.X / 2, 1f);
            _weaponFixture.Restitution = 0.5f;
            _weaponFixture.Friction = 0.3f;
            _weaponFixture.Tag = "weapons";

            _weaponFixture.OnCollision = OnCollisionEventHandler;
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
                SpriteEffects.FlipHorizontally,
                0f
            );

            Globals.DebugView.DrawShape(_weaponFixture, new Transform(Body.Position, Body.Rotation), Color.Aqua);
        }

        private bool OnCollisionEventHandler(Fixture sender, Fixture other, Contact contact)
        {
            if ((string)other.Tag == "statusEffects")
            {
                Debug.WriteLine("Hit status");
                return false;
            }

            if ((string)other.Tag == "players")
            {
                HasCollided = true;
                _enemy.Hit(Damage);
                _player.BloodThirstGauge++;
            }

            if ((string)other.Tag == "ground")
                HasCollided = true;
            
            return true;
        }

        public void CreateBody(Vector2 position)
        {
            Position = position;

            _size = new Vector2(_texture.Width * 0.0234375f, _texture.Height * 0.0234375f);
            _scale = _size / new Vector2(_texture.Width, _texture.Height);

            Body = _world.CreateBody(Position, 0f, BodyType.Dynamic);
            Body.AngularVelocity = 10f;
            _weaponFixture = Body.CreateCircle(_size.X / 2, 1f);
            _weaponFixture.Tag = "weapons";

            _weaponFixture.OnCollision = OnCollisionEventHandler;
        }
    }
}