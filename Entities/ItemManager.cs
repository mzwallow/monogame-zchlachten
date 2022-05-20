using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class ItemManager : IGameEntity
    {
        private readonly World _world;
        private readonly EntityManager _entityManager;
        private const float DEMON_LORD_POS_X = 410;
        private const float DEMON_LORD_POS_Y = 566f;
        private const float BRAVE_POS_X = 1130.5f;
        private const float BRAVE_POS_Y = 566f;
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