using System;

namespace CardGameWar
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            Game game = new Game(player1, player2);

            int rounds = 0;

            while(true)
            {
                try
                {
                    if (player1.CountDeck() == 0 ||
                        player2.CountDeck() == 0)
                    {
                        player1.ResetDeck();
                        player2.ResetDeck();

                        rounds++;
                    }

                    if (player1.AllDecksEmpty())
                    {
                        Console.WriteLine($"Player 1 WON!!!!!!!!!!!!!!!\n{rounds} rounds");
                        break;
                    }
                    else if (player2.AllDecksEmpty())
                    {
                        Console.WriteLine($"Player 2 WON!!!!!!!!!!!!!!!\n{rounds} rounds");
                        break;
                    }

                    game.Draw();
                    game.DisplayDeckStatus(player1);
                    game.DisplayDeckStatus(player2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error drawing card: {ex.Message}");
                }
            }

            Console.ReadKey();
        }
    }
}
