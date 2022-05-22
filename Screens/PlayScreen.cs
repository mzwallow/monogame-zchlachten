using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Common;
using Zchlachten.Entities;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private BasicEffect _spriteEffect;

        private World _world;

        private SpriteFont _debugFont;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _buffGod, _buffDevil, _debuffDragon, _debuffGolden, _debuffSlime;
        private Texture2D _groundTxr, _backgroundTxr, _corpsesPileTxr;
        private Texture2D[] _weaponTxrs, _allStatusEffectTxr;

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
            _backgroundTxr = base.Content.Load<Texture2D>("Environments/BG");
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);
            _corpsesPileTxr = base.Content.Load<Texture2D>("Environments/CorpsesPile");
            _corpsesPile = new CorpsesPile(_corpsesPileTxr, _world);

            // Load Items
            _itemManager = new ItemManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.Brave
            );
            _itemManager.LoadContent(base.Content);



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
            _entityManager.AddEntry(_corpsesPile);
            _entityManager.AddEntry(_playerManager);
            _entityManager.AddEntry(_weaponManager);
            _entityManager.AddEntry(_statusEffectManager);
            _entityManager.AddEntry(_debugUI);
            _entityManager.AddEntry(_itemManager);

        }

        public override void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            Globals.Camera.Update();
            _entityManager.Update(gameTime);

            if (Globals.GameState == GameState.START)
                Globals.GameState = GameState.PRE_PLAY;

            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.Z))
                Globals.GameState = GameState.PRE_PLAY;
            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.X))
                Globals.GameState = GameState.PLAYING;
            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.C))
                Globals.GameState = GameState.POST_PLAY;
            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.V))
                Globals.GameState = GameState.END;
            if (Globals.CurrentKeyboardState.IsKeyDown(Keys.G))
                Globals.DebugView.Enabled = !Globals.DebugView.Enabled;
        }

        public override void Draw(GameTime gameTime)
        {
            // base.GraphicsDevice.Clear(Color.DarkOliveGreen);
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

            _spriteBatch.Draw(
                _backgroundTxr, 
                new Vector2(Globals.Camera.Width/2, Globals.Camera.Height/2), 
                null,
                Color.White,
                0f,
                new Vector2(_backgroundTxr.Width/2, _backgroundTxr.Height/2),
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            _entityManager.Draw(gameTime, _spriteBatch);

            foreach (var b in _world.BodyList)
            {
                foreach (var f in b.FixtureList)
                {
                    Globals.DebugView.DrawShape(f, new Transform(b.Position, b.Rotation), Color.Aqua);
                    // Debug.WriteLine(f.Tag);
                }
            }

            _spriteBatch.End();
            Globals.DebugView.EndCustomDraw();
        }
    }
}