using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Common;
using tainicom.Aether.Physics2D.Collision.Shapes;

namespace Zchlachten.Entities
{
    public class CorpsesPile : IGameEntity
    {
        private readonly World _world;

        private Body _body;
        private Fixture _corpsesPileFixture;

        private Texture2D _texture;
        private Vector2 _textureOrigin;

        private Vector2 _size;
        private Vector2 _scale;

        public CorpsesPile(Texture2D texture, World world)
        {
            _world = world;

            _texture = texture;
            _textureOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            _size = new Vector2(17.953125f, 5.15625f);
            _scale = _size / new Vector2(_texture.Width, _texture.Height);

            _body = _world.CreateBody(new Vector2(
                Globals.Camera.Width / 2,
                5.25f
            ));

            Vertices vertices = new Vertices() {
                new Vector2(-_size.X/2, -_size.Y/2),
                new Vector2(0, _size.Y/2),
                new Vector2(_size.X/2, -_size.Y/2),
            };

            _corpsesPileFixture = _body.CreatePolygon(vertices, 1f);
            _corpsesPileFixture.Tag = new Tag(TagType.ENVIRONMENT);
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