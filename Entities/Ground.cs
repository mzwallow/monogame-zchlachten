using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Content;

namespace Zchlachten.Entities
{
    public class Ground : IGameEntity
    {
        private readonly World _world;

        private Body _body;
        private Fixture _groundFixture;

        private Texture2D _texture;
        private Vector2 _textureOrigin;

        private Vector2 _size;
        private Vector2 _scale;

        public Ground(Texture2D texture, World world)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width/2, _texture.Height/2);
            
            _size = new Vector2(30f, 30f / (_texture.Width/_texture.Height));
            _scale = _size / new Vector2(_texture.Width, _texture.Height);
            
            _body = _world.CreateBody(new Vector2(15f, (_texture.Height * _scale.Y)/2));

            _groundFixture = _body.CreateRectangle(
                1280f, 
                _size.Y, 
                1f, 
                Vector2.Zero
            );
            _groundFixture.Tag = new Tag(TagType.ENVIRONMENT);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture, 
                _body.Position, 
                null, 
                Color.White, 
                _body.Rotation, 
                _textureOrigin, 
                _scale, 
                SpriteEffects.FlipVertically, 
                0f
            );
        }
    }
}