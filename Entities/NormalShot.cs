using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class NormalShot : Weapon
    {
        private const int NORMAL_SHOT_DAMAGE = 10;

        public NormalShot(World world, Player player, Texture2D texture, Vector2 position)
            : base(world, player, texture, position)
        {
            WeaponType = WeaponType.NORMAL;
            Damage = NORMAL_SHOT_DAMAGE;
        }
    }
}