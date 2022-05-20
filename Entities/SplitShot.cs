using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class SplitShot : Weapon
    {
        private const int SPLIT_SHOT_DAMAGE = 20;

        public SplitShot(World world, Player player, Texture2D texture, Vector2 position)
            : base(world, player, texture, position)
        {
            Damage = SPLIT_SHOT_DAMAGE;
        }
    }
}