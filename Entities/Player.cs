using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public abstract class Player : IGameEntity
    {
        private readonly World _world;

        public PlayerSide PlayerSide { get; protected set; }
        public Body Body { get; protected set; }

        protected Texture2D _texture;
        private Vector2 _textureOrigin;

        protected Player(
            World world,
            Texture2D texture,
            Vector2 position,
            PlayerSide playerSide
        )
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            Body = _world.CreateBody(position, 0f, BodyType.Static);
            var playerFixture = Body.CreateRectangle(_texture.Width, _texture.Height, 1f, Vector2.Zero);
            playerFixture.Restitution = 0.3f;
            playerFixture.Friction = 0.5f;

            PlayerSide = playerSide;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Body.Position, null, Color.White, Body.Rotation, _textureOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}