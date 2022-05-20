using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Entities;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private World _world;

        private SpriteFont _debugFont;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _demonEyeTxr;
        private Texture2D _buffTxr;
        private Texture2D _groundTxr;
        private Texture2D[] _weaponTxrs;

        private Ground _ground;

        private EntityManager _entityManager;
        private PlayerManager _playerManager;
        private WeaponManager _weaponManager;
        private StatusEffectManager _statusEffectManager;

        private DebugUI _debugUI;

        public PlayScreen(Zchlachten game) : base(game)
        {
            _world = new World(new Vector2(0, 100f));

            _entityManager = new EntityManager();

            Globals.GameState = GameState.START;
            var values = Enum.GetValues(typeof(PlayerTurn));
            Globals.PlayerTurn = (PlayerTurn)values.GetValue(new Random().Next(values.Length));
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // Load fonts
            _debugFont = base.Content.Load<SpriteFont>("Fonts/Arial");

            // Load players
            _demonLordTxr = base.Content.Load<Texture2D>("Players/DemonLord");
            _braveTxr = base.Content.Load<Texture2D>("Players/Brave");
            _playerManager = new PlayerManager(
                _world,
                _entityManager,
                _demonLordTxr,
                _braveTxr
            );

            // Load weapons
            _demonEyeTxr = base.Content.Load<Texture2D>("Weapons/DemonEye");
            var _lightSwordTxr = base.Content.Load<Texture2D>("Weapons/LightSword");
            _weaponTxrs = new Texture2D[]
            {
                _demonEyeTxr, _lightSwordTxr
            };

            _weaponManager = new WeaponManager(
                base.Content,
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave,
                _weaponTxrs
            );
            _weaponManager.LoadContent();

            // Load buffs & debuffs
            _buffTxr = base.Content.Load<Texture2D>("StatusEffects/Buff");
            _statusEffectManager = new StatusEffectManager(
                _world,
                _entityManager
            );

            // Load environments
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);

            // Load debug UI
            _debugUI = new DebugUI(
                base.Content,
                _groundTxr,
                _debugFont,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _debugUI.LoadContent();

            _entityManager.AddEntry(_ground);
            _entityManager.AddEntry(_playerManager);
            _entityManager.AddEntry(_weaponManager);

            _entityManager.AddEntry(_debugUI);
        }

        public override void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            _entityManager.Update(gameTime);

            if (Globals.GameState == GameState.START)
                Globals.GameState = GameState.PRE_PLAY;

            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.Z))
                Globals.GameState = GameState.PRE_PLAY;
            else if (Globals.CurrentKeyboardState.IsKeyDown(Keys.X))
                Globals.GameState = GameState.PLAYING;
            else if (Globals.CurrentKeyboardState.IsKeyDown(Keys.C))
                Globals.GameState = GameState.POST_PLAY;
            else if (Globals.CurrentKeyboardState.IsKeyDown(Keys.V))
                Globals.GameState = GameState.END;
        }

        public override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.DarkOliveGreen);

            _spriteBatch.Begin();

            _entityManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }
    }
}