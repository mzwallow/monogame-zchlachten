using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffGolden : StatusEffect
    {
        public DebuffGolden(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
             Remaining = 1;
            Type = StatusEffectType.GOLDEN_SERPANT_BILE;
            
        }

        public override void Update(GameTime gameTime) { }
    }
}