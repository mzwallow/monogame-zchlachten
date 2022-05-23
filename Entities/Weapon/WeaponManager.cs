using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Graphics;

namespace Zchlachten.Entities
{
    public class WeaponManager : IGameEntity
    {
        private const float MIN_FORCE = 2f;
        private const float MAX_FORCE = 6f;
        private const float MIN_CHARGE = 0f;
        private const float MAX_CHARGE = 2f;

        private const float DEMON_LORD_IN_HAND_WEAPON_POS_X = 0.8f;
        private const float DEMON_LORD_IN_HAND_WEAPON_POS_Y = 3.5f;
        private const float DEMON_LORD_WEAPON_BAG1_POS_X = 0.8f;
        private const float DEMON_LORD_WEAPON_BAG1_POS_Y = 4.8f;
        private const float DEMON_LORD_WEAPON_BAG2_POS_X = 0.8f;
        private const float DEMON_LORD_WEAPON_BAG2_POS_Y = 5.8f;

        private const float BRAVE_IN_HAND_WEAPON_POS_X = 29.2f;
        private const float BRAVE_IN_HAND_WEAPON_POS_Y = 3.5f;
        private const float BRAVE_WEAPON_BAG1_POS_X = 29.2f;
        private const float BRAVE_WEAPON_BAG1_POS_Y = 4.8f;
        private const float BRAVE_WEAPON_BAG2_POS_X = 29.2f;
        private const float BRAVE_WEAPON_BAG2_POS_Y = 5.8f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private Texture2D _arrowTxr;
        private Texture2D _demonEyeTxr, _lightSwordTxr, _cursedEye, _lightChakra, _mead;
        private Texture2D _flagLittleLeft, _flagLittleRight, _flagFullRight, _flagFullLeft, _NoWind;
        private Texture2D _inHandWeaponTxr, _weaponBagTxr, _chargeGaugetxr;
        private Sprite _inHandWeaponBGSprite, _weaponBagBGSprite, _flag;

        static Random r = new Random();

        private float _rotation;
        private float _rangeForce = 0.4f;
        private float _chargeGauge;
        private float _MeterControl = 1f;
        private int _wind = 0;

        public WeaponManager(
            World world,
            EntityManager entityManager,
            Player demonLord,
            Player brave,
            Texture2D[] weaponsTxrs
        )
        {
            _world = world;
            _entityManager = entityManager;

            _demonLord = demonLord;
            _brave = brave;

            _demonEyeTxr = weaponsTxrs[0];
            _lightSwordTxr = weaponsTxrs[1];
            _cursedEye = weaponsTxrs[2];
            _lightChakra = weaponsTxrs[3];
            _mead = weaponsTxrs[4];

            _chargeGauge = MIN_CHARGE;
        }

        public void LoadContent(ContentManager content)
        {
            _arrowTxr = content.Load<Texture2D>("Players/Arrow");
            _inHandWeaponTxr = content.Load<Texture2D>("UI/InHandWeapon");
            _weaponBagTxr = content.Load<Texture2D>("UI/WeaponBag");
            _chargeGaugetxr = content.Load<Texture2D>("Players/MinChargeGauge");

            Texture2D inHandWeaponBGTxr = content.Load<Texture2D>("UI/InHandWeapon");
            _inHandWeaponBGSprite = new Sprite(inHandWeaponBGTxr);

            Texture2D weaponBagBGTxr = content.Load<Texture2D>("UI/WeaponBag");
            _weaponBagBGSprite = new Sprite(weaponBagBGTxr);

            _flagFullLeft = content.Load<Texture2D>("Environments/FullWindLeft");
            _flagLittleLeft = content.Load<Texture2D>("Environments/LittleWindLeft");

            _NoWind = content.Load<Texture2D>("Environments/NoWind");

            _flagLittleRight = content.Load<Texture2D>("Environments/LittleWindRight");
            _flagFullRight = content.Load<Texture2D>("Environments/FullWindRight");

        }

