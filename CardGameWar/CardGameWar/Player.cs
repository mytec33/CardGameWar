namespace CardGameWar
{
    class Player
    {
        private CardDeck Deck;
        private CardDeck SideDeck;
        private CardDeck WarDeck;

        public Card Card;
        public string Name;

        public Player(string name)
        {
            Name = name;

            Deck = new CardDeck(name);
            Deck.Shuffle();

            SideDeck = new CardDeck(name);
            SideDeck.ClearDeck();
            WarDeck = new CardDeck(name);
            WarDeck.ClearDeck();
        }

        public void AddDrawnCardSideDeck(Card card)
        {
            SideDeck.AddCard(card);
        }

        public void AddDrawnCardWarDeck(Card card)
        {
            WarDeck.AddCard(card);
        }

        public void AddSideDeckToDeck()
        {
            Deck.AddCard(SideDeck);
        }

        public void AddWarDeckToSideDeck(Player player)
        {
            SideDeck.AddCard(player.WarDeck);
        }

        public bool AllDecksEmpty()
        {
            if (Deck.CountCards() == 0 &&
                SideDeck.CountCards() == 0 &&
                WarDeck.CountCards() == 0)
                return true;

            return false;
        }

        public int CountDeck()
        {
            return Deck.CountCards();
        }

        public string CountSideDeck()
        {
            return SideDeck.CountCards().ToString();
        }

        public string CountWarDeck()
        {
            return WarDeck.CountCards().ToString();
        }

        public void DrawCard()
        {
            Card = Deck.DrawCard();
        }

        public void DrawThree()
        {
            for(int x = 1; x <= 3; x++)
            {
                DrawCard();
                RemoveDrawnCard();
                WarDeck.AddCard(Card);
            }
        }

        public void RemoveDrawnCard()
        {
            Deck.RemoveCard(Card);
        }

        public void ResetDeck()
        {
            AddSideDeckToDeck();
            SideDeck.ClearDeck();
            Deck.Shuffle();
        }

        public void ClearWarDeck()
        {
            WarDeck.ClearDeck();
        }
    }
}
