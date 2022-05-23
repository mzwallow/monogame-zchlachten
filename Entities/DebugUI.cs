using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Zchlachten.Entities
{
    public class DebugUI : IGameEntity
    {
        private const int TEXTURE_POS_X = 100;
        private const int TEXTURE_POS_Y = 100;

        private readonly World _world;
        private readonly Player _demonLord, _brave;

        private Texture2D _texture;
        private SpriteFont _font;

        public DebugUI(
            World world,
            Texture2D texture,
            SpriteFont font,
            Player demonLord,
            Player brave
        )
        {
            _world = world;

            _texture = texture;
            _font = font;

            _demonLord = demonLord;
            _brave = brave;
        }

        public void LoadContent() { }

        public void Update(GameTime gameTime) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var text = "";

            text = "Play State: " + Globals.GameState;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(5f, 25f)), 
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );

            text = "Player Turn: " + Globals.PlayerTurn;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(5f, 45f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );

            text = "Gravity: " + _world.Gravity;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(5f, 65f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );

            // Demon Lord status
            text = "Demon Lord Position: " + _demonLord.Body.Position;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(140f, 100f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            text = "Demon Lord HP: " + _demonLord.HP;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(140f, 120f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            text = "In-hand weapon: " + _demonLord.InHandWeapon;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(140f, 140f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            switch (_demonLord.WeaponsBag.Count)
            {
                case 1:
                    text = "Weapon bag: " + _demonLord.WeaponsBag[0].Type;
                    break;
                case 2:
                    text = "Weapon bag: " + _demonLord.WeaponsBag[0].Type + " " + _demonLord.WeaponsBag[1].Type;
                    break;
                default:
                    text = "Weapon bag: empty";
                    break;
            }
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(140f, 160f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            text = "Blood thirst gauge: " + _demonLord.BloodThirstGauge;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(140f, 180f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );

            // Brave status
            text = "Brave Position: " + _brave.Body.Position;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(600f, 100f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            text = "Brave HP: " + _brave.HP;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(600f, 120f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            text = "In-hand weapon: " + _brave.InHandWeapon;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(600f, 140f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            switch (_brave.WeaponsBag.Count)
            {
                case 1:
                    text = "Weapon bag: " + _brave.WeaponsBag[0].Type;
                    break;
                case 2:
                    text = "Weapon bag: " + _brave.WeaponsBag[0].Type + " " + _brave.WeaponsBag[1].Type;
                    break;
                default:
                    text = "Weapon bag: empty";
                    break;
            }
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(600f, 160f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );
            text = "Blood thirst gauge: " + _brave.BloodThirstGauge;
            spriteBatch.DrawString(
                _font, 
                text,
                Globals.Camera.ConvertScreenToWorld(new Vector2(600f, 180f)),
                Color.White,
                0f,
                Vector2.Zero,
                0.0234375f,
                SpriteEffects.FlipVertically,
                0f
            );

            // Weapon
            
        }
    }
}