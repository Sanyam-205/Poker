using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Poker.Core
{
    public static class Deck
    {
        public static int numberofCards = 5;
        public static Card[] dealtCards = new Card[numberofCards];
        public static Card[] customDealtCards = new Card[numberofCards];
        public static Card[] CardDeck = new Card[52];
        private static int cardDeckIndex = 0;
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
            cardDeckIndex = 0; //reset the card deck index pointer to 0 whenever shuffle deck is called.
            for (int i = CardDeck.Length - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(i + 1);
                Card temp = CardDeck[i];
                CardDeck[i] = CardDeck[j];
                CardDeck[j] = temp;
            }
        }

        public static void DealCards(int numberOfCards)
        {
            //    dealtCards = new Card[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
            {
                dealtCards[i] = CardDeck[cardDeckIndex++];

                //Array.Copy(CardDeck, i + 1, CardDeck, i, CardDeck.Length - (i + 1));

            }
        }

        public static void DealCustomCards(int numberOfCustomCards)
        {
            Card customCard1 = new Card(Rank.Ten,   Suit.Club);
            Card customCard2 = new Card(Rank.Jack,  Suit.Club);
            Card customCard3 = new Card(Rank.Queen, Suit.Club);
            Card customCard4 = new Card(Rank.Nine,  Suit.Club);
            Card customCard5 = new Card(Rank.King,  Suit.Club);

            customDealtCards[0] = customCard1;
            customDealtCards[1] = customCard2;
            customDealtCards[2] = customCard3;
            customDealtCards[3] = customCard4;
            customDealtCards[4] = customCard5;

        }











    }
}
/*
Two of Club Card Value =      262658
Three of Club Card Value =    525059
Four of Club Card Value =     1049605
Five of Club Card Value =     2098439
Six of Club Card Value =      4195851
Seven of Club Card Value =    8390413
Eigth of Club Card Value =    16779281
Nine of Club Card Value =     33556755
Ten of Club Card Value =      67111447
Jack of Club Card Value =     134220573
Queen of Club Card Value =    268438559
King of Club Card Value =     536874277
Ace of Club Card Value =      1073745449
Two of Diamond Card Value =   266754
Three of Diamond Card Value = 529155
Four of Diamond Card Value =  1053701
Five of Diamond Card Value =  2102535
Six of Diamond Card Value =   4199947
Seven of Diamond Card Value = 8394509
Eigth of Diamond Card Value = 16783377
Nine of Diamond Card Value =  33560851
Ten of Diamond Card Value =   67115543
Jack of Diamond Card Value =  134224669
Queen of Diamond Card Value = 268442655
King of Diamond Card Value =  536878373
Ace of Diamond Card Value =   1073749545
Two of Heart Card Value =     270850
Three of Heart Card Value =   533251
Four of Heart Card Value =    1057797
Five of Heart Card Value =    2106631
Six of Heart Card Value =     4204043
Seven of Heart Card Value =   8398605
Eigth of Heart Card Value =   16787473
Nine of Heart Card Value =    33564947
Ten of Heart Card Value =     67119639
Jack of Heart Card Value =    134228765
Queen of Heart Card Value =   268446751
King of Heart Card Value =    536882469
Ace of Heart Card Value =     1073753641
Two of Spade Card Value =     274946
Three of Spade Card Value =   537347
Four of Spade Card Value =    1061893
Five of Spade Card Value =    2110727
Six of Spade Card Value =     4208139
Seven of Spade Card Value =   8402701
Eigth of Spade Card Value =   16791569
Nine of Spade Card Value =    33569043
Ten of Spade Card Value =     67123735
Jack of Spade Card Value =    134232861
Queen of Spade Card Value =   268450847
King of Spade Card Value =    536886565
Ace of Spade Card Value =     1073757737
                              
 */


