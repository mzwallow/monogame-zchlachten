using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class Buffs : StatusEffect
    {
        public Buffs(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            // BuffType = BuffType.GOD_BlESSING;
        }

        public override void Update(GameTime gameTime) { }
    }
}