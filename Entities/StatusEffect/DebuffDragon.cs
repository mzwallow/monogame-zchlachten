using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffDragon : StatusEffect
    {
        public DebuffDragon(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Remaining = 3;
            Type = StatusEffectType.FIRE_DRAGON_BLOOD;
        }
        public DebuffDragon(World world, Texture2D texture)
            : base(world, texture)
        {
            Remaining = 3;
            Type = StatusEffectType.FIRE_DRAGON_BLOOD;
        }

        public override void Update(GameTime gameTime) { }
    }
}
