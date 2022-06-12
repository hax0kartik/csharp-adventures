using System;

namespace Book_Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Snake())
                game.Run();
        }
    }
}
