using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Graphics;
using System;

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

        private Sprite _hpBarSprite, _demonHpSprite,_braveHpSprite;
        private Sprite _debuffFireSprite, _debuffSlimeSprite, _debuffCursedSprite;
        private Sprite _buffShieldSprite, _buffAttackSprite;
        private Sprite _braveBloodThirst, _demonBloodThirst;

        private float _DemonCurrentHp;
        private float _BraveCurrentHp;

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
            _hpBarSprite = new Sprite(content.Load<Texture2D>("UI/HPBar"));
            _demonHpSprite = new Sprite(content.Load<Texture2D>("Controls/DemonHp"));
            _braveHpSprite = new Sprite(content.Load<Texture2D>("Controls/BraveHp"));

            _demonBloodThirst = new Sprite(content.Load<Texture2D>("Controls/DemonBloodThirstGauge"));
            _braveBloodThirst = new Sprite(content.Load<Texture2D>("Controls/BraveBloodThirstGauge"));

            _debuffFireSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffFireIcon"));
            _debuffSlimeSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffSlimeIcon"));
            _debuffCursedSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffCursedIcon"));

            _buffShieldSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/BuffShieldIcon"));
            _buffAttackSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/BuffAttackIcon"));
        }

        public void Update(GameTime gameTime)
        {
            if (DemonLord.HP > 150) DemonLord.HP = 150;
            else if (Brave.HP > 150) Brave.HP = 150;

            _DemonCurrentHp = DemonLord.HP / 150f;
            Console.WriteLine("Current HP: " + _DemonCurrentHp);
            _BraveCurrentHp = Brave.HP / 150f;

            switch (Globals.GameState)
            {
                case GameState.POST_PLAY:
                    if (DemonLord.HP <= 0)
                    {
                        Globals.GameState = GameState.END;
                    }
                    else if (Brave.HP <= 0)
                    {
                        Globals.GameState = GameState.END;
                    }
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _demonHpSprite.Draw(spriteBatch, new Vector2(8.5f, 0.88f), new Vector2(_DemonCurrentHp, 1f)); // 8.4, 0.8 //0-1  //0-150 //21.44
            _braveHpSprite.Draw(spriteBatch, new Vector2(21.4f, 0.88f),new Vector2(_BraveCurrentHp,1f));
            _hpBarSprite.Draw(spriteBatch, new Vector2(Globals.Camera.Width / 2, 1.5f));

            if (DemonLord.BloodThirstGauge == 2)
            {
                _demonBloodThirst.Draw(spriteBatch, new Vector2(3.25f, 0.91f)); 
                _demonBloodThirst.Draw(spriteBatch, new Vector2(1.75f, 0.91f)); 
            }
            else if (DemonLord.BloodThirstGauge == 1)
            {
                _demonBloodThirst.Draw(spriteBatch, new Vector2(1.75f, 0.91f)); 
            }

            if (Brave.BloodThirstGauge == 2)
            {
                _braveBloodThirst.Draw(spriteBatch, new Vector2(26.69f, 0.91f)); 
                _braveBloodThirst.Draw(spriteBatch, new Vector2(28.14f, 0.91f)); 
            }
            else if (Brave.BloodThirstGauge == 1)
            {
                _braveBloodThirst.Draw(spriteBatch, new Vector2(26.69f, 0.91f)); 
            }

            


            float demonLordStatusEffectIconPosX = 1.2f;
            foreach (StatusEffect status in DemonLord.StatusEffectBag)
            {
                switch (status.Type)
                {
                    case StatusEffectType.FIRE_DRAGON_BLOOD:
                        _debuffFireSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.SLIME_MUCILAGE:
                        _debuffSlimeSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.GOLDEN_SERPANT_BILE:
                        _debuffCursedSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.SHIELD:
                        _buffShieldSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.ATTACK:
                        _buffAttackSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                }
                demonLordStatusEffectIconPosX += 0.8f;
            }

            float braveStatusEffectIconPosX = 28.7f;
            foreach (StatusEffect status in Brave.StatusEffectBag)
            {
                switch (status.Type)
                {
                    case StatusEffectType.FIRE_DRAGON_BLOOD:
                        _debuffFireSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.SLIME_MUCILAGE:
                        _debuffSlimeSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.GOLDEN_SERPANT_BILE:
                        _debuffCursedSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.SHIELD:
                        _buffShieldSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.ATTACK:
                        _buffAttackSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                }
                braveStatusEffectIconPosX -= 0.8f;
            }
        }
    }
}