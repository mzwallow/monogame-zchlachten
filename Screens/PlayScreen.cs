using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Entities;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private World _world;
        private PlayState _state;
        private PlayerTurn _playerTurn;

        private SpriteFont _debugFont;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _demonEyeTxr;
        private Texture2D _groundTxr;
        private Texture2D[] _weaponTxrs;

        private Ground _ground;

        private EntityManager _entityManager;
        private PlayerManager _playerManager;
        private WeaponManager _weaponManager;

        private DebugUI _debugUI;

        public PlayScreen(Zchlachten game) : base(game)
        {
            _world = new World(new Vector2(0, 100f));

            _entityManager = new EntityManager();

            _state = PlayState.START;
            if (_state == PlayState.START)
            {
                var values = Enum.GetValues(typeof(PlayerTurn));
                _playerTurn = (PlayerTurn)values.GetValue(new Random().Next(values.Length));
            } 
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
                _state,
                _demonLordTxr, 
                _braveTxr
            );

            // Load weapons
            _demonEyeTxr = base.Content.Load<Texture2D>("Weapons/DemonEye");
            _weaponTxrs = new Texture2D[]
            {
                _demonEyeTxr
            };

            _weaponManager = new WeaponManager(
                _world,
                _entityManager,
                _state,
                _playerTurn,
                _playerManager.DemonLord,
                _playerManager.Brave,
                _weaponTxrs
            );

            // Load environments
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);

            // Load debug UI
            _debugUI = new DebugUI(
                _groundTxr, 
                _debugFont,
                _state,
                _playerTurn,
                _playerManager.DemonLord, 
                _playerManager.Brave
            );

            _entityManager.AddEntry(_ground);
            _entityManager.AddEntry(_playerManager);
            _entityManager.AddEntry(_weaponManager);
            
            _entityManager.AddEntry(_debugUI);
        }

        public override void Update(GameTime gameTime)
        {
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            _entityManager.Update(gameTime);

            if (_state == PlayState.START)
                _state = PlayState.PRE_PLAY;
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