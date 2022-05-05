using Microsoft.Xna.Framework;

namespace Zchlachten.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(Zchlachten game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            base.GraphicsDevice.Clear(Color.DarkSlateBlue);
        }
    }
}