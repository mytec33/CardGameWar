using System;
using System.Diagnostics;

namespace CardGameWar
{
    class Game
    {
        private const int MIN_CARDS_FOR_WAR = 4;

        private CardDeck Deck;
        private Player Player1;
        private Player Player2;

        private bool GameOver = false;
        private bool Verbose;

        public int Rounds { get; private set; }
        public string Winner;

        public Game(Player player1, Player player2, bool verbose)
        {
            Player1 = player1;
            Player2 = player2;
           
            EnableVerbose(verbose);

            Deck = new CardDeck("Main Deck");
            Deck.GenerateFullDeck();
            Deck.Shuffle();
            SplitDeck(Deck);

            Debug.Assert(Player1.CountAllDecks() + Player2.CountAllDecks() == 52);

            Rounds = 1;
        }

        private void EnableVerbose(bool verosity)
        {
            Verbose = verosity;

            if (Verbose)
            {
                Player1.EnableVerbose(Verbose);
                Player2.EnableVerbose(Verbose);
            }
        }

        public void SplitDeck(CardDeck deck)
        {
            int x = 0;
            foreach(Card card in deck)
            {
                if (x % 2 == 0)
                    Player1.AddCardToDeck(card);
                else
                    Player2.AddCardToDeck(card);

                x++;
            }
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
                Winner = "Player 2";
                return $"Player 2 WON!!!!!!!!!!!!!!!";
            }
            else if (Player2.AllDecksEmpty())
            {
                Winner = "Player 1";
                return $"Player 1 WON!!!!!!!!!!!!!!!";
            }
            else
            {
                Winner = "Draw";
                return $"It's a draw!! No more cards for war when already engaged in war.";
            }
        }

        public void Draw()
        {
            if (Verbose)
            {
                Debug.Assert(Player1.CountDeck() != 0);
                Debug.Assert(Player2.CountDeck() != 0);
            }

            Player1.DrawCard();
            Player2.DrawCard();

            WriteMessage($"{Player1.Card} vs {Player2.Card}");

            if (Player1.Card.Rank > Player2.Card.Rank)
            {
                if (Verbose)
                    WriteMessage($"{Player1.Name} won");

                Player1.AddDrawnCardSideDeck(Player1.Card);
                Player1.AddDrawnCardSideDeck(Player2.Card);
                Player1.RemoveDrawnCard();
                Player2.RemoveDrawnCard();
            }
            else if (Player1.Card.Rank < Player2.Card.Rank)
            {
                if (Verbose)
                    WriteMessage($"{Player2.Name} won");

                Player2.AddDrawnCardSideDeck(Player1.Card);
                Player2.AddDrawnCardSideDeck(Player2.Card);
                Player1.RemoveDrawnCard();
                Player2.RemoveDrawnCard();
            }
            else
            {
                if (Verbose)
                {
                    WriteMessage("WAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!", false);
                    DisplayDeckStatus(Player1);
                    DisplayDeckStatus(Player2);
                }

                ClearWarDecks(Player1, Player2);

                if ((Player1.CountDeck() + Player1.CountSideDeck() < MIN_CARDS_FOR_WAR) ||
                    Player2.CountDeck() + Player2.CountSideDeck() < MIN_CARDS_FOR_WAR)
                {
                    if (Verbose)
                        WriteMessage("Skipping war. Not enough total cards.", false);

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
                    if (Verbose)
                        WriteMessage("Not enough cards in deck, resetting.", false);

                    ResetDecks();
                }

                DrawThree();
            }
        }

        private void DrawThree()
        {
            if (Verbose)
            {
                WriteMessage("Entering DrawThree()");
                DisplayDeckStatus(Player1);
                DisplayDeckStatus(Player2);
            }

            // Hiden in from players
            Player1.DrawThreeIntoWarDeck();
            Player2.DrawThreeIntoWarDeck();

            // Shown face up to compare
            Player1.DrawCard();
            Player2.DrawCard();

            if (Verbose)
                WriteMessage($"{Player1.Card} vs {Player2.Card}");

            Player1.AddDrawnCardWarDeck(Player1.Card);
            Player2.AddDrawnCardWarDeck(Player2.Card);
            Player1.RemoveDrawnCard();
            Player2.RemoveDrawnCard();

            if (Player1.Card.Rank > Player2.Card.Rank)
            {
                if (Verbose)
                    WriteMessage($"{Player1.Name} won the war!", false);

                Player1.AddWarDeckToSideDeck(Player1);
                Player1.AddWarDeckToSideDeck(Player2);

            }
            else if (Player1.Card.Rank < Player2.Card.Rank)
            {
                if (Verbose)
                    WriteMessage($"{Player2.Name} won the war!", false);

                Player2.AddWarDeckToSideDeck(Player1);
                Player2.AddWarDeckToSideDeck(Player2);
            }
            else
            {
                if (Verbose)
                {
                    WriteMessage("WAR LOOP!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!", false);

                    DisplayDeckStatus(Player1);
                    DisplayDeckStatus(Player2);
                }


                // Reset deck in case we have cards in side deck
                if (Player1.CountDeck() <= MIN_CARDS_FOR_WAR ||
                    Player2.CountDeck() <= MIN_CARDS_FOR_WAR)
                {
                    ResetDecks();
                }

                if ((Player1.CountDeck() + Player1.CountSideDeck() < MIN_CARDS_FOR_WAR) ||
                     Player2.CountDeck() + Player2.CountSideDeck() < MIN_CARDS_FOR_WAR)
                {
                    if (Verbose)
                        WriteMessage("Not enough cards to continue play. Draw.");

                    EndGame();
                }

                if (!GameOver)
                {
                    DrawThree();
                }
            }

            ClearWarDecks(Player1, Player2);
        }

        public void DisplayDeckStatus(Player player)
        {
            if (Verbose)
                WriteMessage($"{player.Name} - Cards: {player.CountDeck()} " +
                $"side deck: {player.CountSideDeck()} " +
                $"war deck: {player.CountWarDeck()}", false);
        }

        public void DisplayRounds()
        {
            if (Rounds != 0)
            {
                if (Verbose)
                    WriteMessage($"Rounds: {Rounds}", false);
            }
        }

        public void EndGame()
        {
            if (Verbose)
                WriteMessage("Ending game");

            GameOver = true;
            DetermineWinner();
            if (Verbose)
            {
                WriteMessage($"Winner: {Winner}");
                WriteMessage($"Rounds: {Rounds}");
            }
        }

        public void Play()
        {
            while (!GameOver)
            {
                if (Player1.AllDecksEmpty() ||
                    Player2.AllDecksEmpty())
                {
                    EndGame();
                    break;
                }

                if (DeckEmpty())
                {
                    if (Verbose)
                        WriteMessage($"Rounds: {Rounds}");

                    ResetDecks();
                }

                DisplayDeckStatus(Player1);
                DisplayDeckStatus(Player2);
                Draw();

            }
        }

        public void ResetDecks()
        {
            Rounds++;
            DisplayRounds();

            Player1.ResetDeck();
            Player2.ResetDeck();

            if (Verbose)
            {
                Debug.Assert(Player1.CountAllDecks() + Player2.CountAllDecks() == 52);
                Debug.Assert(Player1.CountAllDecks() != 0);
                Debug.Assert(Player2.CountAllDecks() != 0);
            }
        }

        public void WriteMessage(string message, bool ignore = false)
        {
            if (Verbose || ignore)
                Console.WriteLine(message);
        }
    }
}
