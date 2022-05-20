using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class DebugUI : IGameEntity
    {
        private const int TEXTURE_POS_X = 100;
        private const int TEXTURE_POS_Y = 100;

        private readonly ContentManager _content;
        private readonly Player _demonLord, _brave;

        private Texture2D _texture;
        private SpriteFont _font;

        

        public DebugUI(
            ContentManager content,
            Texture2D texture,
            SpriteFont font,
            Player demonLord,
            Player brave
        )
        {
            _content = content;

            _texture = texture;
            _font = font;

            _demonLord = demonLord;
            _brave = brave;
        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var text = "";

            text = "Play State: " + Globals.GameState;
            spriteBatch.DrawString(_font, text, new Vector2(5f, 5f), Color.White);

            text = "Player Turn: " + Globals.PlayerTurn;
            spriteBatch.DrawString(_font, text, new Vector2(5f, 25f), Color.White);

            // Demon Lord status
            text = "Demon Lord HP: " + _demonLord.HP;
            spriteBatch.DrawString(_font, text, new Vector2(140f, 100f), Color.White);
            text = "In-hand weapon: " + _demonLord.InHandWeapon;
            spriteBatch.DrawString(_font, text, new Vector2(140f, 120f), Color.White);
            switch (_demonLord.WeaponsBag.Count)
            {
                case 1:
                    text = "Weapon bag: " + _demonLord.WeaponsBag[0];
                    break;
                case 2:
                    text = "Weapon bag: " + _demonLord.WeaponsBag[0] + " " + _demonLord.WeaponsBag[1];
                    break;
                default:
                    text = "Weapon bag: empty";
                    break;
            }
            spriteBatch.DrawString(_font, text, new Vector2(140f, 140f), Color.White);

            // Brave status
            text = "Brave HP: " + _brave.HP;
            spriteBatch.DrawString(_font, text, new Vector2(600f, 100f), Color.White);
            text = "In-hand weapon: " + _brave.InHandWeapon;
            spriteBatch.DrawString(_font, text, new Vector2(600f, 120f), Color.White);
            switch (_brave.WeaponsBag.Count)
            {
                case 1:
                    text = "Weapon bag: " + _brave.WeaponsBag[0];
                    break;
                case 2:
                    text = "Weapon bag: " + _brave.WeaponsBag[0] + " " + _brave.WeaponsBag[1];
                    break;
                default:
                    text = "Weapon bag: empty";
                    break;
            }
            spriteBatch.DrawString(_font, text, new Vector2(600f, 140f), Color.White);

            // Weapon
            
        }
    }
}