using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffSlime : StatusEffect
    {
        public DebuffSlime(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Remaining = 2;
        }

        public override void Update(GameTime gameTime) { }
    }
}
