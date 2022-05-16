using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class Ground : IGameEntity
    {
        private readonly World _world;

        public Body Body;

        private Texture2D _texture;
        private Vector2 _textureOrigin;

        public Ground(Texture2D texture, World world)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width/2, _texture.Height/2);
            Body = _world.CreateBody(
                new Vector2(_textureOrigin.X, Globals.SCREEN_HEIGHT - _textureOrigin.Y),
                0f,
                BodyType.Static
            );
            var groundFixture = Body.CreateRectangle(
                (float)_texture.Width, 
                (float)_texture.Height, 
                1f, 
                Vector2.Zero
            );
            groundFixture.Restitution = 0.3f;
            groundFixture.Friction = 0.5f;
            // Body.Position = new Vector2(0, Globals.SCREEN_HEIGHT - texture.Height);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Body.Position, null, Color.White, Body.Rotation, _textureOrigin, 1f, SpriteEffects.None, 0f);
        }
    }
}