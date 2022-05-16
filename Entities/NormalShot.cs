using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class NormalShot : Weapon
    {
        public NormalShot(
            World world,
            Texture2D texture, 
            Vector2 position,
            WeaponType weaponType,
            int damage
        ) : base(world, texture, position, weaponType, damage) {}

        public override void Update(GameTime gameTime) { }
    }
}