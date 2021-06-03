using System;

namespace CardGameWar
{
    class Program
    {
        static void Main(string[] args)
        {
            int GameCount = 1;

            while(true)
            {
                try
                {
                    Console.WriteLine($"Game: {GameCount}");

                    Player player1 = new Player("Player 1");
                    Player player2 = new Player("Player 2");
                    Game game = new Game(player1, player2);
                    game.Play();

                    GameCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error drawing card: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
    }
}
