using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class PlayerManager : IGameEntity
    {
        private const float DEMON_LORD_POS_X = 149.5f;
        private const float DEMON_LORD_POS_Y = 566f;
        private const float BRAVE_POS_X = 1130.5f;
        private const float BRAVE_POS_Y = 566f;

        private readonly EntityManager _entityManager;
        private readonly World _world;
        private readonly PlayState _state;

        public Player DemonLord, Brave;

        public PlayerManager(
            World world, 
            EntityManager entityManager,
            PlayState state,
            Texture2D demonLordTxr, 
            Texture2D braveTxr)
        {
            _world = world;
            _entityManager = entityManager;
            _state = state;

            DemonLord = new DemonLord(
                    _world,
                    demonLordTxr,
                    new Vector2(DEMON_LORD_POS_X, DEMON_LORD_POS_Y)
                );
            Brave = new Brave(
                    _world,
                    braveTxr,
                    new Vector2(BRAVE_POS_X, BRAVE_POS_Y)
                );

            _entityManager.AddEntry(DemonLord);
            _entityManager.AddEntry(Brave);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}