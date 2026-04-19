using System;

namespace ArkanoidFNA
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Arkanoid())
            {
                game.Run();
            }
        }
    }
}
