using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public abstract class Player : IGameEntity
    {
        private readonly World _world;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        private Body _body;

        public PlayerSide PlayerSide;
        public int HP = 150;
        public int BloodThirstGauge = 0;

        public Weapon InHandWeapon { get; set; }
        public List<Weapon> WeaponsBag { get; set; }

        protected Player(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            _body = _world.CreateBody(position, 0f, BodyType.Static);

            var playerFixture = _body.CreateRectangle(_texture.Width, _texture.Height, 1f, Vector2.Zero);
            playerFixture.Restitution = 0.3f;
            playerFixture.Friction = 0.5f;
            playerFixture.Tag = "players";
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _body.Position, null, Color.White, _body.Rotation, _textureOrigin, 1f, SpriteEffects.None, 0f);
        }

        public void Hit(int damage)
        {
            HP -= damage;
        }
    }
}