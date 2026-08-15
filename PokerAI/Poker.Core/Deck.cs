using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Poker.Core
{
    public static class Deck
    {
        public static Card[] CardDeck = new Card[52];
        public static void PopulateDeck()
        {
            int i = 0;
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    Card newCard = new Card(rank, suit);
                    CardDeck[i++] = newCard;
                }
            }
        }

        public static void ShuffleDeck()
        {
            for(int i = CardDeck.Length - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i+1);
                Card temp = CardDeck[i];
                CardDeck[i] = CardDeck[j];
                CardDeck[j] = temp;
            }
        }

    }
}
