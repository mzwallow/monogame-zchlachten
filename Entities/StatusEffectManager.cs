using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using System;


namespace Zchlachten.Entities
{
    public class StatusEffectManager : IGameEntity
    {
        static Random r = new Random();
        private float positionX, positionY, positionX1, positionY1;
        private readonly World _world;
        private readonly EntityManager _entityManager;

        private Texture2D _buffGod, _buffDevil, _debuffDragon, _debuffGolden, _debuffSlime;


        public StatusEffect Buff, Buff1;


        public StatusEffectManager(
            World world,
            EntityManager entityManager
        )
        {

            _world = world;
            _entityManager = entityManager;

        }

        public void LoadContent(ContentManager content)
        {
            _buffGod = content.Load<Texture2D>("Controls/blessing_of_god");
            _buffDevil = content.Load<Texture2D>("Controls/blessing_of_devil");
            _debuffDragon = content.Load<Texture2D>("Controls/fire_dragon_blood");
            _debuffGolden = content.Load<Texture2D>("Controls/golden_crow_bile");
            _debuffSlime = content.Load<Texture2D>("Controls/Slime");
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.PRE_PLAY:
                    //Random Bullshit
                    positionX = r.Next(Convert.ToInt32(450 * 0.0234375f), Convert.ToInt32(800 * 0.0234375f));
                    positionY = r.Next(Convert.ToInt32(370 * 0.0234375f), Convert.ToInt32(620 * 0.0234375f));
                    positionX1 = r.Next(Convert.ToInt32(450 * 0.0234375f), Convert.ToInt32(800 * 0.0234375f));
                    positionY1 = r.Next(Convert.ToInt32(370 * 0.0234375f), Convert.ToInt32(620 * 0.0234375f));

                    //Random Buff
                    var newBuff = RandomBuff(new Vector2(positionX, positionY));
                    var newBuff1 = RandomBuff(new Vector2(positionX1, positionY1));

                    _entityManager.AddEntry(newBuff);
                    _entityManager.AddEntry(newBuff1);
                    Globals.GameState = GameState.PLAYING;
                    break;
                case GameState.POST_PLAY:
                    foreach (StatusEffect buff in _entityManager.GetEntitiesOfType<StatusEffect>())
                    {
                        _world.Remove(buff.Body);
                        _entityManager.RemoveEntity(buff);
                    }
                    break;
            }

            foreach (StatusEffect status in _entityManager.GetEntitiesOfType<StatusEffect>())
            {
                if (status.HasCollided)
                {
                    _world.Remove(status.Body);
                    _entityManager.RemoveEntity(status);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }



        private StatusEffect RandomBuff(Vector2 position)
        {
            var values = Enum.GetValues(typeof(StatusEffectType));
            var statusType = (StatusEffectType)values.GetValue(new Random().Next(values.Length));

            StatusEffect status;
            switch (statusType)
            {
                case StatusEffectType.GOD_BlESSING:
                    status = new BuffGod(_world, _buffGod, position);
                    break;
                case StatusEffectType.DEVIL_SIN:
                    status = new BuffDevil(_world, _buffDevil, position);
                    break;
                case StatusEffectType.FIRE_DRAGON_BLOOD:
                    status = new DebuffDragon(_world, _debuffDragon, position);
                    break;
                case StatusEffectType.GOLDEN_SERPANT_BILE:
                    status = new DebuffGolden(_world, _debuffGolden, position);
                    break;
                case StatusEffectType.SLIME_MUCILAGE:
                    status = new DebuffSlime(_world, _debuffSlime, position);
                    break;
                default:
                    status = RandomBuff(position);
                    break;
            }

            return status;
        }
    }
}