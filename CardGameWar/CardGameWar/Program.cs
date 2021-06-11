using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CardGameWar
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("You must specify the number of processors.");
                Environment.Exit(1);
            }

            int processors = 1;
            if (int.TryParse(args[0], out int result))
            {
                processors = result;
            }
            else
            {
                Console.WriteLine("Cannot parse number of processors. Exiting.");
                Environment.Exit(2);
            }
                
            int iterations = 1200000 / processors;

            Console.WriteLine($"Processors: {processors}");
            Console.WriteLine($"Games / processor: {iterations.ToString("#,###")}");
            Console.WriteLine("# = 1,000");

            Stopwatch sw = new();
            sw.Start();

            Parallel.For(0, processors, i =>
            {
                Console.WriteLine($"Starting task {i}");
                RunGame(iterations);
            });

            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        private static int RunGame(int count)
        {
            int GameCount = 0;
            List<int> Rounds = new List<int>();
            List<string> Winners = new List<string>();

            while (GameCount < count)
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

            Console.WriteLine("\nStats:");

            var players = Winners.GroupBy(i => i);
            foreach (var grp in players)
            {
                Console.WriteLine($"{grp.Key}: {grp.Count()}");
            }

            return 1;
        }
    }
}
