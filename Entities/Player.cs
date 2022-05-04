using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class Player : IGameEntity
    {
        public PlayerSide PlayerSide;
        public Vector2 Position;

        private Texture2D _texture;

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}