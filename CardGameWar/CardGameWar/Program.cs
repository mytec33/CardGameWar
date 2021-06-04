using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CardGameWar
{
    class Program
    {
        static void Main(string[] args)
        {
            int GameCount = 0;
            List<int> Rounds = new List<int>();
            List<string> Winners = new List<string>();

            Console.WriteLine("# = 1,000");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while(GameCount < 1000000)
            {
                try
                {
                    if (GameCount % 1000 == 0)
                        Console.Write("#");

                    Player player1 = new Player("Player 1");
                    Player player2 = new Player("Player 2");
                    Game game = new Game(player1, player2, false);
                    game.Play();

                    GameCount++;

                    // Gather statistics
                    Rounds.Add(game.Rounds);
                    Winners.Add(game.Winner);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error drawing card: {ex.Message}");
                    Console.ReadKey();
                }
            }
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            Console.WriteLine("\nStats:");

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            var players = Winners.GroupBy(i => i);
            foreach(var grp in players)
            {
                Console.WriteLine($"{grp.Key}: {grp.Count()}");
            }
        }
    }
}
