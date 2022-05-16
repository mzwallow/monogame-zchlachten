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

        public GameState GameState;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _demonEyeTxr;
        private Texture2D _groundTxr;
        private Texture2D[] _weaponTxrs;

        private Ground _ground;

        private EntityManager _entityManager;

        private PlayerManager _playerManager;

        private WeaponManager _weaponManager;

        public PlayScreen(Zchlachten game) : base(game)
        {
            _world = new World(new Vector2(0, 100f));

            _entityManager = new EntityManager();

            GameState = GameState.START;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // Load players
            _demonLordTxr = base.Content.Load<Texture2D>("Players/DemonLord");
            _braveTxr = base.Content.Load<Texture2D>("Players/Brave");
            _playerManager = new PlayerManager(_world, _entityManager, _demonLordTxr, _braveTxr);

            // Load weapons
            _demonEyeTxr = base.Content.Load<Texture2D>("Weapons/DemonEye");
            _weaponTxrs = new Texture2D[]
            {
                _demonEyeTxr
            };

            _weaponManager = new WeaponManager(
                _world,
                _entityManager,
                _playerManager.DemonLord,
                _playerManager.DemonLord,
                _weaponTxrs
            );

            // Load environments
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr, _world);

            _entityManager.AddEntry(_ground);
        }

        public override void Update(GameTime gameTime)
        {
            _entityManager.Update(gameTime);

            _world.Step(gameTime.ElapsedGameTime);
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