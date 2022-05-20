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

        private Texture2D _buffGod,_buffDevil,_debuffDragon,_debuffGolden,_debuffSlime,_test,_test1;

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
            positionX = r.Next(450, 800);
            positionY = r.Next(100, 350);
            positionX1 = r.Next(450, 800);
            positionY1 = r.Next(100, 350);


            _world = world;
            _entityManager = entityManager;

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
            // _entityManager.RemoveEntity(Buff1);
            // _entityManager.RemoveEntity(Buff);

        }

        public void Update(GameTime gameTime)
        {
            // switch (Globals.GameState)
            // {
            //     case GameState.PRE_PLAY:
            //         Buff = new Buffs(
            //             _world,
            //             _txr,
            //             new Vector2(positionX, positionY)
            //         );

            //         Buff1 = new Buffs(
            //             _world,
            //             _txr,
            //             new Vector2(positionX1, positionY1)
            //         );
            //         break;
            // }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}