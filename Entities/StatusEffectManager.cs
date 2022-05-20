using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private Texture2D _buffGod, _buffDevil, _debuffDragon, _debuffGolden, _debuffSlime, _test, _test1;

        //private Texture2D buffTxr;

        public StatusEffect Buff, Buff1;


        public StatusEffectManager(
            World world,
            EntityManager entityManager,
            params Texture2D[] statusEffectTxrs
        )
        {
            _buffGod = statusEffectTxrs[0];
            _buffDevil = statusEffectTxrs[1];
            _debuffDragon = statusEffectTxrs[2];
            _debuffGolden = statusEffectTxrs[3];
            _debuffSlime = statusEffectTxrs[4];
            //Random bullshit go!!
            var values = Enum.GetValues(typeof(StatusEffectType));
            StatusEffectType Type1 = (StatusEffectType)values.GetValue(new Random().Next(values.Length));
            StatusEffectType Type2 = (StatusEffectType)values.GetValue(new Random().Next(values.Length));
            //Select Buff from random
            switch (Type1)
            {
                case StatusEffectType.GOD_BlESSING:
                    _test = _buffGod;
                    break;
                case StatusEffectType.DEVIL_SIN:
                    _test = _buffDevil;
                    break;
                case StatusEffectType.FIRE_DRAGON_BLOOD:
                    _test = _debuffDragon;
                    break;
                case StatusEffectType.GOLDEN_SERPANT_BILE:
                    _test = _debuffGolden;
                    break;
                case StatusEffectType.SLIME_MUCILAGE:
                    _test = _debuffSlime;
                    break;
            }

            switch (Type2)
            {
                case StatusEffectType.GOD_BlESSING:
                    _test1 = _buffGod;
                    break;
                case StatusEffectType.DEVIL_SIN:
                    _test1 = _buffDevil;
                    break;
                case StatusEffectType.FIRE_DRAGON_BLOOD:
                    _test1 = _debuffDragon;
                    break;
                case StatusEffectType.GOLDEN_SERPANT_BILE:
                    _test1 = _debuffGolden;
                    break;
                case StatusEffectType.SLIME_MUCILAGE:
                    _test1 = _debuffSlime;
                    break;
            }
            positionX = r.Next(Convert.ToInt32(450 * 0.0234375f), Convert.ToInt32(800 * 0.0234375f));
            positionY = r.Next(Convert.ToInt32(370 * 0.0234375f), Convert.ToInt32(620 * 0.0234375f));
            positionX1 = r.Next(Convert.ToInt32(450 * 0.0234375f), Convert.ToInt32(800 * 0.0234375f));
            positionY1 = r.Next(Convert.ToInt32(370 * 0.0234375f), Convert.ToInt32(620 * 0.0234375f));

            _world = world;
            _entityManager = entityManager;

        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.PRE_PLAY:
                    Buff = new Buffs(
                        _world,
                        _test,
                        new Vector2(positionX, positionY)
                    );

                    Buff1 = new Buffs(
                        
                        _world,
                        _test1,
                        new Vector2(positionX1, positionY1)
                    );
                    _entityManager.AddEntry(Buff);
                    _entityManager.AddEntry(Buff1);
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
    }
}