using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public abstract class Player : IGameEntity
    {
        public PlayerSide PlayerSide { get; private set; }
        public Vector2 Position { get; private set; }

        protected Texture2D _texture;

        protected Player(Texture2D texture, Vector2 position, PlayerSide playerSide)
        {
            _texture = texture;
            Position = position;
            PlayerSide = playerSide;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}