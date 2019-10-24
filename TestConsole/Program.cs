using System;
using FP.Models;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //GameDayCollection gc = new GameDayCollection();
            //var games = gc.GetGames();

            GameCollection gc = new GameCollection();
            var games = gc.GetGames();

            foreach (var g in games)
                Console.WriteLine($"{g.GameDate} - {g.Code} {g.GameTime}");

            Console.Read();
        }
    }
}
