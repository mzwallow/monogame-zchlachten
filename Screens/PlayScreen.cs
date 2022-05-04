using Microsoft.Xna.Framework;

namespace Zchlachten.Screens
{
    public class PlayScreen : Screen
    {
        public PlayScreen(Zchlachten game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.DarkOliveGreen);
        }
    }
}