        public void Update(GameTime gameTime)
        {
            Vector2 relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);
            //Debug.WriteLine(relativeMousePosition);

            switch (Globals.GameState)
            {
                case GameState.PRE_PLAY:
                    _rangeForce = 0.4f;
                    if (Globals.TotalTurn % 3 == 0)
                    {
                        _wind = r.Next(-2, 2);
                        _world.Gravity = new Vector2(_wind, -12f);
                        Console.WriteLine("Wind: " + _wind);
                    }
                    switch (_wind)
                    {
                        case 2:
                            _flag = new Sprite(_flagFullRight);
                            break;
                        case 1:
                            _flag = new Sprite(_flagLittleRight);
                            break;
                        case 0:
                            _flag = new Sprite(_NoWind);
                            break;
                        case -1:
                            _flag = new Sprite(_flagLittleLeft);
                            break;
                        case -2:
                            _flag = new Sprite(_flagFullLeft);
                            break;
                    }

                    Console.WriteLine("Player Turn: " + Globals.PlayerTurn);

                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {

                        if (_demonLord.StatusEffectBag.Count > 0)
                        {
                            foreach (StatusEffect status in _demonLord.StatusEffectBag)
                            {
                                if (status.Type == StatusEffectType.DRUNKEN)
                                {
                                    Console.WriteLine("Demon Drunken");
                                    _rangeForce = 0.2f;

                                }
                            }
                        }
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        if (_brave.StatusEffectBag.Count > 0)
                        {
                            foreach (StatusEffect status in _brave.StatusEffectBag)
                            {
                                if (status.Type == StatusEffectType.DRUNKEN)
                                {
                                    Console.WriteLine("Brave Drunken");
                                    _rangeForce = 0.2f;

                                }
                            }
                        }
                    } // End if

                    // Globals.GameState = GameState.PLAYING;
                    break;
                case GameState.PLAYING:
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        // Handle when in-hand weapon is null
                        InHandWeaponHandler(_demonLord, _brave, _demonEyeTxr);

                        // Check shootable angle
                        if (relativeMousePosition.X >= _demonLord.Body.Position.X + _demonLord.Size.X / 2
                                && relativeMousePosition.Y >= _demonLord.Body.Position.Y + _demonLord.Size.Y / 2)
                        {
                            _rotation = (float)Math.Atan2(
                                relativeMousePosition.Y - (_demonLord.Body.Position.Y + _demonLord.Size.Y / 2),
                                relativeMousePosition.X - (_demonLord.Body.Position.X + _demonLord.Size.X / 2)
                            );

                            // Handle shooting force when holding mouse
                            ShootingForceHandler();

                            // Handle shooting when mouse released
                            Vector2 weaponPosition = new Vector2(
                                _demonLord.Body.Position.X + _demonLord.Size.X / 2 + 0.5f,
                                _demonLord.Body.Position.Y + _demonLord.Size.Y / 2 + 0.5f
                            );
                            ShootingHandler(_demonLord, _rotation, weaponPosition);
                        }
                        else _chargeGauge = 0;

                        // Handle weapon selection
                        Vector2 weaponBagOnePosition = new Vector2(
                            DEMON_LORD_WEAPON_BAG1_POS_X,
                            DEMON_LORD_WEAPON_BAG1_POS_Y
                        );
                        Vector2 weaponBagTwoPosition = new Vector2(
                            DEMON_LORD_WEAPON_BAG2_POS_X,
                            DEMON_LORD_WEAPON_BAG2_POS_Y
                        );
                        WeaponSelectionHandler(
                            relativeMousePosition,
                            _demonLord,
                            weaponBagOnePosition,
                            weaponBagTwoPosition
                        );
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        // Handle when in-hand weapon is null
                        InHandWeaponHandler(_brave, _demonLord, _lightSwordTxr);

                        // Check shootable angle
                        if (relativeMousePosition.X <= _brave.Body.Position.X - _brave.Size.X / 2
                                && relativeMousePosition.Y >= _brave.Body.Position.Y + _brave.Size.Y / 2)
                        {
                            _rotation = (float)Math.Atan2(
                                relativeMousePosition.Y - (_brave.Body.Position.Y + _brave.Size.Y / 2),
                                relativeMousePosition.X - (_brave.Body.Position.X - _brave.Size.X / 2)
                            );

                            // Handle shooting force when holding mouse
                            ShootingForceHandler();

                            // Handle shooting when mouse released
                            Vector2 weaponPosition = new Vector2(
                                _brave.Body.Position.X - _brave.Size.X / 2 - 0.5f,
                                _brave.Body.Position.Y + _brave.Size.Y / 2 + 0.5f
                            );
                            ShootingHandler(_brave, _rotation, weaponPosition);
                        }
                        else _chargeGauge = 0;

                        // Handle weapon selection
                        Vector2 weaponBagOnePosition = new Vector2(
                            BRAVE_WEAPON_BAG1_POS_X,
                            BRAVE_WEAPON_BAG1_POS_Y
                        );
                        Vector2 weaponBagTwoPosition = new Vector2(
                            BRAVE_WEAPON_BAG2_POS_X,
                            BRAVE_WEAPON_BAG2_POS_Y
                        );
                        WeaponSelectionHandler(
                            relativeMousePosition,
                            _brave,
                            weaponBagOnePosition,
                            weaponBagTwoPosition
                        );
                    } // End if

