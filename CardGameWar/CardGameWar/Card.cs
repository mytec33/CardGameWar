namespace CardGameWar
{
    class Card
    {
        public enum CardRank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace,
        }

        public enum CardSuit
        {
            Diamonds,
            Hearts,
            Clubs,
            Spades
        }

        public CardRank Rank;
        public CardSuit Suit;
        
        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
