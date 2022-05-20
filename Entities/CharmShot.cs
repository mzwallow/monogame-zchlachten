using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class CharmShot : Weapon
    {
        private const int CHARM_SHOT_DAMAGE = 20;

        public CharmShot(World world, Player player, Texture2D texture, Vector2 position)
            : base(world, player, texture, position)
        {
            Damage = CHARM_SHOT_DAMAGE;
        }
    }
}