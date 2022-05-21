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

        public Texture2D Texture;
        public Vector2 TextureOrigin;
        public Body Body;
        public Fixture _weaponFixture;
        public Vector2 Size;
        public Vector2 Scale;
        public Vector2 Position;

        public WeaponType Type;
        public int Damage;

        public bool HasCollided = false;

        protected Weapon(World world, Player player, Player enemy, Texture2D texture)
        {
            _world = world;
            _player = player;
            _enemy = enemy;

            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Size = new Vector2(Texture.Width * 0.0234375f, Texture.Height * 0.0234375f);
            Scale = Size / new Vector2(Texture.Width, Texture.Height);
        }

        protected Weapon(World world, Player player, Player enemy, Texture2D texture, Vector2 position)
        {
            _world = world;
            _player = player;
            _enemy = enemy;

            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Size = new Vector2(Texture.Width * 0.0234375f, Texture.Height * 0.0234375f);
            Scale = Size / new Vector2(Texture.Width, Texture.Height);

            Position = position;

            Body = _world.CreateBody(position, 0f, BodyType.Dynamic);
            _weaponFixture = Body.CreateCircle(Size.X / 2, 1f);
            _weaponFixture.Restitution = 0.5f;
            _weaponFixture.Friction = 0.3f;
            _weaponFixture.Tag = "weapons";

            _weaponFixture.OnCollision = OnCollisionEventHandler;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Body.Position,
                null,
                Color.White,
                Body.Rotation,
                TextureOrigin,
                Scale,
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
                _enemy.HitBy(this);
                _player.BloodThirstGauge++;
            }

            if ((string)other.Tag == "ground")
                HasCollided = true;
            
            return true;
        }

        public void CreateBody(Vector2 position)
        {
            Position = position;

            Size = new Vector2(Texture.Width * 0.0234375f, Texture.Height * 0.0234375f);
            Scale = Size / new Vector2(Texture.Width, Texture.Height);

            Body = _world.CreateBody(Position, 0f, BodyType.Dynamic);
            Body.AngularVelocity = 10f;
            _weaponFixture = Body.CreateCircle(Size.X / 2, 1f);
            _weaponFixture.Tag = "weapons";

            _weaponFixture.OnCollision = OnCollisionEventHandler;
        }
    }
}