using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class WeaponManager : IGameEntity
    {
        private const float DEMON_LORD_WEAPON_START_POS_X = 180.5f;
        private const float DEMON_LORD_WEAPON_START_POS_Y = 501.5f;
        private const float BRAVE_WEAPON_START_POS_X = 1099.5f;
        private const float BRAVE_WEAPON_START_POS_Y = 501.5f;

        private Vector2 _demonLordWeaponStartPos = new Vector2(DEMON_LORD_WEAPON_START_POS_X, DEMON_LORD_WEAPON_START_POS_Y);
        private Vector2 _braveWeaponStartPos = new Vector2(BRAVE_WEAPON_START_POS_X, BRAVE_WEAPON_START_POS_Y);

        private readonly ContentManager _content;
        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private Texture2D[] _weaponTxrs;
        private Texture2D _demonEyeTxr, _lightSwordTxr;
        private Texture2D _inHandWeaponTxr, _weaponBagTxr;

        public WeaponManager(
            ContentManager content,
            World world,
            EntityManager entityManager,
            Player demonLord,
            Player brave,
            params Texture2D[] weaponsTxrs
        )
        {
            _content = content;
            _world = world;
            _entityManager = entityManager;

            _demonLord = demonLord;
            _brave = brave;

            _weaponTxrs = weaponsTxrs;
            _demonEyeTxr = weaponsTxrs[0];
            _lightSwordTxr = weaponsTxrs[1];
        }

        public void LoadContent()
        {
            _inHandWeaponTxr = _content.Load<Texture2D>("UI/InHandWeapon");
            _weaponBagTxr = _content.Load<Texture2D>("UI/WeaponBag");
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.PLAYING:
                    Player enemy;
                    NormalShot normalShot;
                    Texture2D normalShotTxr;
                    Vector2 weaponStartingPos;
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        enemy = _brave;
                        normalShotTxr = _weaponTxrs[0];
                        weaponStartingPos = new Vector2(DEMON_LORD_WEAPON_START_POS_X, DEMON_LORD_WEAPON_START_POS_Y);

                        if (_demonLord.InHandWeapon is null)
                        {
                            normalShot = new NormalShot(
                                _world,
                                enemy,
                                normalShotTxr,
                                weaponStartingPos
                            );

                            _demonLord.InHandWeapon = normalShot;
                            _entityManager.AddEntry(_demonLord.InHandWeapon);
                        }
                    }
                    else
                    {
                        enemy = _demonLord;
                        normalShotTxr = _weaponTxrs[1];
                        weaponStartingPos = new Vector2(BRAVE_WEAPON_START_POS_X, BRAVE_WEAPON_START_POS_Y);

                        if (_brave.InHandWeapon is null)
                        {
                            normalShot = new NormalShot(
                                _world,
                                enemy,
                                normalShotTxr,
                                weaponStartingPos
                            );

                            _brave.InHandWeapon = normalShot;
                            _entityManager.AddEntry(_brave.InHandWeapon);
                        }
                    }
                    break;
                case GameState.POST_PLAY:
                    if (_demonLord.BloodThirstGauge == 2)
                    {
                        _demonLord.BloodThirstGauge = 0;

                        var newWeapon = RandomWeapon(_demonLord, _demonEyeTxr, _demonLordWeaponStartPos);
                        if (_demonLord.WeaponsBag.Count == 2)
                        {
                            Debug.WriteLine("Demon Lord's weapon bag is full. Brave got '", newWeapon, "' instead.");
                            _brave.WeaponsBag.Add(newWeapon);
                        }
                        _demonLord.WeaponsBag.Add(newWeapon);
                        Debug.WriteLine("Demon Lord got ", newWeapon);
                    }
                    else if (_brave.BloodThirstGauge == 2)
                    {
                        _brave.BloodThirstGauge = 0;

                        var newWeapon = RandomWeapon(_brave, _lightSwordTxr, _braveWeaponStartPos);
                        if (_brave.WeaponsBag.Count == 2)
                        {
                            Debug.WriteLine("Brave's weapon bag is full. Demon Lord got '", newWeapon, "' instead.");
                            _demonLord.WeaponsBag.Add(newWeapon);
                        }
                        _brave.WeaponsBag.Add(newWeapon);
                        Debug.WriteLine("Brave got ", newWeapon);
                    }
                    break;
            }

            // Remove collided weapon
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
            spriteBatch.Draw(
                _inHandWeaponTxr, 
                new Vector2(40.5f, 682.5f),
                null,
                Color.White, 
                0f, 
                new Vector2(_inHandWeaponTxr.Width/2, _inHandWeaponTxr.Height/2),
                1f,
                SpriteEffects.None,
                0f
            );
            spriteBatch.Draw(
                _weaponBagTxr, 
                new Vector2(99f, 686f),
                null,
                Color.White, 
                0f, 
                new Vector2(_weaponBagTxr.Width/2, _weaponBagTxr.Height/2),
                1f,
                SpriteEffects.None,
                0f
            );
            spriteBatch.Draw(
                _weaponBagTxr, 
                new Vector2(138f, 686f),
                null,
                Color.White, 
                0f, 
                new Vector2(_weaponBagTxr.Width/2, _weaponBagTxr.Height/2),
                1f,
                SpriteEffects.None,
                0f
            );
        }

        private Weapon RandomWeapon(Player player, Texture2D weaponTxr, Vector2 position)
        {
            var values = Enum.GetValues(typeof(WeaponType));
            var weaponType = (WeaponType)values.GetValue(new Random().Next(values.Length));

            Weapon weapon;
            switch (weaponType)
            {
                case WeaponType.BIG:
                    weapon = new BigShot(_world, player, weaponTxr, position);
                    break;
                case WeaponType.DOUBLE:
                    weapon = new DoubleShot(_world, player, weaponTxr, position);
                    break;
                case WeaponType.SPLIT:
                    weapon = new SplitShot(_world, player, weaponTxr, position);
                    break;
                case WeaponType.CHARM:
                    weapon = new CharmShot(_world, player, weaponTxr, position);
                    break;
                default:
                    weapon = RandomWeapon(player, weaponTxr, position);
                    break;
            }

            return weapon;
        }
    }
}