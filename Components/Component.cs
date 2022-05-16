using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zchlachten.Components
{
    public interface Component
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
