using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class PowerShot : Weapon
    {
        private const int POWER_SHOT_DAMAGE = 20;

        public PowerShot(World world, Player player, Player enemy, Texture2D texture)
            : base(world, player, enemy, texture)
        {
            Type = WeaponType.POWER;
            Damage = POWER_SHOT_DAMAGE;
        }

        public override void Update(GameTime gameTime) { }
    }
}