using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DemonLord : Player
    {
        public DemonLord(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Side = PlayerSide.DEMON_LORD;
        }

        public override void Update(GameTime gameTime) { }
    }
}