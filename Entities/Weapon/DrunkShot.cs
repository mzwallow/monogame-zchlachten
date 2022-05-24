using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DrunkShot : Weapon
    {
        private const int DRUNK_SHOT_DAMAGE = 5;

        public DrunkShot(World world, Player player, Player enemy, Texture2D texture)
            : base(world, player, enemy, texture)
        {
            Type = WeaponType.DRUNK;
            Damage = DRUNK_SHOT_DAMAGE;
        }

        public override void Update(GameTime gameTime) { }
    }
}