using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Common;
using Zchlachten.Entities;
using Zchlachten.Graphics;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private BasicEffect _spriteEffect;

        private World _world;

        private SpriteFont _debugFont;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _groundTxr, _corpsesPileTxr;
        private Texture2D[] _weaponTxrs;
        private Sprite _backgroundSprite;

        private Ground _ground;
        private CorpsesPile _corpsesPile;

        private EntityManager _entityManager;
        private PlayerManager _playerManager;
        private WeaponManager _weaponManager;
        private StatusEffectManager _statusEffectManager;
        private ItemManager _itemManager;

        private DebugUI _debugUI;

        public PlayScreen(Zchlachten game) : base(game)
        {
            _spriteEffect = new BasicEffect(base.GraphicsDevice);
            _spriteEffect.TextureEnabled = true;
            _spriteEffect.VertexColorEnabled = true;

            Globals.Camera = new Camera2D(base.GraphicsDevice);
            Globals.Camera.Position.X = Globals.Camera.Width / 2;
            Globals.Camera.Position.Y = Globals.Camera.Height / 2;

            _world = new World(new Vector2(0, -9.8f));

            Globals.DebugView = new DebugView(_world);
            Globals.DebugView.LoadContent(base.GraphicsDevice, base.Content);
            Globals.DebugView.Enabled = true;

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
            _playerManager.LoadContent(base.Content);

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
            _statusEffectManager = new StatusEffectManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _statusEffectManager.LoadContent(base.Content);

            // Load environments
            _backgroundSprite = new Sprite(base.Content.Load<Texture2D>("Environments/BG"));
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);
            _corpsesPileTxr = base.Content.Load<Texture2D>("Environments/CorpsesPile");
            _corpsesPile = new CorpsesPile(_corpsesPileTxr, _world);

            // Load items
            _itemManager = new ItemManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _itemManager.LoadContent(base.Content);

            //Load debug UI
            _debugUI = new DebugUI(
                _world,
                _groundTxr,
                _debugFont,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _debugUI.LoadContent();

            _entityManager.AddEntry(_ground);
            _entityManager.AddEntry(_corpsesPile);


            _entityManager.AddEntry(_weaponManager);
            _entityManager.AddEntry(_statusEffectManager);
            _entityManager.AddEntry(_playerManager);
            _entityManager.AddEntry(_itemManager);
            
            _entityManager.AddEntry(_debugUI);
        }

        public override void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            Globals.Camera.Update();

            // Update entiites
            _entityManager.Update(gameTime);

            if (Globals.GameState == GameState.START)
                Globals.GameState = GameState.PRE_PLAY;

            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.G))
                Globals.DebugView.Enabled = !Globals.DebugView.Enabled;
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.DebugView.BeginCustomDraw(
                Globals.Camera.Projection,
                Globals.Camera.View,
                null,
                null,
                null,
                RasterizerState.CullNone,
                1f
            );

            _spriteEffect.View = Globals.Camera.View;
            _spriteEffect.Projection = Globals.Camera.Projection;

            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                null,
                null,
                null,
                RasterizerState.CullNone,
                _spriteEffect
            );

            // Draw background
            _backgroundSprite.Draw(
                _spriteBatch,
                new Vector2(Globals.Camera.Width / 2, Globals.Camera.Height / 2)
            );
            // Draw entities
            _entityManager.Draw(gameTime, _spriteBatch);

            // foreach (var b in _world.BodyList)
            // {
            //     foreach (var f in b.FixtureList)
            //         Globals.DebugView.DrawShape(f, new Transform(b.Position, b.Rotation), Color.Aqua);
            // }

            _spriteBatch.End();
            Globals.DebugView.EndCustomDraw();
        }
    }
}