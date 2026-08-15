using System;
using Poker.Core;
class Program
{
    static void Main(string[] args)
    {
        Deck.PopulateDeck();
        Deck.ShuffleDeck();
        foreach(Card card in Deck.CardDeck)
        {
            Console.WriteLine(card.Rank + " of " + card.Suit);
        }
        //Console.WriteLine("\n \n \n");
        //Deck.ShuffleDeck();
        //foreach (Card card in Deck.CardDeck)
        //{
        //    Console.WriteLine(card.Rank + " of " + card.Suit);
        //}
    }

}