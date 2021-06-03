using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CardGameWar
{

    class CardDeck: IEnumerable
    {
        private List<Card> Deck;
        public string Name { get; private set; }

        public Card Card { get; private set; }

        public CardDeck(string name)
        {
            Deck = new List<Card>();
            Name = name;
        }

        public void ClearDeck()
        {
            Deck.Clear();
        }

        public int CountCards()
        {
            return Deck.Count;
        }

        public void AddCard(Card card)
        {
            Deck.Add(card);
        }

        public void AddCard(CardDeck cards)
        {
            Deck.AddRange(cards.Deck);
        }

        public Card DrawCard()
        {
            if (Deck.Count <= 0)
            {
                throw new Exception($"{Name}: deck empty");
            }

            return Deck[0];
        }

        public void GenerateFullDeck()
        {
            foreach (Card.CardRank r in Enum.GetValues(typeof(Card.CardRank)))
            {
                foreach (Card.CardSuit s in Enum.GetValues(typeof(Card.CardSuit)))
                {
                    Deck.Add(new Card(r, s));
                }
            }
        }

        public void RemoveCard(Card card)
        {
            Deck.Remove(card);
        }

        public void RemoveDrawnCard()
        {
            Deck.Remove(Card);
        }

        public void Shuffle()
        {
            //  Based on Java code from wikipedia:
            //  http://en.wikipedia.org/wiki/Fisher-Yates_shuffle

            Random r = new();
            for (int n = Deck.Count - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                Card temp = Deck[n];
                Deck[n] = Deck[k];
                Deck[k] = temp;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            foreach(Card card in Deck)
            {
                sb.AppendLine(card.ToString());
            }

            return sb.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            foreach(Card card in Deck)
            {
                yield return card;
            }
        }
    }
}
