using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class StatusEffectManager : IGameEntity
    {
        private readonly World _world;
        private readonly EntityManager _entityManager;
        
        private StatusEffect _testBuff;

        public StatusEffectManager(
            World world,
            EntityManager entityManager,
            params Texture2D[] statusEffectTxrs
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