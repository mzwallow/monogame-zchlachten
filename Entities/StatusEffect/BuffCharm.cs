using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BuffCharm : StatusEffect
    {
        public BuffCharm(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Remaining = 1;
            Type = StatusEffectType.CHARM;
        }
        public BuffCharm(World world, Texture2D texture)
            : base(world, texture)
        {
            Remaining = 1;
            Type = StatusEffectType.CHARM;
        }

        public override void Update(GameTime gameTime) { }
    }
}
