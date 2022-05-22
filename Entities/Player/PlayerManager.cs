using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class PlayerManager : IGameEntity
    {
        private const float DEMON_LORD_POS_X = 3.50390625f;
        private const float DEMON_LORD_POS_Y = 3.609375f;
        private const float BRAVE_POS_X = 26.49609375f;
        private const float BRAVE_POS_Y = 3.609375f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        public readonly Player DemonLord, Brave;

        private Texture2D _hpBarTxr;

        public PlayerManager(
            World world, 
            EntityManager entityManager,
            Texture2D demonLordTxr, 
            Texture2D braveTxr)
        {
            _world = world;
            _entityManager = entityManager;

            DemonLord = new DemonLord(
                    _world,
                    demonLordTxr,
                    new Vector2(DEMON_LORD_POS_X, DEMON_LORD_POS_Y)
                );
            Brave = new Brave(
                    _world,
                    braveTxr,
                    new Vector2(BRAVE_POS_X, BRAVE_POS_Y)
                );

            _entityManager.AddEntry(DemonLord);
            _entityManager.AddEntry(Brave);
        }

        public void LoadContent(ContentManager content)
        {
            _hpBarTxr = content.Load<Texture2D>("UI/HPBar");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _hpBarTxr,
                new Vector2(Globals.Camera.Width/2, 1.5f),
                null,
                Color.White,
                0f,
                new Vector2(_hpBarTxr.Width/2, _hpBarTxr.Height/2),
                Globals.Camera.Scale,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}