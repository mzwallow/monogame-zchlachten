using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class NormalShot : Weapon
    {
        private const int NORMAL_SHOT_DAMAGE = 10;

        public NormalShot(World world, Player enemy, Texture2D texture)
            : base(world, enemy, texture)
        {
            Damage = NORMAL_SHOT_DAMAGE;
        }

        public NormalShot(World world, Player enemy, Texture2D texture, Vector2 position)
            : base(world, enemy, texture, position)
        {
            Damage = NORMAL_SHOT_DAMAGE;
        }
    }
}