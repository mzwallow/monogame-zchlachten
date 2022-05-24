using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Graphics;


namespace Zchlachten.Entities
{
    public abstract class Player : IGameEntity
    {
        private readonly World _world;

        public PlayerSide Side { get; protected set; }
        public int HP = 150;
        public int BloodThirstGauge = 0;

        public Vector2 Position => _body.Position;
        public float Width => _sprite.Width;
        public float Height => _sprite.Height;

        private Sprite _sprite { get; set; }
        private Body _body { get; set; }

        public Weapon InHandWeapon { get; set; }
        public List<Weapon> WeaponsBag { get; private set; }
        public List<StatusEffect> StatusEffectBag { get; private set; }
        public List<StatusEffect> HoldStatusEffectBag { get; private set; }
        public Items[] ItemsBag { get; private set; }

        public SoundEffectInstance AttackSFXInstance { get; set; }

        protected Player(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _sprite = new Sprite(texture);

            _body = _world.CreateBody(position);

            var playerFixture = _body.CreateRectangle(_sprite.Width, _sprite.Height, 1f, Vector2.Zero);
            playerFixture.Tag = new Tag(TagType.PLAYER);

            WeaponsBag = new List<Weapon>(2);
            StatusEffectBag = new List<StatusEffect>();
            HoldStatusEffectBag = new List<StatusEffect>();
            ItemsBag = new Items[3];
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _body.Position);
        }

        public void HitBy(Weapon weapon)
        {
            AttackSFXInstance.Play();

            HP -= weapon.Damage;

            if (weapon.Type == WeaponType.DRUNK)
                StatusEffectBag.Add(new DebuffDrunken());
        }
    }
}