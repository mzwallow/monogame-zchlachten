using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffSlime : StatusEffect
    {
        private const int SLIME_MUCILAGE_REMAINING = 2;

        public DebuffSlime() : base()
        {
            Type = StatusEffectType.SLIME_MUCILAGE;
            Remaining = SLIME_MUCILAGE_REMAINING;
        }

        public DebuffSlime(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.SLIME_MUCILAGE;
            Remaining = SLIME_MUCILAGE_REMAINING;
        }

        public override void Update(GameTime gameTime) { }
    }
}
