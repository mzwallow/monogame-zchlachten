using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffDragon : StatusEffect
    {
        private const int FIRE_DRAGON_BLOOD_REMAINING = 2;

        public DebuffDragon() : base()
        {
            Type = StatusEffectType.FIRE_DRAGON_BLOOD;
            Remaining = FIRE_DRAGON_BLOOD_REMAINING;
        }

        public DebuffDragon(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.FIRE_DRAGON_BLOOD;
            Remaining = FIRE_DRAGON_BLOOD_REMAINING;
        }

        public override void Update(GameTime gameTime) { }
    }
}
