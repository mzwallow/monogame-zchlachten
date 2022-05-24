using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using Zchlachten.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Zchlachten.Entities
{
    public class StatusEffectManager : IGameEntity
    {
        private const float DEMON_LORD_HOLD_STATUS_EFFECT_START_POS_X = 1.7f;
        private const float DEMON_LORD_HOLD_STATUS_EFFECT_START_POS_Y = 3.9f;
        private const float BRAVE_HOLD_STATUS_EFFECT_START_POS_X = 28.3f;
        private const float BRAVE_HOLD_STATUS_EFFECT_START_POS_Y = 3.9f;

        private readonly World _world;
        private readonly EntityManager _entityManager;
        private readonly Player _demonLord, _brave;

        private float positionX, positionY, positionX1, positionY1;
        private static Random r = new Random();
        private Texture2D _buffGod, _buffDevil, _debuffDragon, _debuffGolden, _debuffSlime;

        private Sprite _holdingDebuffFireIconSprite, _holdingDebuffSlimeIconSprite, _holdingDebuffCursedIconSprite;

        public StatusEffectManager(
            World world,
            EntityManager entityManager,
            Player demonLord,
            Player brave
        )
        {
            _world = world;
            _entityManager = entityManager;

            _demonLord = demonLord;
            _brave = brave;
        }

        public void LoadContent(ContentManager content)
        {
            _buffGod = content.Load<Texture2D>("StatusEffects/blessing_of_god");
            _buffDevil = content.Load<Texture2D>("StatusEffects/blessing_of_devil");
            _debuffDragon = content.Load<Texture2D>("StatusEffects/fire_dragon_blood");
            _debuffGolden = content.Load<Texture2D>("StatusEffects/GoldenSerpantBile");
            _debuffSlime = content.Load<Texture2D>("StatusEffects/Slime");

            Texture2D _holdingDebuffFireIconTxr = content.Load<Texture2D>("StatusEffects/Icons/HoldingDebuffFireIcon");
            _holdingDebuffFireIconSprite = new Sprite(_holdingDebuffFireIconTxr);

            Texture2D _holdingDebuffSlimeIconTxr = content.Load<Texture2D>("StatusEffects/Icons/HoldingDebuffSlimeIcon");
            _holdingDebuffSlimeIconSprite = new Sprite(_holdingDebuffSlimeIconTxr);

            Texture2D _holdingDebuffCursedIconTxr = content.Load<Texture2D>("StatusEffects/Icons/HoldingDebuffCursedIcon");
            _holdingDebuffCursedIconSprite = new Sprite(_holdingDebuffCursedIconTxr);
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.PRE_PLAY:
                    // Random Bullshit Go
                    if (Globals.TotalTurn % 5 == 0 || Globals.TotalTurn == 1)
                    {
                        positionX = r.Next(Convert.ToInt32((450 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((800 + _buffGod.Width / 2) * 0.0234375f));
                        positionY = r.Next(Convert.ToInt32((370 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((620 + _buffGod.Width / 2) * 0.0234375f));
                        positionX1 = r.Next(Convert.ToInt32((450 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((800 + _buffGod.Width / 2) * 0.0234375f));
                        positionY1 = r.Next(Convert.ToInt32((370 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((620 + _buffGod.Width / 2) * 0.0234375f));
                        //Random Buff
                        var newBuffOne = RandomStatusEffect(new Vector2(positionX, positionY));
                        var newBuffTwo = RandomStatusEffect(new Vector2(positionX1, positionY1));

                        _entityManager.AddEntry(newBuffOne);
                        _entityManager.AddEntry(newBuffTwo);
                    }

                    // Handle status effect
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        // Handle status effects that deal direct damage to a player
                        StatusEffectHandler(_demonLord);

                        // Decrease holding status effect remainings
                        foreach (StatusEffect status in _demonLord.HoldStatusEffectBag)
                            status.HoldRemaining--;
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        // Handle status effects that deal direct damage to a player
                        StatusEffectHandler(_brave);

                        // Decrease holding status effect remainings
                        foreach (StatusEffect status in _brave.HoldStatusEffectBag)
                            status.HoldRemaining--;
                    }

                    Globals.GameState = GameState.PLAYING;
                    break;
                case GameState.POST_PLAY:
                    Globals.TotalTurn++;

                    // Remove status effects when 'player turns' equal to 4
                    foreach (StatusEffect buff in _entityManager.GetEntitiesOfType<StatusEffect>())
                    {
                        if (Globals.TotalTurn % 5 == 0)
                        {
                            _world.Remove(buff.Body);
                            _entityManager.RemoveEntity(buff);
                        }
                    }

                    // Check if holding status effects are available
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        // Handle removing status effects in 'bag' when remainings reached 0
                        StatusEffectBagHandler(_demonLord);

                        // Handle removing status effects in 'holding bag' when remainings reached 0
                        StatusEffectHoldBagHandler(_demonLord);

                        Globals.PlayerTurn = PlayerTurn.BRAVE;
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        // Handle removing status effects in 'bag' when remainings reached 0
                        StatusEffectBagHandler(_brave);

                        // Handle removing status effects in 'holding bag' when remainings reached 0
                        StatusEffectHoldBagHandler(_brave);

                        Globals.PlayerTurn = PlayerTurn.DEMON_LORD;
                    }

                    Globals.GameState = GameState.PRE_PLAY;
                    break;
            } // End switch

            // Remove status effects when hit
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
            // Draw Demon Lord's holding status effect
            Vector2 demonLordHoldStatusEffectPos = new Vector2(
                DEMON_LORD_HOLD_STATUS_EFFECT_START_POS_X,
                DEMON_LORD_HOLD_STATUS_EFFECT_START_POS_Y
            );
            if (_demonLord.HoldStatusEffectBag.Count > 0)
            {
                foreach (StatusEffect status in _demonLord.HoldStatusEffectBag)
                {
                    switch (status.Type)
                    {
                        case StatusEffectType.FIRE_DRAGON_BLOOD:
                            _holdingDebuffFireIconSprite.Draw(spriteBatch, demonLordHoldStatusEffectPos);
                            break;
                        case StatusEffectType.SLIME_MUCILAGE:
                            _holdingDebuffSlimeIconSprite.Draw(spriteBatch, demonLordHoldStatusEffectPos);
                            break;
                        case StatusEffectType.GOLDEN_SERPANT_BILE:
                            _holdingDebuffCursedIconSprite.Draw(spriteBatch, demonLordHoldStatusEffectPos);
                            break;
                    }
                    demonLordHoldStatusEffectPos.Y -= 0.5f;
                }
            }

            // Draw Brave's holding status effect
            Vector2 braveHoldStatusEffectPos = new Vector2(
                BRAVE_HOLD_STATUS_EFFECT_START_POS_X,
                BRAVE_HOLD_STATUS_EFFECT_START_POS_Y
            );
            if (_brave.HoldStatusEffectBag.Count > 0)
            {
                foreach (StatusEffect status in _brave.HoldStatusEffectBag)
                {
                    switch (status.Type)
                    {
                        case StatusEffectType.FIRE_DRAGON_BLOOD:
                            _holdingDebuffFireIconSprite.Draw(spriteBatch, braveHoldStatusEffectPos);
                            break;
                        case StatusEffectType.SLIME_MUCILAGE:
                            _holdingDebuffSlimeIconSprite.Draw(spriteBatch, braveHoldStatusEffectPos);
                            break;
                        case StatusEffectType.GOLDEN_SERPANT_BILE:
                            _holdingDebuffCursedIconSprite.Draw(spriteBatch, braveHoldStatusEffectPos);
                            break;
                    }
                    braveHoldStatusEffectPos.Y -= 0.5f;
                }
            }
        }

        // Random status effects to the screen
        private StatusEffect RandomStatusEffect(Vector2 position)
        {
            var values = Enum.GetValues(typeof(StatusEffectType));
            var statusType = (StatusEffectType)values.GetValue(new Random().Next(values.Length));

            StatusEffect status;
            switch (statusType)
            {
                case StatusEffectType.ATTACK_UP:
                    status = new BuffAttack(_world, _buffGod, position);
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
                    status = RandomStatusEffect(position);
                    break;
            }
            return status;
        }

        // Handle status effects that deal direct damage to a player
        private void StatusEffectHandler(Player player)
        {
            if (player.StatusEffectBag.Count > 0)
            {
                for (int i = player.StatusEffectBag.Count - 1; i > -1; --i)
                {
                    StatusEffect statusEffect = player.StatusEffectBag[i];
                    // When turn past, decrease status effect remainings
                    statusEffect.Remaining--;
                    switch (statusEffect.Type)
                    {
                        case StatusEffectType.FIRE_DRAGON_BLOOD:
                            if (player.StatusEffectBag.Count > 0)
                            {
                                for (int j = player.StatusEffectBag.Count - 1; j > -1; --j)
                                {
                                    // Negate damage when having shield buff
                                    StatusEffect x = player.StatusEffectBag[j];
                                    if (x.Type == StatusEffectType.SHIELD)
                                    {
                                        player.StatusEffectBag.RemoveAt(j);
                                        break;
                                    }
                                    player.HP -= 10;
                                }
                            }
                            break;
                        case StatusEffectType.GOLDEN_SERPANT_BILE:
                            // Golden serpent's bile debuff has 20% trigger chance
                            if (new Random().Next(0, 100) < 20)
                            {
                                for (int j = player.StatusEffectBag.Count - 1; j > -1; --j)
                                {
                                    // Negate damage when having shield buff
                                    StatusEffect x = player.StatusEffectBag[j];
                                    if (x.Type == StatusEffectType.SHIELD)
                                    {
                                        player.StatusEffectBag.RemoveAt(j);
                                        break;
                                    }
                                    player.HP /= 2;
                                }
                            }
                            break;
                    }
                }
            }
        }

        // Handle removing status effects in 'bag' when remainings reached 0
        private void StatusEffectBagHandler(Player player)
        {
            if (player.HoldStatusEffectBag.Count > 0)
            {
                for (int i = player.HoldStatusEffectBag.Count - 1; i > -1; --i)
                {
                    StatusEffect status = player.HoldStatusEffectBag[i];
                    if (status.HoldRemaining <= 0)
                        player.HoldStatusEffectBag.RemoveAt(i);
                }
            }
        }

        // Handle removing status effects in 'holding bag' when remainings reached 0
        private void StatusEffectHoldBagHandler(Player player)
        {
            if (player.StatusEffectBag.Count > 0)
            {
                for (int i = player.StatusEffectBag.Count - 1; i > -1; --i)
                {
                    StatusEffect status = player.StatusEffectBag[i];
                    if (status.Remaining <= 0)
                        player.StatusEffectBag.RemoveAt(i);
                }
            }
        }
    }
}