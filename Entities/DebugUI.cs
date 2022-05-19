using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Entities
{
    public class DebugUI : IGameEntity
    {
        private const int TEXTURE_POS_X = 100;
        private const int TEXTURE_POS_Y = 100;

        private readonly PlayerTurn _playerTurn;
        private readonly Player _demonLord, _brave;

        private Texture2D _texture;
        private SpriteFont _font;

        public DebugUI(
            Texture2D texture, 
            SpriteFont font,
            Player demonLord, 
            Player brave
        )
        {
            _texture = texture;
            _font = font;

            _demonLord = demonLord;
            _brave = brave;
        }

        public void Update(GameTime gameTime) 
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            var text = "";

            text = "Play State: " + Globals.GameState;
            spriteBatch.DrawString(_font, text, new Vector2(5f, 5f), Color.White);

            text = "Player Turn: " + _playerTurn;
            spriteBatch.DrawString(_font, text, new Vector2(5f, 25f), Color.White);

            text = "Demon Lord HP: " + _demonLord.HP;
            spriteBatch.DrawString(_font, text, new Vector2(140f, 500f), Color.White);
            text = "Brave HP: " + _brave.HP;
            spriteBatch.DrawString(_font, text, new Vector2(1120f, 500f), Color.White);
        }
    }
}