using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BuffShield : StatusEffect
    {
        private const int SHIELD_REMAINING = 3;

        public BuffShield(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.SHIELD;
            Remaining = SHIELD_REMAINING;
        }
        public BuffShield() : base()
        {
            Type = StatusEffectType.SHIELD;
            Remaining = SHIELD_REMAINING;
        }

        public override void Update(GameTime gameTime) { }
    }
}
