using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework.Input;
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

        public ItemManager(
            World world,
            EntityManager entityManager,
            Player demonlord,
            Player brave
        )
        {
            _world = world;
            _entityManager = entityManager;
            _demonLord = demonlord;
            _brave = brave;

        }

        public void LoadContent(ContentManager content)
        {
            _holyWaterTxr = content.Load<Texture2D>("Controls/mystery_potion");
            _shieldTxr = content.Load<Texture2D>("Controls/shield");
            _worldTreeTxr = content.Load<Texture2D>("Controls/blessing_of_world_tree");
            _itemsBag = content.Load<Texture2D>("Items/ItemBag");

            _demonLord.ItemsBag.Add(new HolyWater(_world, _holyWaterTxr));
            _demonLord.ItemsBag.Add(new Shield(_world,_shieldTxr));
            _demonLord.ItemsBag.Add(new WorldTree(_world, _worldTreeTxr));
            
            _brave.ItemsBag.Add(new WorldTree(_world, _worldTreeTxr));
            _brave.ItemsBag.Add(new Shield (_world,_shieldTxr));
            _brave.ItemsBag.Add(new HolyWater(_world, _holyWaterTxr));
        }

        public void Update(GameTime gameTime)
        {

            Vector2 relativeMousePosition = Globals.Camera.ConvertScreenToWorld(Globals.CurrentMouseState.Position);
            //Select Buff BraveAndDevil
            if (Globals.GameState == GameState.PLAYING)
            {
                if (Globals.PlayerTurn == PlayerTurn.DEMON_LORD) //Demon side
                {
                    var itemBagOnePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(428f, 112f));
                    var ItemBagTwoPosition = Globals.Camera.ConvertScreenToWorld(new Vector2(476f, 112f));
                    var ItemBagThreePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(524f, 112f));
                    if (relativeMousePosition.X >= itemBagOnePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= itemBagOnePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= itemBagOnePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= itemBagOnePosition.Y + _itemsBag.Height * 0.0234375f / 2)
                    {
                        Debug.WriteLine("Demon Item bag 1");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            _demonLord.ItemsBag.Remove(_demonLord.ItemsBag[0]);
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagTwoPosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagTwoPosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagTwoPosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagTwoPosition.Y + _itemsBag.Height * 0.0234375f / 2)
                    {
                        Debug.WriteLine("Demon Item bag 2");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {

                            _demonLord.ItemsBag.Remove(_demonLord.ItemsBag[1]);
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagThreePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagThreePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagThreePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagThreePosition.Y + _itemsBag.Height * 0.0234375f / 2)
                    {
                        Debug.WriteLine("Demon Item bag 3");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {

                            _demonLord.ItemsBag.Remove(_demonLord.ItemsBag[2]);
                        }
                    }
                }
                else //Brave side
                {
                    var itemBagOnePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(722f, 112f));
                    var ItemBagTwoPosition = Globals.Camera.ConvertScreenToWorld(new Vector2(770f, 112f));
                    var ItemBagThreePosition = Globals.Camera.ConvertScreenToWorld(new Vector2(818f, 112f));
                    if (relativeMousePosition.X >= itemBagOnePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= itemBagOnePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= itemBagOnePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= itemBagOnePosition.Y + _itemsBag.Height * 0.0234375f / 2)
                    {
                        Debug.WriteLine("Brave Item bag 1");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {

                            _brave.ItemsBag.Remove(_brave.ItemsBag[0]);
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagTwoPosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagTwoPosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagTwoPosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagTwoPosition.Y + _itemsBag.Height * 0.0234375f / 2)
                    {
                        Debug.WriteLine("Brave Item bag 2");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {

                            _brave.ItemsBag.Remove(_brave.ItemsBag[1]);
                        }
                    }
                    else if (relativeMousePosition.X >= ItemBagThreePosition.X - _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.X <= ItemBagThreePosition.X + _itemsBag.Width * 0.0234375f / 2
                                    && relativeMousePosition.Y >= ItemBagThreePosition.Y - _itemsBag.Height * 0.0234375f / 2
                                    && relativeMousePosition.Y <= ItemBagThreePosition.Y + _itemsBag.Height * 0.0234375f / 2)
                    {
                        Debug.WriteLine("Brave Item bag 3");
                        Mouse.SetCursor(MouseCursor.Hand);

                        if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed
                                && Globals.PreviousMouseState.LeftButton == ButtonState.Released)
                        {

                            _brave.ItemsBag.Remove(_brave.ItemsBag[2]);
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
              Globals.Camera.ConvertScreenToWorld(new Vector2(428f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //holy water
            spriteBatch.Draw(
              _demonLord.ItemsBag[0].Texture,
              Globals.Camera.ConvertScreenToWorld(new Vector2(428f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_demonLord.ItemsBag[0].Texture.Width / 2, _demonLord.ItemsBag[0].Texture.Height / 2),
              0.0234375f,
              SpriteEffects.FlipVertically,
              0f
            );

            // Demon bag 2
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(476f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //shield
            spriteBatch.Draw(
               _demonLord.ItemsBag[1].Texture,
              Globals.Camera.ConvertScreenToWorld(new Vector2(476f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2( _demonLord.ItemsBag[1].Texture.Width / 2,  _demonLord.ItemsBag[1].Texture.Height / 2),
              0.0234375f,
              SpriteEffects.FlipVertically,
              0f
            );
            // Demon bag 3
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(524f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //worldTree
            spriteBatch.Draw(
               _demonLord.ItemsBag[2].Texture,
              Globals.Camera.ConvertScreenToWorld(new Vector2(524f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_demonLord.ItemsBag[2].Texture.Width / 2, _demonLord.ItemsBag[2].Texture.Height / 2),
              0.0234375f,
              SpriteEffects.FlipVertically,
              0f
            );

            // Brave bag
            //Brave bag 4
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(722f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //worldTree
            spriteBatch.Draw(
             _brave.ItemsBag[0].Texture,
             Globals.Camera.ConvertScreenToWorld(new Vector2(722f, 112f)),
             null,
             Color.White,
             0f,
             new Vector2(_brave.ItemsBag[0].Texture.Width / 2, _brave.ItemsBag[0].Texture.Height / 2),
             0.0234375f,
             SpriteEffects.FlipVertically,
             0f
            );

            //Brave bag 5
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(770f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //shield
            spriteBatch.Draw(
              _brave.ItemsBag[1].Texture,
              Globals.Camera.ConvertScreenToWorld(new Vector2(770f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2( _brave.ItemsBag[1].Texture.Width / 2,  _brave.ItemsBag[1].Texture.Height / 2),
              0.0234375f,
              SpriteEffects.FlipVertically,
              0f
            );

            //Brave bag 6
            spriteBatch.Draw(
              _itemsBag,
              Globals.Camera.ConvertScreenToWorld(new Vector2(818f, 112f)),
              null,
              Color.White,
              0f,
              new Vector2(_itemsBag.Width / 2, _itemsBag.Height / 2),
              0.0234375f,
              SpriteEffects.None,
              0f
            );
            //holyWater
            spriteBatch.Draw(
               _brave.ItemsBag[2].Texture,
              Globals.Camera.ConvertScreenToWorld(new Vector2(818f, 112f)),
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





