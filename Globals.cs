using Microsoft.Xna.Framework.Audio;
using Zchlachten.Screens;

namespace Zchlachten
{
    public static class Globals
    {
        public static int SCREEN_WIDTH { get; } = 1280;
        public static int SCREEN_HEIGHT { get; } = 720;

        public static  SoundEffect bgm , soundFX;
       public static SoundEffectInstance _bgmInstance, _soundFXInstance;
       public static string song;

       public static float MusicVolume=0.1f,SoundVolume=0.1f;

        public static ScreenManager ScreenManager;
    }
}