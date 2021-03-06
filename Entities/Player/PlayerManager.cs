using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Graphics;
using Microsoft.Xna.Framework.Audio;

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

        private Sprite _hpBarSprite, _demonHpSprite, _braveHpSprite;
        private Sprite _debuffFireSprite, _debuffSlimeSprite,
            _debuffCursedSprite, _debuffDrunkSprite;
        private Sprite _buffShieldSprite, _buffAttackSprite;
        private Sprite _braveBloodThirst, _demonBloodThirst;

        private float _DemonCurrentHp,_BraveCurrentHp;
    

        

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
            _demonHpSprite = new Sprite(content.Load<Texture2D>("UI/DemonHp"));
            _braveHpSprite = new Sprite(content.Load<Texture2D>("UI/BraveHp"));

            _demonBloodThirst = new Sprite(content.Load<Texture2D>("UI/DemonBloodThirstGauge"));
            _braveBloodThirst = new Sprite(content.Load<Texture2D>("UI/BraveBloodThirstGauge"));

            _debuffFireSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffFireIcon"));
            _debuffSlimeSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffSlimeIcon"));
            _debuffCursedSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffCursedIcon"));
            _debuffDrunkSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/DebuffDrunkIcon"));

            _buffShieldSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/BuffShieldIcon"));
            _buffAttackSprite = new Sprite(content.Load<Texture2D>("StatusEffects/Icons/BuffAttackIcon"));

            SoundEffect attackSFX = content.Load<SoundEffect>("Sound/Attacked");
            SoundEffectInstance attackSFXInstance = attackSFX.CreateInstance();
            DemonLord.AttackSFXInstance = attackSFXInstance;
            Brave.AttackSFXInstance = attackSFXInstance;
        }

        public void Update(GameTime gameTime)
        {
            if (DemonLord.HP > 150) DemonLord.HP = 150;
            else if (Brave.HP > 150) Brave.HP = 150;

            _DemonCurrentHp = DemonLord.HP / 150f;
            _BraveCurrentHp = Brave.HP / 150f;

            switch (Globals.GameState)
            {
                case GameState.PLAYING:
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
            _hpBarSprite.Draw(spriteBatch, new Vector2(Globals.Camera.Width / 2, 1.5f));
            _demonHpSprite.TextureOrigin = Vector2.Zero;
            _demonHpSprite.Draw(spriteBatch, new Vector2(4.1f, 0.71f), new Vector2(_DemonCurrentHp, 1f));
            _braveHpSprite.TextureOrigin = Vector2.Zero;
            _braveHpSprite.Draw(spriteBatch, new Vector2(25.9f, 0.71f), new Vector2(-_BraveCurrentHp, 1f));
            

            if (DemonLord.BloodThirstGauge == 2)
            {
                _demonBloodThirst.Draw(spriteBatch, new Vector2(3.25f, 0.9f));
                _demonBloodThirst.Draw(spriteBatch, new Vector2(1.75f, 0.9f));
            }
            else if (DemonLord.BloodThirstGauge == 1)
            {
                _demonBloodThirst.Draw(spriteBatch, new Vector2(1.75f, 0.9f));
            }

            if (Brave.BloodThirstGauge == 2)
            {
                _braveBloodThirst.Draw(spriteBatch, new Vector2(26.69f, 0.9f));
                _braveBloodThirst.Draw(spriteBatch, new Vector2(28.14f, 0.9f));
            }
            else if (Brave.BloodThirstGauge == 1)
            {
                _braveBloodThirst.Draw(spriteBatch, new Vector2(26.69f, 0.9f));
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
                    case StatusEffectType.DRUNKEN:
                        _debuffDrunkSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.SHIELD:
                        _buffShieldSprite.Draw(spriteBatch, new Vector2(demonLordStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.ATTACK_UP:
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
                    case StatusEffectType.DRUNKEN:
                        _debuffDrunkSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.SHIELD:
                        _buffShieldSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                    case StatusEffectType.ATTACK_UP:
                        _buffAttackSprite.Draw(spriteBatch, new Vector2(braveStatusEffectIconPosX, 1.8f));
                        break;
                }
                braveStatusEffectIconPosX -= 0.8f;
            }
        }
    }
}