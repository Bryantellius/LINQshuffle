using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFaroShuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a single sequence from combining the first sequence with the second sequence 
            // Written in query syntax
            var startingDeck = (from s in Suits().LogQuery("Suit Generation") from r in Ranks().LogQuery("Rank Generation") select new { Suit = s, Rank = r}).LogQuery("Starting Deck").ToArray();
            // var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));
            // Can be written in method syntax ^^

            var shuffle = startingDeck;

            int numShuffles = 0;

            do
            {
                // Considered an "in" shuffle
                shuffle = shuffle.Skip(26).LogQuery("Bottom Half").InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top half")).LogQuery("Shuffle").ToArray();

                numShuffles++;
            }
            while(!startingDeck.SequenceEqual(shuffle));

            System.Console.WriteLine($"shuffled {numShuffles} times until equal to starter deck.");
        }
        static IEnumerable<string> Suits(){
            yield return "Clubs";
            yield return "Diamonds";
            yield return "Hearts";
            yield return "Spades";
        }
        static IEnumerable<string> Ranks(){
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
