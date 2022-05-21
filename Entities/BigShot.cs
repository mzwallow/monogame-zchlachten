using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class BigShot : Weapon
    {
        private const int BIG_SHOT_DAMAGE = 20;

        public BigShot(World world, Player player, Player enemy, Texture2D texture)
            : base(world, player, enemy, texture)
        {
            Type = WeaponType.BIG;
            Damage = BIG_SHOT_DAMAGE;
        }

        public BigShot(World world, Player player, Player enemy, Texture2D texture, Vector2 position)
            : base(world, player, enemy, texture, position)
        {
            Type = WeaponType.BIG;
            Damage = BIG_SHOT_DAMAGE;
        }

        public override void Update(GameTime gameTime) { }
    }
}