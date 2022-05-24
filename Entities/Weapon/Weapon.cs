using System;
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
        public Player Player, Enemy;

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
            Player = player;
            Enemy = enemy;

            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Size = new Vector2(Texture.Width * 0.0234375f, Texture.Height * 0.0234375f);
            Scale = Size / new Vector2(Texture.Width, Texture.Height);
        }

        protected Weapon(World world, Player player, Player enemy, Texture2D texture, Vector2 position)
        {
            _world = world;
            Player = player;
            Enemy = enemy;

            Texture = texture;
            TextureOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Size = new Vector2(Texture.Width * 0.0234375f, Texture.Height * 0.0234375f);
            Scale = Size / new Vector2(Texture.Width, Texture.Height);

            Position = position;

            Body = _world.CreateBody(position, 0f, BodyType.Dynamic);
            _weaponFixture = Body.CreateCircle(Size.X / 2, 1f);
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
        }

        private bool OnCollisionEventHandler(Fixture sender, Fixture other, Contact contact)
        {
            Tag otherTag = (Tag)other.Tag;

            if (otherTag.Type == TagType.STATUS_EFFECT)
            {
                Player.HoldStatusEffectBag.Add(otherTag.StatusEffect);

                if (Player.HoldStatusEffectBag.Count > 0)
                {
                    for (int i = Player.HoldStatusEffectBag.Count - 1; i > -1; --i)
                    {
                        var x = Player.HoldStatusEffectBag[i];

                        if (x.Type == StatusEffectType.SHIELD || x.Type == StatusEffectType.ATTACK_UP)
                        {
                            Player.StatusEffectBag.Add(x);
                            Player.HoldStatusEffectBag.RemoveAt(i);
                        }
                    }
                }
                return false;
            }

            if (otherTag.Type == TagType.PLAYER)
            {
                HasCollided = true;
                Enemy.HitBy(this);
                Player.BloodThirstGauge++;

                
                if (Player.HoldStatusEffectBag.Count > 0)
                {
                    foreach (StatusEffect status in Player.HoldStatusEffectBag)
                    {
                        if (status.Type != StatusEffectType.SHIELD && status.Type != StatusEffectType.ATTACK_UP)
                            Enemy.StatusEffectBag.Add(status);
                    }
                }

                if (Enemy.StatusEffectBag.Count > 0)
                {
                    for (int i = Enemy.StatusEffectBag.Count - 1; i > -1; --i)
                    {
                        var x = Enemy.StatusEffectBag[i];
                        if (x.Type == StatusEffectType.SHIELD)
                        {
                            Enemy.HP += Player.InHandWeapon.Damage;
                            Enemy.StatusEffectBag.RemoveAt(i);
                        }
                    }
                }
            }

            if (otherTag.Type == TagType.ENVIRONMENT)
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
            _weaponFixture.Tag = new Tag(Player, Enemy, TagType.WEAPON);
            _weaponFixture.OnCollision = OnCollisionEventHandler;
        }
    }
}