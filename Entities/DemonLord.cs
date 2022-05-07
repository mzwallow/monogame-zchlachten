using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class DemonLord : Player
    {
        public DemonLord(Texture2D texture, Vector2 position, PlayerSide playerSide)
            : base(texture, position, playerSide) { }

        public override void Update(GameTime gameTime) { }
    }
}