using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Zchlachten.Entities
{
    public class ItemManager : IGameEntity
    {
        private readonly World _world;
        private readonly Player _demonLord, _brave;
        private readonly EntityManager _entityManager;

        private Texture2D _worldTreeTxr, _shieldTxr, _holyWaterTxr;
        private Texture2D _itemsBag;
        private SoundEffectInstance _useItemInstance;

        public ItemManager(
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
            _holyWaterTxr = content.Load<Texture2D>("Items/mystery_potion");
            _shieldTxr = content.Load<Texture2D>("Items/shield");
            _worldTreeTxr = content.Load<Texture2D>("Items/blessing_of_world_tree");
            _itemsBag = content.Load<Texture2D>("Items/ItemBag");


            _demonLord.ItemsBag[0] = new WorldTree(_world, _worldTreeTxr);
            _demonLord.ItemsBag[1] = new Shield(_world, _shieldTxr);
            _demonLord.ItemsBag[2] = new HolyWater(_world, _holyWaterTxr);

            _brave.ItemsBag[0] = new HolyWater(_world, _holyWaterTxr);
            _brave.ItemsBag[1] = new Shield(_world, _shieldTxr);
            _brave.ItemsBag[2] = new WorldTree(_world, _worldTreeTxr);

            Globals.soundFX = content.Load<SoundEffect>("Sound/UseItems");
            _useItemInstance = Globals.soundFX.CreateInstance();           
        }

        public void Update(GameTime gameTime)
        {

            Vector2 relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);
            //Select Buff BraveAndDevil
            if (Globals.GameState == GameState.PLAYING && !Globals.IsShooting)
            {
                if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD) //Demon side
                {
                    var itemBagOnePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(395.63f, 637f));
                    var ItemBagTwoPosition = Globals.Camera.ConvertScreenToWorld(new Vector2(443.63f, 637f));
                    var ItemBagThreePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(491.63f, 637f));
                    if (relativeMousePosition.X >= itemBagOnePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= itemBagOnePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= itemBagOnePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= itemBagOnePosition.Y + _itemsBag.Height * 0.0234375f / 2
                                    && _demonLord.ItemsBag[0] != null)
                    {
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _useItemInstance.Play();
                            // if(_demonLord.ItemsBag.Count == )
                            _demonLord.ItemsBag[0] = null;
                            _demonLord.HP += 45;
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagTwoPosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagTwoPosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagTwoPosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagTwoPosition.Y + _itemsBag.Height * 0.0234375f / 2
                                    && _demonLord.ItemsBag[1] != null)
                    {
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _useItemInstance.Play();
                            _demonLord.ItemsBag[1] = null;
                            _demonLord.StatusEffectBag.Add(new BuffShield());
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagThreePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagThreePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagThreePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagThreePosition.Y + _itemsBag.Height * 0.0234375f / 2
                                    && _demonLord.ItemsBag[2] != null)
                    {
                        
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _useItemInstance.Play();
                            _demonLord.ItemsBag[2] = null;
                            _demonLord.StatusEffectBag.Clear();
                        }
                    }
                }
                else //Brave side
                {
                    var itemBagOnePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(769.63f, 637f));
                    var ItemBagTwoPosition = Globals.Camera.ConvertScreenToWorld(new Vector2(817.63f, 637f));
                    var ItemBagThreePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(865.63f, 637f));
                    if (relativeMousePosition.X >= itemBagOnePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= itemBagOnePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= itemBagOnePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= itemBagOnePosition.Y + _itemsBag.Height * 0.0234375f / 2
                                    && _brave.ItemsBag[0] != null)
                    {
                       
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _useItemInstance.Play();
                            _brave.StatusEffectBag.Clear();
                            _brave.ItemsBag[0] = null;
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagTwoPosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagTwoPosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagTwoPosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagTwoPosition.Y + _itemsBag.Height * 0.0234375f / 2
                                     && _brave.ItemsBag[1] != null)
                    {
                        Debug.WriteLine("Brave Item bag 2");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _useItemInstance.Play();
                            _brave.StatusEffectBag.Add(new BuffShield());
                            _brave.ItemsBag[1] = null;
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagThreePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagThreePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagThreePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagThreePosition.Y + _itemsBag.Height * 0.0234375f / 2
                                     && _brave.ItemsBag[2] != null)
                    {
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _useItemInstance.Play();
                            _brave.HP += 45;
                            _brave.ItemsBag[2] = null;

                        }
                    }
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Demon Bag
            // Demon bag 1
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(395.63f, 637f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //holy water
            if (_demonLord.ItemsBag[0] != null)
            {
                spriteBatch.Draw(
                  _demonLord.ItemsBag[0].Texture,
                  Globals.Camera.ConvertScreenToWorld(new Vector2(395.63f, 637f)),
                  null,
                  Color.White,
                  0f,
                  new Vector2(_demonLord.ItemsBag[0].Texture.Width / 2, _demonLord.ItemsBag[0].Texture.Height / 2),
                  0.0234375f,
                  SpriteEffects.FlipVertically,
                  0f
                );
            }

            // Demon bag 2
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(443.63f, 637f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            if (_demonLord.ItemsBag[1] != null)
            {
                //shield
                spriteBatch.Draw(
                   _demonLord.ItemsBag[1].Texture,
                  Globals.Camera.ConvertScreenToWorld(new Vector2(443.63f, 637f)),
                  null,
                  Color.White,
                  0f,
                  new Vector2(_demonLord.ItemsBag[1].Texture.Width / 2, _demonLord.ItemsBag[1].Texture.Height / 2),
                  0.0234375f,
                  SpriteEffects.FlipVertically,
                  0f
                );
            }
            // Demon bag 3
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(491.63f, 637f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            if (_demonLord.ItemsBag[2] != null)
            {
                //worldTree
                spriteBatch.Draw(
                   _demonLord.ItemsBag[2].Texture,
                  Globals.Camera.ConvertScreenToWorld(new Vector2(491.63f, 637f)),
                  null,
                  Color.White,
                  0f,
                  new Vector2(_demonLord.ItemsBag[2].Texture.Width / 2, _demonLord.ItemsBag[2].Texture.Height / 2),
                  0.0234375f,
                  SpriteEffects.FlipVertically,
                  0f
                );
            }

            // Brave bag
            //Brave bag 4
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(769.63f, 637f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            if (_brave.ItemsBag[0] != null)
            {
                //worldTree
                spriteBatch.Draw(
                 _brave.ItemsBag[0].Texture,
                 Globals.Camera.ConvertScreenToWorld(new Vector2(769.63f, 637f)),
                 null,
                 Color.White,
                 0f,
                 new Vector2(_brave.ItemsBag[0].Texture.Width / 2, _brave.ItemsBag[0].Texture.Height / 2),
                 0.0234375f,
                 SpriteEffects.FlipVertically,
                 0f
                );
            }

            //Brave bag 5
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(817.63f, 637f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            if (_brave.ItemsBag[1] != null)
            {
                //shield
                spriteBatch.Draw(
                  _brave.ItemsBag[1].Texture,
                  Globals.Camera.ConvertScreenToWorld(new Vector2(817.63f, 637f)),
                  null,
                  Color.White,
                  0f,
                  new Vector2(_brave.ItemsBag[1].Texture.Width / 2, _brave.ItemsBag[1].Texture.Height / 2),
                  0.0234375f,
                  SpriteEffects.FlipVertically,
                  0f
                );
            }

            //Brave bag 6
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(865.63f, 637f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //holyWater
            if (_brave.ItemsBag[2] != null)
            {
                spriteBatch.Draw(
                   _brave.ItemsBag[2].Texture,
                  Globals.Camera.ConvertScreenToWorld(new Vector2(865.63f, 637f)),
                  null,
                  Color.White,
                  0f,
                  new Vector2(_brave.ItemsBag[2].Texture.Width / 2, _brave.ItemsBag[2].Texture.Height / 2),
                  0.0234375f,
                  SpriteEffects.FlipVertically,
                  0f
                );
            }
        }
    }
}





