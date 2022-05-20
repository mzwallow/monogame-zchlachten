using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using tainicom.Aether.Physics2D.Common;

namespace Zchlachten.Entities
{
    public abstract class StatusEffect : IGameEntity
    {
        private readonly World _world;

        private Texture2D _texture;
        private Vector2 _textureOrigin;
        public Body Body;
        private Fixture _statusEffectFixture;
        public StatusEffectType statusEffectType;
        public bool HasCollided = false;
        private Vector2 _size;
        private Vector2 _scale;

        public StatusEffect(World world, Texture2D texture, Vector2 position)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            _size = new Vector2(_texture.Width * 0.0234375f, _texture.Height * 0.0234375f)*1.25f;
            _scale = _size / new Vector2(_texture.Width, _texture.Height);

            Body = _world.CreateBody(position);

            _statusEffectFixture = Body.CreateCircle(_size.X / 2, 1f);
            _statusEffectFixture.Tag = "statusEffects";

            _statusEffectFixture.OnCollision = OnCollisionEventHandler;

            // Body = _world.CreateCircle(_size.X / 2, 1f, position);
            // Body.Tag = "statusEffects";
            // Body.OnCollision += OnCollisionEventHandler;
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
                SpriteEffects.FlipVertically,
                0f
            );

            // Globals.DebugView.DrawShape(_statusEffectFixture, new Transform(Body.Position, Body.Rotation), Color.Red);
        }

        private bool OnCollisionEventHandler(Fixture sender, Fixture other, Contact contact)
        {
            HasCollided = true;
            return false;
        }
    }
}