using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DoubleShot : Weapon
    {
        private const int DOUBLE_SHOT_DAMAGE = 20;

        public DoubleShot(World world, Player player, Texture2D texture, Vector2 position)
            : base(world, player, texture, position)
        {
            Damage = DOUBLE_SHOT_DAMAGE;
        }
    }
}