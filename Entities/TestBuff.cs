using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class TestBuff : StatusEffect
    {
        public TestBuff(
            World world,
            Texture2D texture,
            Vector2 position
        ) : base(world, texture, position) { }

        public override void Update(GameTime gameTime)
        {
        }
    }
}