using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class WeaponManager : IGameEntity
    {
        // private const float DEMON_LORD_WEAPON_START_POS_X = 4.23046875f;
        // private const float DEMON_LORD_WEAPON_START_POS_Y = 5.12109375f;
        // private const float BRAVE_WEAPON_START_POS_X = 25.76953125f;
        // private const float BRAVE_WEAPON_START_POS_Y = 5.12109375f;
        private const float MIN_FORCE = 2f;
        private const float MAX_FORCE = 6f;
        private const float MIN_CHARGE = 0.1f;
        private const float MAX_CHARGE = 2f;
        private const float RANGE_FORCE = 0.4f;
        private const float DEMON_LORD_WEAPON_START_POS_X = 5f;
        private const float DEMON_LORD_WEAPON_START_POS_Y = 5f;
        private const float BRAVE_WEAPON_START_POS_X = 25f;
        private const float BRAVE_WEAPON_START_POS_Y = 5f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private Texture2D _arrowTxr;
        private Texture2D _demonEyeTxr, _lightSwordTxr;
        private Texture2D _inHandWeaponTxr, _weaponBagTxr, _chargeGauge;

        private float _rotation;
        private float _chargeMeter;
        private float _force = 2f;
        private float _Xforce = 1f;
        private bool _isShoot = false;

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

            _demonEyeTxr = weaponsTxrs[0];
            _lightSwordTxr = weaponsTxrs[1];

            _chargeMeter = MIN_CHARGE;
        }

        public void LoadContent(ContentManager content)
        {
            _arrowTxr = content.Load<Texture2D>("Players/Arrow");
            _inHandWeaponTxr = content.Load<Texture2D>("UI/InHandWeapon");
            _weaponBagTxr = content.Load<Texture2D>("UI/WeaponBag");
            _chargeGauge = content.Load<Texture2D>("Players/MinChargeGauge");
        }

        public void Update(GameTime gameTime)
        {
            Vector2 relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);
            // Vector2 relativeMousePosition = new Vector2(15f, 15f); // ! For development only

            switch (Globals.GameState)
            {
                case GameState.PLAYING:
                    // Globals.PlayerTurn = PlayerTurn.DEMON_LORD; // ! For development only
                    Vector2 weaponStartingPos;
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        weaponStartingPos = _demonLord.Body.Position
                            + new Vector2(_demonLord.Size.X / 2 + 0.5f,
                            _demonLord.Size.Y / 2 + 0.5f);

                        _rotation = (float)Math.Atan2(
                            relativeMousePosition.Y - (_demonLord.Body.Position.Y + _demonLord.Size.Y / 2),
                            relativeMousePosition.X - (_demonLord.Body.Position.X + _demonLord.Size.X / 2)
                        ); //  + MathHelper.ToRadians(90f)

                        if (_demonLord.InHandWeapon is null)
                            _demonLord.InHandWeapon = new NormalShot(_world, _brave, _demonEyeTxr);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (_chargeMeter > MAX_CHARGE)
                            {
                                _Xforce = -1f;
                            }
                            else if (_chargeMeter < MIN_CHARGE)
                            {
                                _Xforce = 1f;
                            }
                            _chargeMeter += (0.1f * _Xforce);
                            

                        }
                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Released && Globals.PreviousMouseState.LeftButton == ButtonState.Pressed)
                        {
                            _isShoot = true;
                            float x = (float)Math.Cos(_rotation);
                            float y = (float)Math.Sin(_rotation);

                            _demonLord.InHandWeapon.CreateBody(weaponStartingPos);
                            _demonLord.InHandWeapon.Body.ApplyLinearImpulse(new Vector2(x * (MIN_FORCE + RANGE_FORCE * (_chargeMeter / 0.2f)), y * (MIN_FORCE + RANGE_FORCE * (_chargeMeter / 0.2f))));

                            // * Debug                            
                            Debug.WriteLine("Demon lord speed: " + _demonLord.InHandWeapon.Body.LinearVelocity);

                            _entityManager.AddEntry(_demonLord.InHandWeapon);
                            Console.WriteLine((MIN_FORCE + RANGE_FORCE * (_chargeMeter / 0.2f)));

                        }

                    }
                    else
                    {
                        weaponStartingPos = new Vector2(
                            _brave.Body.Position.X - _brave.Size.X / 2 - 0.5f,
                            _brave.Body.Position.Y + _brave.Size.Y / 2 + 0.5f
                        );

                        _rotation = (float)Math.Atan2(
                            relativeMousePosition.Y - (_brave.Body.Position.Y + _brave.Size.Y / 2),
                            relativeMousePosition.X - (_brave.Body.Position.X - _brave.Size.X / 2)
                        ); //  + MathHelper.ToRadians(90f)

                        if (_brave.InHandWeapon is null)
                            _brave.InHandWeapon = new NormalShot(_world, _demonLord, _lightSwordTxr);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (_chargeMeter > MAX_CHARGE)
                            {
                                _Xforce = -1f;
                            }
                            else if (_chargeMeter < MIN_CHARGE)
                            {
                                _Xforce = 1f;
                            }
                            _chargeMeter += (0.1f * _Xforce);

                        }
                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Released && Globals.PreviousMouseState.LeftButton == ButtonState.Pressed)
                        {
                            _isShoot = true;
                            float x = (float)Math.Cos(_rotation);
                            float y = (float)Math.Sin(_rotation);

                            _brave.InHandWeapon.CreateBody(weaponStartingPos);
                            _brave.InHandWeapon.Body.ApplyLinearImpulse(new Vector2(x * (MIN_FORCE + RANGE_FORCE * (_chargeMeter / 0.2f)), y * (MIN_FORCE + RANGE_FORCE * (_chargeMeter / 0.2f))));

                            // * Debug
                            Debug.WriteLine("Brave speed: " + _brave.InHandWeapon.Body.LinearVelocity);

                            _entityManager.AddEntry(_brave.InHandWeapon);
                            Console.WriteLine((MIN_FORCE + RANGE_FORCE * (_chargeMeter / 0.2f)));

                        }

                    }
                    break;
                case GameState.POST_PLAY:
                    /*if (_demonLord.BloodThirstGauge == 2)
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
                    }*/
                    break;
            }

            // Remove collided weapon
            foreach (Weapon weapon in _entityManager.GetEntitiesOfType<Weapon>())
            {
                if (weapon.HasCollided)
                {
                    Debug.WriteLine("Hit");
                    _world.Remove(weapon.Body);
                    _entityManager.RemoveEntity(weapon);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Arrow
            if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
            {
                spriteBatch.Draw(
                    _arrowTxr,
                    _demonLord.Body.Position + new Vector2(_demonLord.Size.X / 2, _demonLord.Size.Y / 2),
                    null,
                    Color.White,
                    _rotation + MathHelper.ToRadians(90f),
                    new Vector2(_arrowTxr.Width / 2, _arrowTxr.Height),
                    new Vector2(0.05f, 1f) / new Vector2(_arrowTxr.Width, _arrowTxr.Height),
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw(
                         _chargeGauge,
                         _demonLord.Body.Position + new Vector2(_demonLord.Size.X - _chargeGauge.Width / 2, _demonLord.Size.Y / 2 + 1f),
                         null,
                         Color.White,
                         MathHelper.ToRadians(90f),
                         new Vector2(_chargeGauge.Width / 2, _chargeGauge.Height),
                         new Vector2(0.3f, MAX_CHARGE) / new Vector2(_chargeGauge.Width, _chargeGauge.Height),
                         SpriteEffects.None,
                         0f
                     );
                if (!_isShoot)
                    spriteBatch.Draw(
                         _chargeGauge,
                         _demonLord.Body.Position + new Vector2(_demonLord.Size.X - _chargeGauge.Width / 2, _demonLord.Size.Y / 2 + 0.5f),
                         null,
                         Color.White,
                         MathHelper.ToRadians(90f),
                         new Vector2(_chargeGauge.Width / 2, _chargeGauge.Height),
                         new Vector2(0.3f, _chargeMeter) / new Vector2(_chargeGauge.Width, _chargeGauge.Height),
                         SpriteEffects.None,
                         0f
                     );
            }
            else
            {
                spriteBatch.Draw(
                    _arrowTxr,
                    new Vector2(_brave.Body.Position.X - _brave.Size.X / 2, _brave.Body.Position.Y + _brave.Size.Y / 2),
                    null,
                    Color.White,
                    _rotation + MathHelper.ToRadians(90f),
                    new Vector2(_arrowTxr.Width / 2, _arrowTxr.Height),
                    new Vector2(0.05f, 1f) / new Vector2(_arrowTxr.Width, _arrowTxr.Height),
                    SpriteEffects.None,
                    0f
                );
                if (!_isShoot)
                    spriteBatch.Draw(
                        _chargeGauge,
                        _brave.Body.Position + new Vector2(_brave.Size.X - _chargeGauge.Height / 2, _brave.Size.Y / 2 + 0.5f),
                        null,
                        Color.White,
                        MathHelper.ToRadians(90f),
                        new Vector2(_chargeGauge.Width / 2, _chargeGauge.Height),
                        new Vector2(0.3f, _chargeMeter) / new Vector2(_chargeGauge.Width, _chargeGauge.Height),
                        SpriteEffects.None,
                        0f
                    );
            }


            // In-hand weapon
            spriteBatch.Draw(
                _inHandWeaponTxr,
                new Vector2(40.5f, 682.5f),
                null,
                Color.White,
                0f,
                new Vector2(_inHandWeaponTxr.Width / 2, _inHandWeaponTxr.Height / 2),
                1f,
                SpriteEffects.None,
                0f
            );

            // Weapon bag 1
            spriteBatch.Draw(
                _weaponBagTxr,
                new Vector2(99f, 686f),
                null,
                Color.White,
                0f,
                new Vector2(_weaponBagTxr.Width / 2, _weaponBagTxr.Height / 2),
                1f,
                SpriteEffects.None,
                0f
            );
            // Weapon bag 2
            spriteBatch.Draw(
                _weaponBagTxr,
                new Vector2(138f, 686f),
                null,
                Color.White,
                0f,
                new Vector2(_weaponBagTxr.Width / 2, _weaponBagTxr.Height / 2),
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