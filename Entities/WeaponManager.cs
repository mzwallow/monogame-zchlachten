using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class WeaponManager : IGameEntity
    {
        private const float DEMON_LORD_WEAPON_START_POS_X = 180.5f;
        private const float DEMON_LORD_WEAPON_START_POS_Y = 501.5f;
        private const float BRAVE_WEAPON_START_POS_X = 149.5f;
        private const float BRAVE_WEAPON_START_POS_Y = 100f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private Texture2D _normalTxr;

        public WeaponManager(
            World world,
            EntityManager entityManager,
            Player demonLord, 
            Player brave, 
            params Texture2D[] weaponsTxrs
        )
        {
            _world = world;
            _entityManager = entityManager;

            _demonLord = demonLord;
            _brave = brave;
            
            foreach (var txr in weaponsTxrs)
            {
                _normalTxr = txr;
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.PLAYING:
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        var _normalShot = new NormalShot(
                            _world,
                            _brave,
                            _normalTxr, 
                            new Vector2(DEMON_LORD_WEAPON_START_POS_X, DEMON_LORD_WEAPON_START_POS_Y)
                        );
                    }

                    break;
            }

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