using Microsoft.Xna.Framework;

namespace Zchlachten.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(Zchlachten game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.GraphicsDevice.Clear(Color.DarkSlateBlue);
        }
    }
}