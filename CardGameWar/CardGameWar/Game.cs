using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameWar
{
    class Game
    {
        private Player Player1;
        private Player Player2;

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
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
                if (Player1.CountDeck() <= 5 ||
                    Player2.CountDeck() <= 5)
                {
                    Console.WriteLine("Not enough cards for war. Each player gets card back.");
                    
                    Player1.AddDrawnCardSideDeck(Player1.Card);
                    Player2.AddDrawnCardSideDeck(Player2.Card);
                    Player1.RemoveDrawnCard();
                    Player2.RemoveDrawnCard();
                }
                else
                {
                    Console.WriteLine("WAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                    Player1.ClearWarDeck();
                    Player2.ClearWarDeck();

                    Player1.AddDrawnCardWarDeck(Player1.Card);
                    Player2.AddDrawnCardWarDeck(Player2.Card);
                    Player1.RemoveDrawnCard();
                    Player2.RemoveDrawnCard();

                    DrawThree();

                    Player1.ClearWarDeck();
                    Player2.ClearWarDeck();
                }
            }
        }

        private void DrawThree()
        {
            // Hiden in from players
            Player1.DrawThree();
            Player2.DrawThree();

            // Shown face up to compare
            Player1.DrawCard();
            Player2.DrawCard();

            if (Player1.Card.Rank > Player2.Card.Rank)
            {
                Console.WriteLine($"{Player1.Name} won the war!");

                Player1.AddDrawnCardWarDeck(Player1.Card);
                Player2.AddDrawnCardWarDeck(Player2.Card);
                Player1.AddWarDeckToSideDeck(Player1);
                Player1.AddWarDeckToSideDeck(Player2);
            }
            else if (Player1.Card.Rank < Player2.Card.Rank)
            {
                Console.WriteLine($"{Player2.Name} won the war!");

                Player1.AddDrawnCardWarDeck(Player1.Card);
                Player2.AddDrawnCardWarDeck(Player2.Card);
                Player2.AddWarDeckToSideDeck(Player1);
                Player2.AddWarDeckToSideDeck(Player2);
            }
            else
            {
                Console.WriteLine("WAR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                Player1.AddDrawnCardWarDeck(Player1.Card);
                Player2.AddDrawnCardWarDeck(Player2.Card);
                Player1.RemoveDrawnCard();
                Player2.RemoveDrawnCard();

                DrawThree();
            }

            // Cards that broke the tie
            Player1.RemoveDrawnCard();
            Player2.RemoveDrawnCard();
        }

        public void DisplayDeckStatus(Player player)
        {
            Console.WriteLine($"{player.Name} - Cards: {player.CountDeck()} " +
                $"side deck: {player.CountSideDeck()} " +
                $"war deck: {player.CountWarDeck()}");
        }


    }
}
