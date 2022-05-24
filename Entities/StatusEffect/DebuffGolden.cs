using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffGolden : StatusEffect
    {
        private const int GOLDEN_SERPANT_BILE_REMAINING = 1;

        public DebuffGolden() : base()
        {
            Type = StatusEffectType.GOLDEN_SERPANT_BILE;
            Remaining = GOLDEN_SERPANT_BILE_REMAINING;
        }

        public DebuffGolden(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.GOLDEN_SERPANT_BILE;
            Remaining = GOLDEN_SERPANT_BILE_REMAINING;

        }

        public override void Update(GameTime gameTime) { }
    }
}
