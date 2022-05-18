using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class StatusEffectManager : IGameEntity
    {
        private readonly World _world;
        private readonly EntityManager _entityManager;
        
        private PlayState _playState;

        private StatusEffect _testBuff;

        public StatusEffectManager(
            World world,
            EntityManager entityManager,
            PlayState playState,
            params Texture2D[] statusEffectTxrs
        )
        {
            _world = world;
            _entityManager = entityManager;

            _playState = playState;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}