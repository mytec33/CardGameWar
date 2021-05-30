using System;
using System.Diagnostics;

namespace CardGameWar
{
    class Game
    {
        private const int MIN_CARDS_FOR_WAR = 4;

        private Player Player1;
        private Player Player2;

        public int Rounds { get; private set; }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;

            Rounds = 1;
        }

        public void ClearWarDecks(Player player1, Player player2)
        {
            Player1.ClearWarDeck();
            Player2.ClearWarDeck();
        }

        public bool DeckEmpty()
        {
            if (Player1.CountDeck() == 0 ||
                Player2.CountDeck() == 0)
            {
                return true;
            }

            return false;
        }

        public string DetermineWinner()
        {
            if (Player1.AllDecksEmpty())
            {
                return $"Player 2 WON!!!!!!!!!!!!!!!";
            }
            else if (Player2.AllDecksEmpty())
            {
                return $"Player 1 WON!!!!!!!!!!!!!!!";
            }
            else
            {
                return $"It's a draw!! No more cards for war when already engaged in war.";
            }
        }

        public void Draw()
        {
            Player1.DrawCard();
            Player2.DrawCard();

            Console.WriteLine($"{Player1.Card} vs {Player2.Card}");

            if (Player1.Card.Rank > Player2.Card.Rank)
            { 
                Console.WriteLine($"{Player1.Name} won");
                Player1.AddDrawnCardSideDeck(Player1.Card);
                Player1.AddDrawnCardSideDeck(Player2.Card);
                Player1.RemoveDrawnCard();
                Player2.RemoveDrawnCard();
            }
            else if (Player1.Card.Rank < Player2.Card.Rank)
            {
                Console.WriteLine($"{Player2.Name} won");
                Player2.AddDrawnCardSideDeck(Player1.Card);
                Player2.AddDrawnCardSideDeck(Player2.Card);
                Player1.RemoveDrawnCard();
                Player2.RemoveDrawnCard();
            }
            else
            {
                Console.WriteLine("WAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                DisplayDeckStatus(Player1);
                DisplayDeckStatus(Player2);

                ClearWarDecks(Player1, Player2);

                if ((Player1.CountDeck() + Player1.CountSideDeck() < MIN_CARDS_FOR_WAR) ||
                    Player2.CountDeck() + Player2.CountSideDeck() < MIN_CARDS_FOR_WAR)
                {
                    Console.WriteLine("Skipping war. Not enough total cards.");
                    Player1.AddDrawnCardSideDeck(Player1.Card);
                    Player2.AddDrawnCardSideDeck(Player2.Card);
                    Player1.RemoveDrawnCard();
                    Player2.RemoveDrawnCard();

                    // We don't end the game, we just bypass. There are enough cards
                    // to keep playing with in normal mode.
                    return;
                }

                // Otherwise we have enough cards to get into a war, and a possible draw
                if (Player1.CountDeck() <= MIN_CARDS_FOR_WAR ||
                    Player2.CountDeck() <= MIN_CARDS_FOR_WAR)
                {
                    Console.WriteLine("Not enough cards in deck, resetting.");
                    ResetDecks();
                }

                DrawThree();
            }
        }

        private void DrawThree()
        {
            Console.WriteLine("Entering DrawThree()");
            DisplayDeckStatus(Player1);
            DisplayDeckStatus(Player2);

            // Hiden in from players
            Player1.DrawThreeIntoWarDeck();
            Player2.DrawThreeIntoWarDeck();

            // Shown face up to compare
            Player1.DrawCard();
            Player2.DrawCard();

            Console.WriteLine($"{Player1.Card} vs {Player2.Card}");

            Player1.AddDrawnCardWarDeck(Player1.Card);
            Player2.AddDrawnCardWarDeck(Player2.Card);
            Player1.RemoveDrawnCard();
            Player2.RemoveDrawnCard();

            if (Player1.Card.Rank > Player2.Card.Rank)
            {
                Console.WriteLine($"{Player1.Name} won the war!");

                Player1.AddWarDeckToSideDeck(Player1);
                Player1.AddWarDeckToSideDeck(Player2);

            }
            else if (Player1.Card.Rank < Player2.Card.Rank)
            {
                Console.WriteLine($"{Player2.Name} won the war!");

                Player2.AddWarDeckToSideDeck(Player1);
                Player2.AddWarDeckToSideDeck(Player2);
            }
            else
            {
                Console.WriteLine("WAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                DisplayDeckStatus(Player1);
                DisplayDeckStatus(Player2);

                // Reset deck in case we have cards in side deck
                if (Player1.CountDeck() <= MIN_CARDS_FOR_WAR ||
                    Player2.CountDeck() <= MIN_CARDS_FOR_WAR)
                {
                    ResetDecks();
                }

                if ((Player1.CountDeck() + Player1.CountSideDeck() < MIN_CARDS_FOR_WAR) ||
                     Player2.CountDeck() + Player2.CountSideDeck() < MIN_CARDS_FOR_WAR)
                {
                    EndGame();
                }

                DrawThree();
            }

            ClearWarDecks(Player1, Player2);
        }

        public void DisplayDeckStatus(Player player)
        {
            Console.WriteLine($"{player.Name} - Cards: {player.CountDeck()} " +
                $"side deck: {player.CountSideDeck()} " +
                $"war deck: {player.CountWarDeck()}");
        }

        public void DisplayRounds()
        {
            if (Rounds != 0)
            {
                Console.WriteLine($"Rounds: {Rounds}");
            }
        }

        public void EndGame()
        {
            Console.WriteLine("Ending game");

            DisplayDeckStatus(Player1);
            DisplayDeckStatus(Player2);
            DisplayRounds();
            Console.WriteLine(DetermineWinner());

            Console.Write("Press any key to end...");
            Console.ReadKey();

            Environment.Exit(0);
        }

        public void ResetDecks()
        {
            Rounds++;
            DisplayRounds();

            Player1.ResetDeck();
            Player2.ResetDeck();

            Debug.Assert(Player1.CountAllDecks() + Player2.CountAllDecks() == 104);
            Debug.Assert(Player1.CountAllDecks() != 0);
            Debug.Assert(Player2.CountAllDecks() != 0);
        }
    }
}
