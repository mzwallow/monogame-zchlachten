using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class WeaponManager : IGameEntity
    {
        private const float WEAPON_START_POS_X = 149.5f;
        private const float WEAPON_START_POS_Y = 100f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private Texture2D _normalTxr;
        private Weapon _normalShot;

        public WeaponManager(
            World world,
            EntityManager entityManager, 
            Player demonLord, 
            Player brave, 
            params Texture2D[] weaponsTxr
        )
        {
            _world = world;
            _entityManager = entityManager;

            _demonLord = demonLord;
            _brave = brave;
            
            foreach (var txr in weaponsTxr)
            {
                _normalTxr = txr;
            }

            _normalShot = new NormalShot(
                _world, 
                _normalTxr, 
                new Vector2(WEAPON_START_POS_X, WEAPON_START_POS_Y), 
                WeaponType.NORMAL, 
                10
            );

            entityManager.AddEntry(_normalShot);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}