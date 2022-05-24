using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class NormalShot : Weapon
    {
        private const int NORMAL_SHOT_DAMAGE = 150;
        public NormalShot(World world, Player player, Player enemy, Texture2D texture)
            : base(world, player, enemy, texture)
        {
            Type = WeaponType.NORMAL;
            Damage = NORMAL_SHOT_DAMAGE;
        }

        public override void Update(GameTime gameTime){}
    }
}