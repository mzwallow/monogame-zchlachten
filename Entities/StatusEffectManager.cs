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
        private Player _demonLord, _brave;
        //private StatusEffect DebuffDragon;

        public StatusEffectManager(
            World world,
            EntityManager entityManager,
            Player demon,
            Player brave
        )
        {
            _world = world;
            _entityManager = entityManager;

            _brave = brave;
            _demonLord = demon;
        }

        public void LoadContent(ContentManager content)
        {
            _buffGod = content.Load<Texture2D>("Controls/blessing_of_god");
            _buffDevil = content.Load<Texture2D>("Controls/blessing_of_devil");
            _debuffDragon = content.Load<Texture2D>("Controls/fire_dragon_blood");
            _debuffGolden = content.Load<Texture2D>("Controls/golden_crow_bile");
            _debuffSlime = content.Load<Texture2D>("Controls/Slime");

            //DebuffDragon = new DebuffDragon(_world, _debuffDragon);
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState)
            {
                case GameState.PRE_PLAY:
                    //Random Bullshit
                    if (Globals.TotalTurn % 4 == 0 || Globals.TotalTurn == 1)
                    {
                        positionX = r.Next(Convert.ToInt32((450 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((800 + _buffGod.Width / 2) * 0.0234375f));
                        positionY = r.Next(Convert.ToInt32((370 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((620 + _buffGod.Width / 2) * 0.0234375f));
                        positionX1 = r.Next(Convert.ToInt32((450 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((800 + _buffGod.Width / 2) * 0.0234375f));
                        positionY1 = r.Next(Convert.ToInt32((370 + _buffGod.Width / 2) * 0.0234375f), Convert.ToInt32((620 + _buffGod.Width / 2) * 0.0234375f));
                        //Random Buff
                        var newBuff = RandomBuff(new Vector2(positionX, positionY));
                        var newBuff1 = RandomBuff(new Vector2(positionX1, positionY1));

                        _entityManager.AddEntry(newBuff);
                        _entityManager.AddEntry(newBuff1);

                    }

                    Console.WriteLine("totalTurn: " + Globals.TotalTurn);

                    // Handle status effect
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        foreach (StatusEffect status in _demonLord.StatusEffectBag)
                        {
                            status.Remaining--;
                            switch (status.Type)
                            {
                                case StatusEffectType.FIRE_DRAGON_BLOOD:
                                    _demonLord.HP -= 10;
                                    break;
                                case StatusEffectType.GOLDEN_SERPANT_BILE:
                                    if (new Random().Next(0, 100) < 20)
                                    {
                                        _demonLord.HP /= 2;
                                    }
                                    break;
                            }

                            Console.WriteLine("Demon Effect bag: " + status);
                        }

                        foreach (StatusEffect status in _demonLord.HoldStatusEffectBag)
                        {
                            status.HoldRemaining--;
                            Console.WriteLine("Demon Hold Effect: " + status);
                        }
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        foreach (StatusEffect status in _brave.StatusEffectBag)
                        {
                            status.Remaining--;
                            switch (status.Type)
                            {
                                case StatusEffectType.FIRE_DRAGON_BLOOD:
                                    _brave.HP -= 10;
                                    break;
                                case StatusEffectType.GOLDEN_SERPANT_BILE:
                                    if (new Random().Next(0, 100) < 20)
                                    {
                                        _brave.HP /= 2;
                                    }
                                    break;
                            }

                            Console.WriteLine("brave Effect bag: " + status);
                        }

                        foreach (StatusEffect status in _brave.HoldStatusEffectBag)
                        {
                            status.HoldRemaining--;
                            Console.WriteLine("brave Hold Effect: " + status);
                        }
                    }

                    if (_demonLord.StatusEffectBag.Count > 0)
                    {
                        for (int i = _demonLord.StatusEffectBag.Count - 1; i > -1; --i)
                        {
                            var x = _demonLord.StatusEffectBag[i];
                            if (x.Remaining == 0)
                                _demonLord.StatusEffectBag.RemoveAt(i);
                        }
                    }
                    
                    if (_brave.StatusEffectBag.Count > 0)
                    {
                        for (int i = _brave.StatusEffectBag.Count - 1; i > -1; --i)
                        {
                            var x = _brave.StatusEffectBag[i];
                            if (x.Remaining == 0)
                                _brave.StatusEffectBag.RemoveAt(i);
                        }
                    }

                    Globals.GameState = GameState.PLAYING;
                    break;
                case GameState.PLAYING:
                    // if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    // {
                    //     foreach (StatusEffect status in _demonLord.StatusEffectBag)
                    //     {
                    //         switch (status.Type)
                    //         {
                    //             case StatusEffectType.ATTACK:
                    //                 var tmp = _demonLord.InHandWeapon.Damage * 1.25f;
                    //                 _demonLord.InHandWeapon.Damage = (int)Math.Ceiling(tmp);
                    //                 break;
                    //             case StatusEffectType.SLIME_MUCILAGE:
                    //                 var tmp1 = _demonLord.InHandWeapon.Damage * 0.8f;
                    //                 _demonLord.InHandWeapon.Damage = (int)Math.Ceiling(tmp1);
                    //                 break;
                    //         }
                    //     }

                    //     foreach (StatusEffect status in _demonLord.HoldStatusEffectBag)
                    //     {
                    //         status.HoldRemaining--;
                    //     }
                    // }

                    // if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    // {
                    //     foreach (StatusEffect status in _brave.StatusEffectBag)
                    //     {
                    //         switch (status.Type)
                    //         {
                    //             case StatusEffectType.ATTACK:
                    //                 var tmp = _brave.InHandWeapon.Damage * 1.25f;
                    //                 _brave.InHandWeapon.Damage = (int)Math.Ceiling(tmp);
                    //                 break;
                    //             case StatusEffectType.SLIME_MUCILAGE:
                    //                 var tmp1 = _brave.InHandWeapon.Damage * 0.8f;
                    //                 _brave.InHandWeapon.Damage = (int)Math.Ceiling(tmp1);
                    //                 break;
                    //         }
                    //     }

                    //     foreach (StatusEffect status in _brave.HoldStatusEffectBag)
                    //     {
                    //         status.HoldRemaining--;
                    //     }
                    // }
                    break;
                case GameState.POST_PLAY:
                    Globals.TotalTurn++;
                    
                    // Remove status effects when player turns equal 4
                    foreach (StatusEffect buff in _entityManager.GetEntitiesOfType<StatusEffect>())
                    {
                        if (Globals.TotalTurn % 4 == 0)
                        {
                            _world.Remove(buff.Body);
                            _entityManager.RemoveEntity(buff);
                        }
                    }

                    // Check if holding status effects are available
                    if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD)
                    {
                        if (_demonLord.HoldStatusEffectBag.Count > 0)
                        {
                            for (int i = _demonLord.HoldStatusEffectBag.Count - 1; i > -1; --i)
                            {
                                var x = _demonLord.HoldStatusEffectBag[i];
                                Console.WriteLine("HoldRemaining Demon: " + x.HoldRemaining);
                                if (x.HoldRemaining == 0)
                                {
                                    _demonLord.HoldStatusEffectBag.RemoveAt(i);
                                    Console.WriteLine("Deleted Hold Demon: " + x.Type);
                                }
                            }
                        }
                    }
                    else if (Globals.PlayerTurn == PlayerTurn.BRAVE)
                    {
                        if (_brave.HoldStatusEffectBag.Count > 0)
                        {
                            for (int i = _brave.HoldStatusEffectBag.Count - 1; i > -1; --i)
                            {
                                var x = _brave.HoldStatusEffectBag[i];
                                Console.WriteLine("HoldRemaining Brave: " + x.HoldRemaining);
                                if (x.HoldRemaining == 0)
                                {
                                    _brave.HoldStatusEffectBag.RemoveAt(i);
                                    Console.WriteLine("Deleted Hold Brave: " + x.Type);
                                }
                            }
                        }
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        private StatusEffect RandomBuff(Vector2 position)
        {
            var values = Enum.GetValues(typeof(StatusEffectType));
            var statusType = (StatusEffectType)values.GetValue(new Random().Next(values.Length));

            StatusEffect status;
            switch (statusType)
            {
                case StatusEffectType.ATTACK:
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
                    status = RandomBuff(position);
                    break;
            }
            return status;
        }
    }
}