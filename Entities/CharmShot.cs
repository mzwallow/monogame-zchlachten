using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class CharmShot : Weapon
    {
        private const int CHARM_SHOT_DAMAGE = 0;

        public CharmShot(World world, Player player, Player enemy, Texture2D texture)
            : base(world, player, enemy, texture)
        {
            Type = WeaponType.CHARM;
            Damage = CHARM_SHOT_DAMAGE;
        }

        public CharmShot(World world, Player player, Player enemy, Texture2D texture, Vector2 position)
            : base(world, player, enemy, texture, position)
        {
            Type = WeaponType.CHARM;
            Damage = CHARM_SHOT_DAMAGE;
        }

        public override void Update(GameTime gameTime) { }
    }
}