using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BuffAttack : StatusEffect
    {
        private const int ATTACK_UP_REMAINING = 2;

        public BuffAttack() : base()
        {
            Type = StatusEffectType.ATTACK_UP;
            Remaining = ATTACK_UP_REMAINING;
        }

        public BuffAttack(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.ATTACK_UP;
            Remaining = ATTACK_UP_REMAINING;
        }
        
        public override void Update(GameTime gameTime) { }
    }
}
