using System;

namespace Zchlachten
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Zchlachten())
                game.Run();
        }
    }
}
