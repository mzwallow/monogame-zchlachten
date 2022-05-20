using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class ItemManager : IGameEntity
    {
        private readonly World _world;
        private readonly EntityManager _entityManager;
        private const float WORLD_TREE_X = 410f;
        private const float WORLD_TREE_Y = 626f;
        private const float SHEILD_X = 458f;
        private const float SHEILD_Y = 626f;
        private const float HOLY_WATER_X = 506f;
        private const float HOLY_WATER_Y = 626f;
        public ItemManager(
            World world,
            EntityManager entityManager,
            Texture2D[] itemTxr
        )
        {
            _world = world;
            _entityManager = entityManager;

        }
        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}