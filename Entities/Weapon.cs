using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class Weapon : IGameEntity
    {
        public WeaponType WeaponType { get; protected set; }
        public Vector2 Position { get; set; }
        public int Damage { get; protected set; }

        private Texture2D _texture;
        private int _width;
        private int _height;

        public Weapon() { }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}