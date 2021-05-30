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
            game.DisplayRounds();

            while(true)
            {
                try
                {
                    if (player1.AllDecksEmpty() ||
                        player2.AllDecksEmpty())
                    {
                        game.EndGame();
                    }

                    if (game.DeckEmpty())
                    {
                        game.ResetDecks();
                    }

                    game.Draw();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error drawing card: {ex.Message}");
                    game.EndGame();
                }
            }
        }
    }
}
