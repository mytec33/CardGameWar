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
            int processors = 5;
            int iterations = 1200000 / processors;

            Console.WriteLine("# = 1,000");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Task task1 = Task.Run(() => RunGame(iterations));
            Task task2 = Task.Run(() => RunGame(iterations));
            Task task3 = Task.Run(() => RunGame(iterations));
            Task task4 = Task.Run(() => RunGame(iterations));
            Task task5 = Task.Run(() => RunGame(iterations));
            //Task task6 = Task.Run(() => RunGame(iterations));
            //Task task7 = Task.Run(() => RunGame(iterations));
            //Task task8 = Task.Run(() => RunGame(iterations));
            //Task task9 = Task.Run(() => RunGame(iterations));
            //Task task10 = Task.Run(() => RunGame(iterations));
            //Task task11 = Task.Run(() => RunGame(iterations));
            //Task task12 = Task.Run(() => RunGame(iterations));



            task1.Wait();
            task2.Wait();
            task3.Wait();
            task4.Wait();
            task5.Wait();
            //task6.Wait();
            //task7.Wait();
            //task8.Wait();
            //task9.Wait();
            //task10.Wait();
            //task11.Wait();
            //task12.Wait();

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
