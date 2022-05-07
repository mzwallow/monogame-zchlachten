using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zchlachten.Entities;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        private SpriteBatch _spriteBatch;

        private Texture2D _demonLordTxr, _braveTxr;
        private Texture2D _demonEyeTxr;
        private Texture2D _groundTxr;

        private Ground _ground;

        private EntityManager _entityManager;

        private PlayerManager _playerManager;

        public PlayScreen(Zchlachten game) : base(game) 
        { 
            _entityManager = new EntityManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(base.GraphicsDevice);

            // Load players
            _demonLordTxr = base.Content.Load<Texture2D>("Players/DemonLord");
            _braveTxr = base.Content.Load<Texture2D>("Players/Brave");
            _playerManager = new PlayerManager(_entityManager, _demonLordTxr, _braveTxr);

            // Load weapons
            _demonEyeTxr = base.Content.Load<Texture2D>("Weapons/DemonEye");

            // Load environments
            _groundTxr = base.Content.Load<Texture2D>("Environments/Ground");
            _ground = new Ground(_groundTxr);

            _entityManager.AddEntry(_ground);
        }

        public override void Update(GameTime gameTime)
        {
            _entityManager.Update(gameTime);
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