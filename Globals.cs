using Microsoft.Xna.Framework.Input;
using Zchlachten.Screens;

namespace Zchlachten
{
    public static class Globals
    {
        public static int SCREEN_WIDTH { get; } = 1280;
        public static int SCREEN_HEIGHT { get; } = 720;

        public static MouseState CurrentMouseState, PreviousMouseState;

        public static ScreenManager ScreenManager;
    }
}