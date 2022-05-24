using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffDrunken : StatusEffect
    {
        private const int DRUNKEN_REMAINING = 1;

        public DebuffDrunken() : base()
        {
            Type = StatusEffectType.DRUNKEN;
            Remaining = DRUNKEN_REMAINING;
        }

        public DebuffDrunken(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Type = StatusEffectType.DRUNKEN;
            Remaining = DRUNKEN_REMAINING;
        }

        public override void Update(GameTime gameTime) { }
    }
}
