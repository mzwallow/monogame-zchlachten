using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class Brave : Player
    {
        public Brave(World world, Texture2D texture, Vector2 position, PlayerSide playerSide)
            : base(world, texture, position, playerSide) { }

        public override void Update(GameTime gameTime) { }
    }
}