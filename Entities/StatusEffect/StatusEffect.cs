using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using tainicom.Aether.Physics2D.Common;

using Zchlachten.Graphics;

namespace Zchlachten.Entities
{
    public abstract class StatusEffect : IGameEntity
    {
        private readonly World _world;

        public StatusEffectType Type { get; protected set; }
        public int Remaining;
        public int HoldRemaining = 1;

        public Vector2 Position => Body.Position;
        public float Width => _sprite.Width;
        public float Height => _sprite.Height;

        private Sprite _sprite { get; set; }
        public Body Body { get; private set; }

        public bool HasCollided { get; private set; } = false;

        public StatusEffect() { }

        public StatusEffect(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _sprite = new Sprite(texture, new Vector2(1.25f));

            Body = _world.CreateBody(position);

            var statusEffectFixture = Body.CreateCircle(Width / 2, 1f);
            statusEffectFixture.Tag = new Tag(TagType.STATUS_EFFECT, this);
            statusEffectFixture.OnCollision += OnCollisionEventHandler;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, Position);
        }

        private bool OnCollisionEventHandler(Fixture sender, Fixture other, Contact contact)
        {
            HasCollided = true;
            return false;
        }
    }
}