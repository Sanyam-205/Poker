using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Core
{
    // Royal Flush
    // Straight Flush
    // Four of a Kind
    // Full House
    // Flush
    // Straight
    // Three of a kind
    // Two Pair
    // One Pair
    // High card

    // Royal Flush, Straight Flush, Flush all require us to check if all the cards are of the same suit.
    // Royal Flush, Straight Flush, Straight all require us to check if the cards are in a sequence.
    // Four of a Kind, Full House, Three of a Kind, Two Pair, One Pair all require us to check if there are any duplicate cards.

    // To check for same suit, we can use the bitwise AND operator on the suit value of each card.
    // To check for a sequence, we can use the bitwise AND operator on the bitwise rank mask value of each card.
    // To check for duplicates, we can use the bitwise AND operator on the prime value of each card.

    // 3 bits unused, 13 bits for rank mask, 4 bits for suit, 4 bits for rank, 8 bits for prime number. 
    public class HandEvaluator
    {
        private static readonly ulong[] straightMask = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            0b1111100000000, // Ace High
            0b111110000000, // King High
            0b11111000000, // Queen High
            0b1111100000, // Jack High
            0b111110000, // 10 High
            0b11111000, // 9 High
            0b1111100, // 8 High
            0b111110, // 7 High
            0b11111, // 6 High
            0b1000000001111  // 5 High or Ace low
        };  
        public static void EvaluateHand(Card[] hand)
        {
            int sameSuit = hand[0].Value & hand[1].Value & hand[2].Value & hand[3].Value & hand[4].Value & Card.suitMask;
            // when doing hand[0].value & hand[1].value & Card.suitMask, the bitwise AND operator will return a value that has the same suit bits set to one. If all 5 cards are of the same suit lets say spades, then sameSuit will be equal to 00000000000000000011000000000000. If the cards are of different suits, then sameSuit will be equal to 0. 
            // We use this to check in one cpu cycle if there is a flush or not. The same flush check will be useful for straight flush and royal flush.

            // The 13 bits long rank bitmnask is used to find a sequence. For rank 2, the rank bitmask is 0000000000001, 3 is 0000000000010, 4 is 0000000000100 and so on. So for A, it will be 10000000000000, K will be 0100000000000, Q will be 00100000000000 and J will be 00010000000000. 
            // So to check a sequence, we can use the bitwise OR operator on the rank bitmask of each card in the hand. Since currently we are working with 5 cards, if those 5 cards are in sequence then we should get a value where there are 5 consecutive bits set to 1.
            // For example, if the hand is 2,3,4,5,6 then the OR-ing the rank bitmask will give us 0000000011111. 
            // There is an edge case of Ace-Low. In this a straight is formed by A-2-3-4-5. In this case, the rank bitmask will be 1000000001111. So we will have to perform a separate check for this.

            //int rankCheck = (hand[0].Value & hand[1].Value & hand[2].Value & hand[3].Value & hand[4].Value) >> 16; // We right shift by 16 to remove the bits we don't need for rankCheck.

            int rankCheck = hand[0].Value >> 16 | hand[1].Value >> 16 | hand[2].Value >> 16 | hand[3].Value >> 16 | hand[4].Value >> 16; 
            // We right shift by 16 to remove the bits we don't need for rankCheck. We use bitwise OR operator to combine the rank bitmask of each card in the hand. If the result is not 0, then we have a sequence. If the result is 0, then we don't have a sequence.
            
            
            
            //Console.WriteLine($"hand 0 value = {Convert.ToString(hand[0].Value >> 16, 2)}");
            //Console.WriteLine($"hand 1 value = {Convert.ToString(hand[1].Value >> 16, 2)}");
            //Console.WriteLine($"hand 2 value = {Convert.ToString(hand[2].Value >> 16, 2)}");
            //Console.WriteLine($"hand 3 value = {Convert.ToString(hand[3].Value >> 16, 2)}");
            //Console.WriteLine($"hand 4 value = {Convert.ToString(hand[4].Value >> 16, 2)}");

            //Console.WriteLine($"Rank Check Value : {Convert.ToString(rankCheck, 2)}");



            // to check for a sequence, we can use the bitwise AND operator on the rankCheck value and the straightFlushes array. If the result is not 0, then we have a straight or a straight flush. If the result is 0, then we don't have a straight or a straight flush.

            if(rankCheck != 0) // we have a straight or a straight flush. We will check for flush later.
            {
                //Console.WriteLine("Straight I think");

                int MSBValue = System.Numerics.BitOperations.LeadingZeroCount((uint)rankCheck); // We use leading zero count to find most significant bit set to 1. For Ace high, this is 19, K-high is 20, Q-high is 21...., 6-high is 27 and ace-low/5-high is also 19. We will have a separate check for ace low.
                // The straightMask array is indexed by the corresponding MSB value for each of the 10 possible straights, so we can use that value directly as index and achieve O(1) time complexity compared to O(n) for a for loop.

                if(MSBValue != 0) // standard safety check
                {
                    if (MSBValue != 19 && ((ulong)rankCheck == straightMask[MSBValue]))
                    {
                        // We have a straight. Do something
                        Console.WriteLine("Straight found with MSB value: " + MSBValue);

                    }
                    // Separate check for ACE
                    else if (MSBValue == 19 && (ulong)rankCheck == straightMask[19]) // Ace has MSB of 19 on two occassions
                    {
                        Console.WriteLine("Ace high Straight found");
                        // ACE HIGH STRAIGHT
                    }
                    else if (MSBValue == 19 && (ulong)rankCheck == straightMask[28]) // Ace has MSB of 19 on two occassions
                    {
                        Console.WriteLine("Ace low Straight found");
                        // ACE LOW STRAIGHT
                    }
                    
                }

            }


            

        }

    }
}
