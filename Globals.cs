using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Zchlachten.Screens;
using tainicom.Aether.Physics2D.Diagnostics;

namespace Zchlachten
{
    public static class Globals
    {
        public static int SCREEN_WIDTH { get; } = 1280;
        public static int SCREEN_HEIGHT { get; } = 720;

        public static MouseState CurrentMouseState, PreviousMouseState;
        public static KeyboardState CurrentKeyboardState, PreviousKeyboardState;
        public static MouseCursor MouseCursor;

        public static SoundEffect soundFX;
        public static string song;

        public static float MusicVolume = 0.1f, SoundVolume = 0.1f;

        public static ScreenManager ScreenManager;

        public static GameState GameState;
        public static PlayerTurn PlayerTurn;
        public static bool IsShooting = false;
        public static int TotalTurn = 1;

        public static Camera2D Camera;

        public static DebugView DebugView;

        public static bool IsPlayerAffectedByCharm = false;

        public static bool IsClicked()
        {
            if (CurrentMouseState.LeftButton == ButtonState.Pressed
                    && PreviousMouseState.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsReleased()
        {
            if (CurrentMouseState.LeftButton == ButtonState.Released
                    && PreviousMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsHolding()
        {
            if (CurrentMouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }
    }
}