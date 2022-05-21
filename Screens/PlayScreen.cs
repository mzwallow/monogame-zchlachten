using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Diagnostics;
using Zchlachten.Entities;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private Camera2D _camera;
        private BasicEffect _spriteEffect;

        private World _world;

        private SpriteFont _debugFont;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _buffGod, _buffDevil, _debuffDragon, _debuffGolden, _debuffSlime;
        private Texture2D _groundTxr;
        private Texture2D[] _weaponTxrs, _allStatusEffectTxr;

        private Ground _ground;

        private EntityManager _entityManager;
        private PlayerManager _playerManager;
        private WeaponManager _weaponManager;
        private StatusEffectManager _statusEffectManager;

        private DebugUI _debugUI;

        public PlayScreen(Zchlachten game) : base(game)
        {
            _spriteEffect = new BasicEffect(base.Game.Graphics.GraphicsDevice);
            _spriteEffect.TextureEnabled = true;
            _camera = new Camera2D(base.Game.Graphics.GraphicsDevice);
            _camera.Position.X = _camera.Width / 2;
            _camera.Position.Y = _camera.Height / 2;

            Globals.Camera = _camera;

            _world = new World(new Vector2(0, -9.8f));

            _entityManager = new EntityManager();

            Globals.GameState = GameState.START;
            var values = Enum.GetValues(typeof(PlayerTurn));
            Globals.PlayerTurn = (PlayerTurn)values.GetValue(new Random().Next(values.Length));

            Globals.DebugView = new DebugView(_world);
            Globals.DebugView.LoadContent(base.Game.Graphics.GraphicsDevice, base.Content);
            Globals.DebugView.Enabled = true;
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
            var demonEyeTxr = base.Content.Load<Texture2D>("Weapons/DemonEye");
            var cursedEyeTxr = base.Content.Load<Texture2D>("Weapons/CursedEye");
            var lightSwordTxr = base.Content.Load<Texture2D>("Weapons/LightSword");
            var lightChakraTxr = base.Content.Load<Texture2D>("Weapons/LightChakra");
            var meadTxr = base.Content.Load<Texture2D>("Weapons/Mead");
            _weaponTxrs = new Texture2D[]
            {
                demonEyeTxr, lightSwordTxr, cursedEyeTxr, lightChakraTxr, meadTxr
            };

            _weaponManager = new WeaponManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave,
                _weaponTxrs
            );
            _weaponManager.LoadContent(base.Content);

            // Load buffs & debuffs
            _buffGod = base.Content.Load<Texture2D>("Controls/blessing_of_god");
            _buffDevil = base.Content.Load<Texture2D>("Controls/blessing_of_devil");
            _debuffDragon = base.Content.Load<Texture2D>("Controls/fire_dragon_blood");
            _debuffGolden = base.Content.Load<Texture2D>("Controls/golden_crow_bile");
            _debuffSlime = base.Content.Load<Texture2D>("StatusEffects/Buff");
            _allStatusEffectTxr = new Texture2D[]{
                _buffGod,
                _buffDevil,
                _debuffDragon,
                _debuffGolden,
                _debuffSlime
            };
            _statusEffectManager = new StatusEffectManager(
                _world,
                _entityManager,
                _allStatusEffectTxr
            );

            // Load environments
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);

            //Load debug UI
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
            _entityManager.AddEntry(_statusEffectManager);

            _entityManager.AddEntry(_debugUI);
        }

        public override void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            _camera.Update();
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

            _spriteEffect.View = _camera.View;
            _spriteEffect.Projection = _camera.Projection;

            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                null,
                null,
                null,
                RasterizerState.CullNone,
                _spriteEffect
            );
            Globals.DebugView.BeginCustomDraw(
                Globals.Camera.Projection, 
                Globals.Camera.View,
                null,
                null,
                null,
                RasterizerState.CullNone,
                1f
            );

            _entityManager.Draw(gameTime, _spriteBatch);

            Globals.DebugView.EndCustomDraw();
            _spriteBatch.End();
        }
    }
}