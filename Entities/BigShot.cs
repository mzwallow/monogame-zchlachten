using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BigShot : Weapon
    {
        private const int BIG_SHOT_DAMAGE = 20;

        public BigShot(World world, Player player, Texture2D texture, Vector2 position)
            : base(world, player, texture, position)
        {
            Damage = BIG_SHOT_DAMAGE;
        }
    }
}