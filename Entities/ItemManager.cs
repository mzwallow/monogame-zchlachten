using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class ItemManager : IGameEntity
    {
        private readonly World _world;
        private readonly EntityManager _entityManager;
        private Texture2D _worldTreeTxr, _shieldTxr, _holyWaterTxr;
        private const float WORLD_TREE_X = 428f;
        private const float WORLD_TREE_Y = 112f;
        private const float SHEILD_X = 476f;
        private const float SHEILD_Y = 112f;
        private const float HOLY_WATER_X = 524f;
        private const float HOLY_WATER_Y = 112f;

        Items _worldTree, _shield, _holyWater;

        public ItemManager(
            World world,
            EntityManager entityManager
        )
        {
            _world = world;
            _entityManager = entityManager;

        }
        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.START:
                    _worldTree = new WorldTree(
                    _world,
                    _worldTreeTxr,
                    new Vector2(WORLD_TREE_X, WORLD_TREE_Y)
                    );

                    _shield = new Shield(
                           _world,
                           _shieldTxr,
                           new Vector2(SHEILD_X, SHEILD_Y)
                    );

                    _worldTree = new WorldTree(
                           _world,
                           _worldTreeTxr,
                           new Vector2(WORLD_TREE_X, WORLD_TREE_Y)
                    );

                    _holyWater = new HolyWater(
                           _world,
                           _holyWaterTxr,
                           new Vector2(HOLY_WATER_X, HOLY_WATER_Y)
                    );


                    _entityManager.AddEntry(_worldTree);
                    _entityManager.AddEntry(_shield);
                    _entityManager.AddEntry(_holyWater);
                    break;
            }
        }

        public void LoadContent(ContentManager content)
        {
            _holyWaterTxr = content.Load<Texture2D>("Items/BG_Item");
            _shieldTxr = content.Load<Texture2D>("Controls/shield");
            _worldTreeTxr = content.Load<Texture2D>("Controls/eyeball");
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

    }
}