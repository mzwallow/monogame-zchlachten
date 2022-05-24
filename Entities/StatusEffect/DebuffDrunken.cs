using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebuffDrunken : StatusEffect
    {
        public DebuffDrunken(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Remaining = 1;
            Type = StatusEffectType.DRUNKEN;
        }
        public DebuffDrunken(World world, Texture2D texture)
            : base(world, texture)
        {
            Remaining = 1;
            Type = StatusEffectType.DRUNKEN;
        }

        public override void Update(GameTime gameTime) { }
    }
}
