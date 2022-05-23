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
        private const float MIN_FORCE = 2f;
        private const float MAX_FORCE = 5.5f;
        private const float DEMON_LORD_WEAPON_START_POS_X = 5f;
        private const float DEMON_LORD_WEAPON_START_POS_Y = 5f;
        private const float BRAVE_WEAPON_START_POS_X = 25f;
        private const float BRAVE_WEAPON_START_POS_Y = 5f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private Texture2D _arrowTxr;
        private Texture2D _demonEyeTxr, _lightSwordTxr, _cursedEye, _lightChakra, _mead;
        private Texture2D _inHandWeaponTxr, _weaponBagTxr;

        private float _rotation;
        private bool _applyBuff = false;


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
        }

        public void LoadContent(ContentManager content)
        {
            _arrowTxr = content.Load<Texture2D>("Players/Arrow");
            _inHandWeaponTxr = content.Load<Texture2D>("UI/InHandWeapon");
            _weaponBagTxr = content.Load<Texture2D>("UI/WeaponBag");
        }

        public void Update(GameTime gameTime)
        {
            Vector2 relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);

            switch (Globals.GameState)
            {
                case GameState.PLAYING:
                    Console.WriteLine("Apply:" + _applyBuff);
                    Vector2 weaponStartingPos;
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        if (_demonLord.InHandWeapon is null)
                            _demonLord.InHandWeapon = new NormalShot(_world, _demonLord, _brave, _demonEyeTxr);

                        // Check shootable angle
                        if (relativeMousePosition.X >= _demonLord.Body.Position.X + _demonLord.Size.X / 2
                                && relativeMousePosition.Y >= _demonLord.Body.Position.Y + _demonLord.Size.Y / 2)
                        {
                            _rotation = (float)Math.Atan2(
                                relativeMousePosition.Y - (_demonLord.Body.Position.Y + _demonLord.Size.Y / 2),
                                relativeMousePosition.X - (_demonLord.Body.Position.X + _demonLord.Size.X / 2)
                            );

                            // Handle shooting
                            if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                    && Globals.PreviousMouseState.LeftButton == ButtonState.Released
                                    && !Globals.IsShooting)
                            {
                                float x = (float)Math.Cos(_rotation);
                                float y = (float)Math.Sin(_rotation);

                                weaponStartingPos = new Vector2(
                                    _demonLord.Body.Position.X + _demonLord.Size.X / 2 + 0.5f,
                                    _demonLord.Body.Position.Y + _demonLord.Size.Y / 2 + 0.5f
                                );

                                _demonLord.InHandWeapon.CreateBody(weaponStartingPos);
                                _demonLord.InHandWeapon.Body.ApplyLinearImpulse(new Vector2(x * MAX_FORCE, y * MAX_FORCE));

                                _entityManager.AddEntry(_demonLord.InHandWeapon);
                                Globals.IsShooting = true;
                            }
                        }

                        // Handle weapon selection
                        if (!Globals.IsShooting)
                        {
                            var weaponBagOnePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(99f, 686f));
                            var weaponBagTwoPosition = Globals.Camera.ConvertScreenToWorld(new Vector2(138f, 686f));
                            if (relativeMousePosition.X >= weaponBagOnePosition.X - _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= weaponBagOnePosition.X + _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= weaponBagOnePosition.Y - _weaponBagTxr.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= weaponBagOnePosition.Y + _weaponBagTxr.Height * 0.0234375f / 2
                                    && _demonLord.WeaponsBag.Count > 0)
                            {
                                Debug.WriteLine("Weapon bag 1");
                                Mouse.SetCursor(MouseCursor.Hand);

                                if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                        && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                                {
                                    _demonLord.InHandWeapon = _demonLord.WeaponsBag[0];
                                    _demonLord.WeaponsBag.Remove(_demonLord.WeaponsBag[0]);
                                }
                            }
                            else if (relativeMousePosition.X >= weaponBagTwoPosition.X - _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= weaponBagTwoPosition.X + _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= weaponBagTwoPosition.Y - _weaponBagTxr.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= weaponBagTwoPosition.Y + _weaponBagTxr.Height * 0.0234375f / 2
                                    && _demonLord.WeaponsBag.Count > 1)
                            {
                                Debug.WriteLine("Weapon bag 2");
                                Mouse.SetCursor(MouseCursor.Hand);

                                if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                        && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                                {
                                    _demonLord.InHandWeapon = _demonLord.WeaponsBag[1];
                                    _demonLord.WeaponsBag.Remove(_demonLord.WeaponsBag[1]);
                                }
                            }
                            else
                                Mouse.SetCursor(MouseCursor.Arrow);
                        }
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        if (_brave.InHandWeapon is null)
                            _brave.InHandWeapon = new NormalShot(_world, _brave, _demonLord, _lightSwordTxr);

                        // Check shootable angle
                        if (relativeMousePosition.X <= _brave.Body.Position.X - _brave.Size.X / 2
                                && relativeMousePosition.Y >= _brave.Body.Position.Y + _brave.Size.Y / 2)
                        {
                            _rotation = (float)Math.Atan2(
                                relativeMousePosition.Y - (_brave.Body.Position.Y + _brave.Size.Y / 2),
                                relativeMousePosition.X - (_brave.Body.Position.X - _brave.Size.X / 2)
                            );

                            // Handle shooting
                            if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                    && Globals.PreviousMouseState.LeftButton == ButtonState.Released
                                    && !Globals.IsShooting)
                            {
                                float x = (float)Math.Cos(_rotation);
                                float y = (float)Math.Sin(_rotation);

                                weaponStartingPos = new Vector2(
                                    _brave.Body.Position.X - _brave.Size.X / 2 - 0.5f,
                                    _brave.Body.Position.Y + _brave.Size.Y / 2 + 0.5f
                                );

                                _brave.InHandWeapon.CreateBody(weaponStartingPos);
                                _brave.InHandWeapon.Body.ApplyLinearImpulse(new Vector2(x * MAX_FORCE, y * MAX_FORCE));

                                _entityManager.AddEntry(_brave.InHandWeapon);
                                Globals.IsShooting = true;
                            }
                        }

                        // Handle weapon selection
                        if (!Globals.IsShooting)
                        {
                            var weaponBagOnePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(1179f, 686f));
                            var weaponBagTwoPosition = Globals.Camera.ConvertScreenToWorld(new Vector2(1140f, 686f));
                            if (relativeMousePosition.X >= weaponBagOnePosition.X - _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= weaponBagOnePosition.X + _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= weaponBagOnePosition.Y - _weaponBagTxr.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= weaponBagOnePosition.Y + _weaponBagTxr.Height * 0.0234375f / 2
                                    && _brave.WeaponsBag.Count > 0)
                            {
                                Debug.WriteLine("Weapon bag 1");
                                Mouse.SetCursor(MouseCursor.Hand);

                                if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                        && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                                {
                                    _brave.InHandWeapon = _brave.WeaponsBag[0];
                                    _brave.WeaponsBag.Remove(_brave.WeaponsBag[0]);
                                }
                            }
                            else if (relativeMousePosition.X >= weaponBagTwoPosition.X - _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= weaponBagTwoPosition.X + _weaponBagTxr.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= weaponBagTwoPosition.Y - _weaponBagTxr.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= weaponBagTwoPosition.Y + _weaponBagTxr.Height * 0.0234375f / 2
                                    && _brave.WeaponsBag.Count > 1)
                            {
                                Debug.WriteLine("Weapon bag 2");
                                Mouse.SetCursor(MouseCursor.Hand);

                                if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                        && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                                {
                                    _brave.InHandWeapon = _brave.WeaponsBag[1];
                                    _brave.WeaponsBag.Remove(_brave.WeaponsBag[1]);
                                }
                            }
                            else
                                Mouse.SetCursor(MouseCursor.Arrow);
                        }
                    }

                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD && !_applyBuff)
                    {
                        foreach (StatusEffect status in _demonLord.StatusEffectBag)
                        {
                            switch (status.Type)
                            {
                                case StatusEffectType.ATTACK:
                                    var tmp = _demonLord.InHandWeapon.Damage * 1.25f;
                                    Console.WriteLine("Inhand Damage: " + _demonLord.InHandWeapon.Damage);
                                    Console.WriteLine("Damage: " + tmp);
                                    _demonLord.InHandWeapon.Damage = (int)Math.Ceiling(tmp);
                                    break;
                                case StatusEffectType.SLIME_MUCILAGE:
                                    var tmp1 = _demonLord.InHandWeapon.Damage * 0.8f;
                                    Console.WriteLine("Inhand Damage: " + _demonLord.InHandWeapon.Damage);
                                    Console.WriteLine("Damage: " + tmp1);
                                    _demonLord.InHandWeapon.Damage = (int)Math.Ceiling(tmp1);
                                    break;
                            }
                            _applyBuff = true;
                        }

                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE && !_applyBuff)
                    {
                        foreach (StatusEffect status in _brave.StatusEffectBag)
                        {
                            switch (status.Type)
                            {
                                case StatusEffectType.ATTACK:
                                    var tmp = _brave.InHandWeapon.Damage * 1.25f;
                                    Console.WriteLine("Inhand Damage: " + _brave.InHandWeapon.Damage);
                                    Console.WriteLine("Damage: " + tmp);
                                    _brave.InHandWeapon.Damage = (int)Math.Ceiling(tmp);
                                    break;
                                case StatusEffectType.SLIME_MUCILAGE:
                                    var tmp1 = _brave.InHandWeapon.Damage * 0.8f;
                                    Console.WriteLine("Inhand Damage: " + _brave.InHandWeapon.Damage);
                                    Console.WriteLine("Damage: " + tmp1);
                                    _brave.InHandWeapon.Damage = (int)Math.Ceiling(tmp1);
                                    break;
                            }
                            _applyBuff = true;
                        }

                    }

                    break;
                case GameState.POST_PLAY:
                    if (_demonLord.BloodThirstGauge == 2)
                    {
                        _demonLord.BloodThirstGauge = 0;

                        var newWeapon = RandomWeapon(_demonLord, _brave);
                        if (_demonLord.WeaponsBag.Count == 2)
                        {
                            if (_brave.WeaponsBag.Count < 2)
                            {
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
                    else if (_brave.BloodThirstGauge == 2)
                    {
                        _brave.BloodThirstGauge = 0;

                        var newWeapon = RandomWeapon(_brave, _demonLord);
                        if (_brave.WeaponsBag.Count == 2)
                        {
                            if (_demonLord.WeaponsBag.Count < 2)
                            {
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

                    Globals.GameState = GameState.PRE_PLAY;
                    _applyBuff = false;


                    break;
            }

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
                    {
                        _demonLord.InHandWeapon = null;
                        Globals.PlayerTurn = PlayerTurn.BRAVE;
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        _brave.InHandWeapon = null;
                        Globals.PlayerTurn = PlayerTurn.DEMON_LORD;
                    }




                    // if (_demonLord.StatusEffectBag.Count > 0)
                    // {
                    //     for (int i = _demonLord.StatusEffectBag.Count - 1; i > -1; --i)
                    //     {
                    //         var x = _demonLord.StatusEffectBag[i];
                    //         if (x.Remaining == 0)
                    //         {
                    //             _demonLord.StatusEffectBag.RemoveAt(i);
                    //         }
                    //     }
                    // }
                    Globals.GameState = GameState.POST_PLAY;
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
            }

            // Demon Lord in-hand weapon
            spriteBatch.Draw(
                _inHandWeaponTxr,
                Globals.Camera.ConvertScreenToWorld(new Vector2(40.5f, 682.5f)),
                null,
                Color.White,
                0f,
                new Vector2(_inHandWeaponTxr.Width / 2, _inHandWeaponTxr.Height / 2),
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            if (_demonLord.InHandWeapon != null)
            {
                spriteBatch.Draw(
                    _demonLord.InHandWeapon.Texture,
                    Globals.Camera.ConvertScreenToWorld(new Vector2(40.5f, 682.5f)),
                    null,
                    Color.White,
                    0f,
                    _demonLord.InHandWeapon.TextureOrigin,
                    _demonLord.InHandWeapon.Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }

            // Demon Lord weapon bag 1
            spriteBatch.Draw(
                _weaponBagTxr,
                Globals.Camera.ConvertScreenToWorld(new Vector2(99f, 686f)),
                null,
                Color.White,
                0f,
                new Vector2(_weaponBagTxr.Width / 2, _weaponBagTxr.Height / 2),
                0.0234375f,
                SpriteEffects.None,
                0f
            );
            // Demon Lord weapon bag 2
            spriteBatch.Draw(
                _weaponBagTxr,
                Globals.Camera.ConvertScreenToWorld(new Vector2(138f, 686f)),
                null,
                Color.White,
                0f,
                new Vector2(_weaponBagTxr.Width / 2, _weaponBagTxr.Height / 2),
                0.0234375f,
                SpriteEffects.None,
                0f
            );
            if (_demonLord.WeaponsBag.Count == 1)
            {
                spriteBatch.Draw(
                    _demonLord.WeaponsBag[0].Texture,
                    Globals.Camera.ConvertScreenToWorld(new Vector2(99f, 686f)),
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
                    Globals.Camera.ConvertScreenToWorld(new Vector2(99f, 686f)),
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
                    Globals.Camera.ConvertScreenToWorld(new Vector2(138f, 686f)),
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
            spriteBatch.Draw(
                _inHandWeaponTxr,
                Globals.Camera.ConvertScreenToWorld(new Vector2(1241.5f, 682.5f)),
                null,
                Color.White,
                0f,
                new Vector2(_inHandWeaponTxr.Width / 2, _inHandWeaponTxr.Height / 2),
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            if (_brave.InHandWeapon != null)
            {
                spriteBatch.Draw(
                    _brave.InHandWeapon.Texture,
                    Globals.Camera.ConvertScreenToWorld(new Vector2(1241.5f, 682.5f)),
                    null,
                    Color.White,
                    0f,
                    _brave.InHandWeapon.TextureOrigin,
                    _brave.InHandWeapon.Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }

            // Brave weapon bag 1
            spriteBatch.Draw(
                _weaponBagTxr,
                Globals.Camera.ConvertScreenToWorld(new Vector2(1179f, 686f)),
                null,
                Color.White,
                0f,
                new Vector2(_weaponBagTxr.Width / 2, _weaponBagTxr.Height / 2),
                0.0234375f,
                SpriteEffects.None,
                0f
            );
            // Brave weapon bag 2
            spriteBatch.Draw(
                _weaponBagTxr,
                Globals.Camera.ConvertScreenToWorld(new Vector2(1140f, 686f)),
                null,
                Color.White,
                0f,
                new Vector2(_weaponBagTxr.Width / 2, _weaponBagTxr.Height / 2),
                0.0234375f,
                SpriteEffects.None,
                0f
            );
            if (_brave.WeaponsBag.Count == 1)
            {
                spriteBatch.Draw(
                    _brave.WeaponsBag[0].Texture,
                    Globals.Camera.ConvertScreenToWorld(new Vector2(1179f, 686f)),
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
                    Globals.Camera.ConvertScreenToWorld(new Vector2(1179f, 686f)),
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
                    Globals.Camera.ConvertScreenToWorld(new Vector2(1140f, 686f)),
                    null,
                    Color.White,
                    0f,
                    _brave.WeaponsBag[1].TextureOrigin,
                    _brave.WeaponsBag[1].Scale,
                    SpriteEffects.FlipVertically,
                    0f
                );
            }
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
                        player.PlayerSide == PlayerSide.BRAVE ? _lightChakra : _cursedEye
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
    }
}