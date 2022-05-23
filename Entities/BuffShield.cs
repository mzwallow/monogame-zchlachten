using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BuffShield : StatusEffect
    {
        public BuffShield(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Remaining = 3;
            Type = StatusEffectType.SHIELD;
        }
        public BuffShield(World world, Texture2D texture)
            : base(world, texture)
        {
            Remaining = 3;
            Type = StatusEffectType.SHIELD;
        }

        public override void Update(GameTime gameTime) { }
    }
}
