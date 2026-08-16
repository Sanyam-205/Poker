using System.Net.Security;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Poker.Core
{
    // The goal is to create a struct that represents a deck of cards
    // Even though a 2+2 approach would be better for the evaluator I am building (7 card), I am going to change the classic 5 card evaluator (cactus kev) to achieve this
    // My aim with doing so is to learn about the 32 bit encoding of cards and sice I used this same technique in my chess engine, I feel like I will be more comfortable with this

    // Here's how the encoding works:
    // Each card is represented by a 32 bit integer. These 32 bits include all the information about the cards.
    // The first 8 bits represent the prime number linked with the rank of the card.Each rank is assigned a unique prime number (2,3,5,7, 11, 13, 17, 19, 23, 29, 31, 37, 41)
    // The next 4 bits represent the rank of the card (2-14, where 2 = 2, 3 = 3, ..., 11=Jack, 12=Queen, 13=King, 14=Ace)
    // The next 4 bits represent the suit of the card (0-3, where 0=Clubs, 1= Diamonds, 2=Hearts, 3=Spades)
    // The next 13 bits represent the rank of the card in a bitmask format.This allows for easy comparison of hands and detection of flushes and straights.
    // Rank mask - 
    /*
    2 -  0000000000001
    3 -  0000000000010
    4 -  0000000000100
    5 -  0000000001000
    6 -  0000000010000
    7 -  0000000100000
    8 -  0000001000000
    9 -  0000010000000
    10 - 0000100000000
    J -  0001000000000
    Q -  0010000000000
    K -  0100000000000
    A -  1000000000000
     */
    // Rest of the bits are unused. These bits are added to accomodate for cpu cycles as cpu cycles are faster when the data is aligned to 32 bits. This is a technique used in chess engines as well.

    // Here is an example of how cards will be represented
    // Ace of spades: 000(unused bits) 1000000000000(rank mask) 0011(3 - suit) 1110(14 - rank) 00101001(41 - prime number for ace)
    // Ace of spades: 000 1000000000000 0011 1110 00101001

    public readonly struct Card
    {
        public static readonly int suitMask = 0b00000000000000001111000000000000;
        public static readonly int rankMask = 0b00000000000000000000111100000000;
        public static readonly int primeMask = 0b00000000000000000000000011111111;
        public static readonly int[] primes = {0, 0, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41}; // Array of prime numbers for each rank. 
        private static readonly int[] rankToRankBitmask = { 0, 0, 1 << 0, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8, 1 << 9, 1 << 10, 1 << 11, 1 << 12 }; 
        public int Value { get; }
        // A value of integer type that can be assesed by anyone but can only be assigned once by the struct constructor. This value is immutable once set.

        public Card(Rank rank, Suit suit) // constructor. This is what will allow us to use the struct to create a card.
        {
            Value = (rankToRankBitmask[(int)rank] << 16 | (int)rank << 8 | (int)suit << 12 | primes[(int)rank]);
            // The struct defines each card as one 32 bit integer. This is the card's 'value'. 
            // To set this value, we use bitwise OR operator and left shift operator to se the bits of rank and suit. We OR it with the primes array to set the prime number corresponding to that rank. The primes array is indexed by the rank of the card. 
            // Lastly, we create a bitmask for the rank by left shifting 1 by the rank of the card and left shifting that by 16.
            // For an ace of spades, these individual values shifted by corresponding positions would look like this:
            // ace = 14 ==> 00000000000000000000111000000000
            // spade = 3 ==> 00000000000000000011000000000000
            // prime = 41 ==> 00000000000000000000000000101001
            // rank bitmask = 00010000000000000000000000000000
            // OR-ing these values gives us the final value of the card = 00010000000000000011111000101001
        }

        //Bit masking. Did it extensively in Arbor. Self explanatory I'd say
        public Rank Rank => (Rank)((Value & rankMask) >> 8); // Right shifting by eight gives us the exact rank
        public Suit Suit => (Suit)((Value & suitMask) >> 12); // Right shifting by twelve gives us the exact suit
        public int Prime => Value & primeMask;

        public int RankBitMask => (1 << (int)Rank) >> 16;

    }

    public enum Rank : byte // By default, enums are of integer type. That is, each element of the enum takes up 4 bytes. By using : byte, we cast the enum to byte type, meaning each element of the enum will take up 1 byte. The consquenses of doing this are that the enum takes much less space. For 100 elements, it will take 100 bytes of space compared to 400 but we will only be able to store 256 elements since byte has maximum range of 0 - 255 compared to 2.14b for integer. 
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eigth = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14

        // We set the values to their corresponding integer values so we can get them cleanly with indexed based. We do the same for suit.

    }

    public enum Suit : byte
    { 
        Club = 0,
        Diamond = 1,
        Heart = 2,
        Spade = 3
    }

}