                    // Remove collided weapon & handle gamestate and player turn
                    foreach (Weapon weapon in _entityManager.GetEntitiesOfType<Weapon>())
                    {
                        if (weapon.HasCollided)
                        {
                            // Remove physics body and weapon object
                            _world.Remove(weapon.Body);
                            _entityManager.RemoveEntity(weapon);
                            Globals.IsShooting = false;

                            // Clear player's in-hand weapon
                            if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                                _demonLord.InHandWeapon = null;
                            else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                                _brave.InHandWeapon = null;

                            Globals.GameState = GameState.POST_PLAY;
                        }
                    }
                    // Handle player blood thirst gauge
                    BloodThirstGaugeHandler();
                    break;
                case GameState.POST_PLAY:
                    // Globals.GameState = GameState.PRE_PLAY;
                    break;
            } // End switch
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
                if (!Globals.IsShooting)
                    spriteBatch.Draw(
                         _chargeGaugetxr,
                         _demonLord.Body.Position + new Vector2(_demonLord.Size.X - _chargeGaugetxr.Width / 2, _demonLord.Size.Y / 2 + 0.5f),
                         null,
                         Color.White,
                         MathHelper.ToRadians(90f),
                         new Vector2(_chargeGaugetxr.Width / 2, _chargeGaugetxr.Height),
                         new Vector2(0.3f, _chargeGauge) / new Vector2(_chargeGaugetxr.Width, _chargeGaugetxr.Height),
                         SpriteEffects.None,
                         0f
                     );
            }
            else if ((Globals.PlayerTurn == PlayerTurn.BRAVE))
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
                if (!Globals.IsShooting)
                    spriteBatch.Draw(
                        _chargeGaugetxr,
                        _brave.Body.Position + new Vector2(_brave.Size.X - _chargeGaugetxr.Height / 2, _brave.Size.Y / 2 + 0.5f),
                        null,
                        Color.White,
                        MathHelper.ToRadians(90f),
                        new Vector2(_chargeGaugetxr.Width / 2, _chargeGaugetxr.Height),
                        new Vector2(0.3f, _chargeGauge) / new Vector2(_chargeGaugetxr.Width, _chargeGaugetxr.Height),
                        SpriteEffects.None,
                        0f
                    );
            }

            // Demon Lord in-hand weapon
            _inHandWeaponBGSprite.Draw(
                spriteBatch,
                new Vector2(
                    DEMON_LORD_IN_HAND_WEAPON_POS_X,
                    DEMON_LORD_IN_HAND_WEAPON_POS_Y
                )
            );
            if (_demonLord.InHandWeapon != null)
            {
                spriteBatch.Draw(
                    _demonLord.InHandWeapon.Texture,
                    new Vector2(DEMON_LORD_IN_HAND_WEAPON_POS_X, DEMON_LORD_IN_HAND_WEAPON_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _demonLord.InHandWeapon.TextureOrigin,
                    _demonLord.InHandWeapon.Scale * 1.5f,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }

            // Demon Lord weapon bag 1
            _weaponBagBGSprite.Draw(
                spriteBatch,
                new Vector2(
                    DEMON_LORD_WEAPON_BAG1_POS_X,
                    DEMON_LORD_WEAPON_BAG1_POS_Y
                )
            );
            // Demon Lord weapon bag 2
            _weaponBagBGSprite.Draw(
                spriteBatch,
                new Vector2(
                    DEMON_LORD_WEAPON_BAG2_POS_X,
                    DEMON_LORD_WEAPON_BAG2_POS_Y
                )
            );
            if (_demonLord.WeaponsBag.Count == 1)
            {
                spriteBatch.Draw(
                    _demonLord.WeaponsBag[0].Texture,
                    new Vector2(DEMON_LORD_WEAPON_BAG1_POS_X, DEMON_LORD_WEAPON_BAG1_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _demonLord.WeaponsBag[0].TextureOrigin,
                    _demonLord.WeaponsBag[0].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }
            if (_demonLord.WeaponsBag.Count == 2)
            {
                spriteBatch.Draw(
                    _demonLord.WeaponsBag[0].Texture,
                    new Vector2(DEMON_LORD_WEAPON_BAG1_POS_X, DEMON_LORD_WEAPON_BAG1_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _demonLord.WeaponsBag[0].TextureOrigin,
                    _demonLord.WeaponsBag[0].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
                spriteBatch.Draw(
                    _demonLord.WeaponsBag[1].Texture,
                    new Vector2(DEMON_LORD_WEAPON_BAG2_POS_X, DEMON_LORD_WEAPON_BAG2_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _demonLord.WeaponsBag[1].TextureOrigin,
                    _demonLord.WeaponsBag[1].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }

            // Brave in-hand weapon
            _inHandWeaponBGSprite.Draw(
                spriteBatch,
                new Vector2(
                    BRAVE_IN_HAND_WEAPON_POS_X,
                    BRAVE_IN_HAND_WEAPON_POS_Y
                )
            );
            if (_brave.InHandWeapon != null)
            {
                spriteBatch.Draw(
                    _brave.InHandWeapon.Texture,
                    new Vector2(BRAVE_IN_HAND_WEAPON_POS_X, BRAVE_IN_HAND_WEAPON_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _brave.InHandWeapon.TextureOrigin,
                    _brave.InHandWeapon.Scale * 1.5f,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }

            // Brave weapon bag 1
            _weaponBagBGSprite.Draw(
                spriteBatch,
                new Vector2(
                    BRAVE_WEAPON_BAG1_POS_X,
                    BRAVE_WEAPON_BAG1_POS_Y
                )
            );
            // Brave weapon bag 2
            _weaponBagBGSprite.Draw(
                spriteBatch,
                new Vector2(
                    BRAVE_WEAPON_BAG2_POS_X,
                    BRAVE_WEAPON_BAG2_POS_Y
                )
            );
            if (_brave.WeaponsBag.Count == 1)
            {
                spriteBatch.Draw(
                    _brave.WeaponsBag[0].Texture,
                    new Vector2(BRAVE_WEAPON_BAG1_POS_X, BRAVE_WEAPON_BAG1_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _brave.WeaponsBag[0].TextureOrigin,
                    _brave.WeaponsBag[0].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }
            if (_brave.WeaponsBag.Count == 2)
            {
                spriteBatch.Draw(
                    _brave.WeaponsBag[0].Texture,
                    new Vector2(BRAVE_WEAPON_BAG1_POS_X, BRAVE_WEAPON_BAG1_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _brave.WeaponsBag[0].TextureOrigin,
                    _brave.WeaponsBag[0].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
                spriteBatch.Draw(
                    _brave.WeaponsBag[1].Texture,
                    new Vector2(BRAVE_WEAPON_BAG2_POS_X, BRAVE_WEAPON_BAG2_POS_Y),
                    null,
                    Color.White,
                    0f,
                    _brave.WeaponsBag[1].TextureOrigin,
                    _brave.WeaponsBag[1].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );

            }

            if (_flag is null)
            {
                _flag = new Sprite(_NoWind);
                _world.Gravity = new Vector2(0, -12f);
            }
            _flag.Draw(
                spriteBatch,
                new Vector2(15.2f, 8.65f)
            );
        }

        private Weapon RandomWeapon(Player player, Player enemy)
        {
            var values = Enum.GetValues(typeof(WeaponType));
            var weaponType = (WeaponType)values.GetValue(new Random().Next(values.Length));

            Weapon weapon;
            switch (weaponType)
            {
                case WeaponType.BIG:
                    weapon = new BigShot(
                        _world,
                        player,
                        enemy,
                        player.Side == PlayerSide.BRAVE ? _lightChakra : _cursedEye
                    );
                    break;
                case WeaponType.CHARM:
                    weapon = new CharmShot(_world, player, enemy, _mead);
                    break;
                default:
                    weapon = RandomWeapon(player, enemy);
                    break;
            }

            return weapon;
        }

        // Handle weapon selection
        private void WeaponSelectionHandler(
            Vector2 relativeMousePosition,
            Player player,
            Vector2 weaponBagOnePosition,
            Vector2 weaponBagTwoPosition
        )
        {
            if (!Globals.IsShooting)
            {
                if (relativeMousePosition.X >= weaponBagOnePosition.X - _weaponBagBGSprite.Width / 2
                        && relativeMousePosition.X <= weaponBagOnePosition.X + _weaponBagBGSprite.Width / 2
                        && relativeMousePosition.Y >= weaponBagOnePosition.Y - _weaponBagBGSprite.Height / 2
                        && relativeMousePosition.Y <= weaponBagOnePosition.Y + _weaponBagBGSprite.Height / 2
                        && player.WeaponsBag.Count > 0)
                {
                    Mouse.SetCursor(MouseCursor.Hand);

                    if (Globals.IsClicked())
                    {
                        player.InHandWeapon = player.WeaponsBag[0];
                        player.WeaponsBag.Remove(player.WeaponsBag[0]);
                    }
                }
                else if (relativeMousePosition.X >= weaponBagTwoPosition.X - _weaponBagBGSprite.Width / 2
                        && relativeMousePosition.X <= weaponBagTwoPosition.X + _weaponBagBGSprite.Width / 2
                        && relativeMousePosition.Y >= weaponBagTwoPosition.Y - _weaponBagBGSprite.Height / 2
                        && relativeMousePosition.Y <= weaponBagTwoPosition.Y + _weaponBagBGSprite.Height / 2
                        && player.WeaponsBag.Count > 1)
                {
                    Mouse.SetCursor(MouseCursor.Hand);

                    if (Globals.IsClicked())
                    {
                        player.InHandWeapon = player.WeaponsBag[1];
                        player.WeaponsBag.Remove(player.WeaponsBag[1]);
                    }
                }
                else Mouse.SetCursor(MouseCursor.Arrow);
            }
        }

        // Handle when in-hand weapon is null
        private void InHandWeaponHandler(Player player, Player enemy, Texture2D texture)
        {
            if (player.InHandWeapon is null)
                player.InHandWeapon = new NormalShot(
                    _world,
                    player,
                    enemy,
                    texture
                );
        }

        // Handle shooting force when holding mouse
        private void ShootingForceHandler()
        {
            if (Globals.IsHolding() && !Globals.IsShooting)
            {
                if (_chargeGauge > MAX_CHARGE)
                    _MeterControl = -1f;
                else if (_chargeGauge <= MIN_CHARGE)
                    _MeterControl = 1f;

                _chargeGauge += (0.05f * _MeterControl);
            }
        }

        // Handle shooting when mouse released
        private void ShootingHandler(Player player, float rotation, Vector2 weaponPosition)
        {
            if (Globals.IsReleased() && !Globals.IsShooting)
            {
                Globals.IsShooting = true;

                foreach (StatusEffect status in player.StatusEffectBag)
                {
                    switch (status.Type)
                    {
                        case StatusEffectType.ATTACK:
                            var tmp1 = player.InHandWeapon.Damage * 1.25f;
                            Console.WriteLine("Damage: " + tmp1);
                            player.InHandWeapon.Damage = (int)Math.Ceiling(tmp1);
                            Console.WriteLine("Inhand Damage: " + player.InHandWeapon.Damage);
                            break;
                        case StatusEffectType.SLIME_MUCILAGE:
                            var tmp2 = player.InHandWeapon.Damage * 0.8f;
                            Console.WriteLine("Damage: " + tmp2);
                            player.InHandWeapon.Damage = (int)Math.Ceiling(tmp2);
                            Console.WriteLine("Inhand Damage: " + player.InHandWeapon.Damage);
                            break;
                    }

                }

                float x = (float)Math.Cos(rotation);
                float y = (float)Math.Sin(rotation);

                player.InHandWeapon.CreateBody(weaponPosition);
                player.InHandWeapon.Body.ApplyLinearImpulse(
                    new Vector2(
                        x * (MIN_FORCE + _rangeForce * (_chargeGauge / 0.2f)),
                        y * (MIN_FORCE + _rangeForce * (_chargeGauge / 0.2f))
                    )
                );
                Console.WriteLine("Force: " + (MIN_FORCE + _rangeForce * (_chargeGauge / 0.2f)));

                _entityManager.AddEntry(player.InHandWeapon);

                _chargeGauge = 0;
            }
        }

        // Handle player blood thirst gauge
        private void BloodThirstGaugeHandler()
        {
            if (_demonLord.BloodThirstGauge == 2)
            {
                _demonLord.BloodThirstGauge = 0;

                var newWeapon = RandomWeapon(_demonLord, _brave);
                if (_demonLord.WeaponsBag.Count == 2)
                {
                    if (_brave.WeaponsBag.Count < 2)
                    {
                        newWeapon.Player = _brave;
                        newWeapon.Enemy = _demonLord;
                        newWeapon.Texture = _lightChakra;

                        _brave.WeaponsBag.Add(newWeapon);
                        Debug.WriteLine("Demon Lord's weapon bag is full. Brave got '" + newWeapon.Type + "' instead.");
                    }
                }
                else
                {
                    _demonLord.WeaponsBag.Add(newWeapon);
                    Debug.WriteLine("Demon Lord got: " + newWeapon.Type);
                }
            }

            if (_brave.BloodThirstGauge == 2)
            {
                _brave.BloodThirstGauge = 0;

                var newWeapon = RandomWeapon(_brave, _demonLord);
                if (_brave.WeaponsBag.Count == 2)
                {
                    if (_demonLord.WeaponsBag.Count < 2)
                    {
                        newWeapon.Player = _demonLord;
                        newWeapon.Enemy = _brave;
                        newWeapon.Texture = _cursedEye;

                        _demonLord.WeaponsBag.Add(newWeapon);
                        Debug.WriteLine("Brave's weapon bag is full. Demon Lord got '" + newWeapon.Type + "' instead.");
                    }
                }
                else
                {
                    _brave.WeaponsBag.Add(newWeapon);
                    Debug.WriteLine("Brave got: " + newWeapon.Type);
                }
            }
        }
    }
}
