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
      
        public static  SoundEffect bgm , soundFX;
        public static SoundEffectInstance _bgmInstance, _soundFXInstance;
        public static string song;

        public static float MusicVolume=0.0f,SoundVolume=0.1f;

        public static ScreenManager ScreenManager;

        public static GameState GameState;
        public static PlayerTurn PlayerTurn;
        public static bool IsShooting = false;
        public static int TotalTurn = 1;

        public static Camera2D Camera;
        
        public static DebugView DebugView;
    }
}