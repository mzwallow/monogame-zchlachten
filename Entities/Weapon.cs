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
        private readonly Player _enemy;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        public Body Body;
        public Fixture _weaponFixture;
        private Vector2 _size;
        private Vector2 _scale;
        public Vector2 Position;

        public WeaponType WeaponType;
        public int Damage;

        public bool HasCollided = false;

        protected Weapon(World world, Player enemy, Texture2D texture)
        {
            _world = world;
            _enemy = enemy;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        protected Weapon(World world, Player enemy, Texture2D texture, Vector2 position)
        {
            _world = world;
            _enemy = enemy;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            // _size = new Vector2(_texture.Width*0.0234375f, _texture.Height*0.0234375f);
            // _scale = _size / new Vector2(_texture.Width, _texture.Height);
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
                _scale,
                SpriteEffects.FlipHorizontally,
                0f
            );

            Globals.DebugView.DrawShape(_weaponFixture, new Transform(Body.Position, Body.Rotation), Color.Aqua);
        }

        private bool OnCollisionEventHandler(Fixture sender, Fixture other, Contact contact)
        {
            HasCollided = true;

            Debug.WriteLine((string)sender.Tag + " hit " + (string)other.Tag);

            if ((string)other.Tag == "players")
                _enemy.Hit(Damage);

            return true;
        }

        public void CreateBody(Vector2 position)
        {
            Position = position;

            _size = new Vector2(_texture.Width * 0.0234375f, _texture.Height * 0.0234375f);
            _scale = _size / new Vector2(_texture.Width, _texture.Height);

            Body = _world.CreateBody(Position, 0f, BodyType.Dynamic);
            _weaponFixture = Body.CreateCircle(_size.X / 2, 1f);
            _weaponFixture.Restitution = 0.5f;
            _weaponFixture.Friction = 0.3f;
            _weaponFixture.Tag = "weapons";

            _weaponFixture.OnCollision = OnCollisionEventHandler;
        }
    }
}