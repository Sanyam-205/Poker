using System;
using System.Numerics;
using System.IO;
using Poker.Core;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        //Deck.PopulateDeck();
        //Deck.ShuffleDeck();

        //Deck.DealCards(25);
        //foreach(Card card in Deck.dealtCards)
        //{
        //    Console.WriteLine(card.Rank + " of " + card.Suit);
        //}
        //Console.WriteLine("\n\n\n Remaining deck -");
        //foreach (Card card in Deck.CardDeck)
        //{
        //    Console.WriteLine(card.Rank + " of " + card.Suit + " Card Value = " + card.Value);
        //}
        //Console.WriteLine("MSB for A High: " + BitOperations.LeadingZeroCount(0b00011111000000000000000000000000 >> 16));
        //Console.WriteLine("MSB for K High: " + BitOperations.LeadingZeroCount(0b00001111100000000000000000000000 >> 16));
        //Console.WriteLine("MSB for Q High: " + BitOperations.LeadingZeroCount(0b00000111110000000000000000000000 >> 16));
        //Console.WriteLine("MSB for J High: " + BitOperations.LeadingZeroCount(0b00000011111000000000000000000000 >> 16));
        //Console.WriteLine("MSB for T High: " + BitOperations.LeadingZeroCount(0b00000001111100000000000000000000 >> 16));
        //Console.WriteLine("MSB for 9 High: " + BitOperations.LeadingZeroCount(0b00000000111110000000000000000000 >> 16));
        //Console.WriteLine("MSB for 8 High: " + BitOperations.LeadingZeroCount(0b00000000011111000000000000000000 >> 16));
        //Console.WriteLine("MSB for 7 High: " + BitOperations.LeadingZeroCount(0b00000000001111100000000000000000 >> 16));
        //Console.WriteLine("MSB for 6 High: " + BitOperations.LeadingZeroCount(0b00000000000111110000000000000000 >> 16));
        //Console.WriteLine("MSB for 5 High: " + BitOperations.LeadingZeroCount(0b00010000000011110000000000000000 >> 16));

        /*
         "MSB for A High: " + 
         "MSB for K High: " + 
         "MSB for Q High: " + 
         "MSB for J High: " + 
         "MSB for T High: " + 
         "MSB for 9 High: " + 
         "MSB for 8 High: " + 
         "MSB for 7 High: " + 
         "MSB for 6 High: " + 
         "MSB for 5 High: " + 
         */


        //Console.WriteLine(BitOperations.TrailingZeroCount((0b00000000000111110000000000000000) ));
        //Console.WriteLine("\n \n \n");
        //Deck.ShuffleDeck();
        //foreach (Card card in Deck.CardDeck)
        //{
        //    Console.WriteLine(card.Rank + " of " + card.Suit);
        //}

        /*
            Two of Heart
            10100001000000010
            Four of Heart
            1000100010000000101
            Three of Club
            100001001100000011
            Three of Spade
            101000001100000011
            Three of Heart
            100100001100000011
         */




        //Deck.DealCustomCards(5);
        //////Deck.DealCards(5);
        //foreach (Card card in Deck.customDealtCards)
        //{
        //    Console.WriteLine(card.Rank + " of " + card.Suit);
        //}
        //////Card card1 = Deck.dealtCards[0];
        //////Console.WriteLine(card1.Suit);
        //Console.WriteLine(HandEvaluator.EvaluateHand(Deck.customDealtCards));


        //Card twoOfClubs = new Card(Rank.Two, Suit.Club);
        //Console.WriteLine($"Two of Clubs value = {Convert.ToString(twoOfClubs.Value , 2)}");

        //Console.WriteLine((1 << (int)2 - 2) << 16);
        //Console.WriteLine(Convert.ToString((1 << (int)2 - 2) << 16), 2);

        #region logging
        Deck.PopulateDeck();
        for (int i = 5001; i < 10001; i++)
        {
            Deck.ShuffleDeck();
            Deck.DealCards(5);
            string path = @"D:\Poker\PokerAI\Poker.Core\TestResult.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (Card card in Deck.dealtCards)
                {
                    writer.WriteLine(card.Rank + " of " + card.Suit);
                }
                writer.WriteLine($"Result of hand {i}: {HandEvaluator.EvaluateHand(Deck.dealtCards)} \n");
            }
        }
        Console.WriteLine("Test finished!");
        Console.ReadKey();
        #endregion


    }

}