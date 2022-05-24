using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework.Audio;

namespace Zchlachten.Entities
{
    public class Brave : Player
    {
        public Brave(World world, Texture2D texture, Vector2 position)
            : base(world, texture, position)
        {
            Side = PlayerSide.BRAVE;
        }

        public override void Update(GameTime gameTime) { }
    }
}