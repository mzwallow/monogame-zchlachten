using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class PlayerManager : IGameEntity
    {
        private const int DEMON_LORD_POS_X = 129;
        private const int DEMON_LORD_POS_Y = 528;

        private readonly EntityManager _entityManager;

        private Player _demonLord;

        public PlayerManager(EntityManager entityManager, Texture2D demonLordTxr, Texture2D braveTxr)
        {
            _entityManager = entityManager;
            _demonLord = new DemonLord
                (
                    demonLordTxr, 
                    new Vector2(DEMON_LORD_POS_X, DEMON_LORD_POS_Y), 
                    PlayerSide.DEMON_LORD
                );

            _entityManager.AddEntry(_demonLord);
        }

        public void Update(GameTime gameTime)
        { 
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        { 
        }
    }
}