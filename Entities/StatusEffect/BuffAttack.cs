using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BuffAttack : StatusEffect
    {
        public BuffAttack(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.ATTACK;
            Remaining = 2;
        }
        
        public override void Update(GameTime gameTime) { }
    }
}
