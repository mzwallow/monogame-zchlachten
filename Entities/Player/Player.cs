using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Graphics;

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
        public List<StatusEffect> StatusEffectBag = new List<StatusEffect>();
        public List<StatusEffect> HoldStatusEffectBag = new List<StatusEffect>();
        public Items[] ItemsBag = new Items[3];

        private Sprite _sprite;

        protected Player(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Size = new Vector2(0.9609375f, 2.0625f);
            _scale = Size / new Vector2(_texture.Width, _texture.Height);

            Body = _world.CreateBody(position);

            _playerFixture = Body.CreateRectangle(Size.X, Size.Y, 1f, Vector2.Zero);
            _playerFixture.Tag = new Tag(TagType.PLAYER);

            WeaponsBag = new List<Weapon>(2);

            _sprite = new Sprite(texture);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, Body.Position);
        }

        public void HitBy(Weapon weapon)
        {
            HP -= weapon.Damage;

            if (weapon.Type == WeaponType.CHARM)
            {
                StatusEffectBag.Add(new DebuffDrunken(_world, _texture));

                Debug.WriteLine("Player '" + PlayerSide + "' has seduced by '" + weapon.Type + "'.");
            }
        }
    }
}