using System.Diagnostics;
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
        private readonly PlayState _state;
        private readonly Player _demonLord, _brave;

        private Texture2D _normalTxr;
        private PlayState _playState;

        public WeaponManager(
            World world,
            EntityManager entityManager, 
            PlayState state,
            PlayerTurn playerTurn,
            Player demonLord, 
            Player brave, 
            params Texture2D[] weaponsTxr
        )
        {
            _world = world;
            _entityManager = entityManager;
            _playState = state;

            _demonLord = demonLord;
            _brave = brave;
            
            foreach (var txr in weaponsTxr)
            {
                _normalTxr = txr;
            }

            var _normalShot = new NormalShot(
                _world,
                _demonLord,
                _normalTxr, 
                new Vector2(WEAPON_START_POS_X, WEAPON_START_POS_Y)
            );

            _entityManager.AddEntry(_normalShot);
        }

        public void Update(GameTime gameTime)
        {
            

            foreach (Weapon weapon in _entityManager.GetEntitiesOfType<Weapon>())
            {
                if (weapon.HasCollided)
                {
                    _world.Remove(weapon.Body);
                    _entityManager.RemoveEntity(weapon);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}