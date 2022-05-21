using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Common;

namespace Zchlachten.Entities
{
    public abstract class Player : IGameEntity
    {
        private readonly World _world;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        public Vector2 Size;
        private Vector2 _scale;
        public Body Body;
        private Fixture _playerFixture;

        public PlayerSide PlayerSide;
        public int HP = 150;
        public int BloodThirstGauge = 0;

        public Weapon InHandWeapon { get; set; }
        public List<Weapon> WeaponsBag { get; set; }

        public List<StatusEffect> StatusEffectBag { get; set; } = new List<StatusEffect>();

        protected Player(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Size = new Vector2(0.9609375f, 2.0625f);
            _scale = Size / new Vector2(_texture.Width, _texture.Height);

            Body = _world.CreateBody(position);
            _playerFixture = Body.CreateRectangle(Size.X, Size.Y, 1f, Vector2.Zero);
            _playerFixture.Tag = "players";

            WeaponsBag = new List<Weapon>(2);
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
                SpriteEffects.None,
                0f
            );

            Globals.DebugView.DrawShape(_playerFixture, new Transform(Body.Position, Body.Rotation), Color.Crimson);
        }

        public void HitBy(Weapon weapon)
        {
            HP -= weapon.Damage;

            if (weapon.Type == WeaponType.CHARM)
            {
                Debug.WriteLine("Player '" + PlayerSide + "' has seduced by '" + weapon.Type + "'.");
            }
        }
    }
}