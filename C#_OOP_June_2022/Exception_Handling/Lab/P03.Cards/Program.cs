using System;
using System.Collections.Generic;
using System.Linq;

namespace P03.Cards
{
    internal class Program
    {
        private static string[] validFaces = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        private static string[] validSuits = new string[] { "S", "H", "D", "C" };

        static void Main()
        {
            var deck = new List<Card>();

            string[] cardsInput = Console.ReadLine().Split(", ");

            foreach (var cardStr in cardsInput)
            {
                string face = cardStr.Split()[0];
                string suit = cardStr.Split()[1];

                try
                {
                    var card = CreateCard(face, suit);

                    deck.Add(card);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            Console.WriteLine(string.Join(" ", deck));
        }

        private static Card CreateCard(string face, string suit)
        {
            Card card;

            if (!validFaces.Contains(face) || !validSuits.Contains(suit))
                throw new ArgumentException("Invalid card!");
            else
                card = new Card(face, suit);

            return card;
        }
    }

    class Card
    {
        private readonly Dictionary<string, string> graphics = new Dictionary<string, string> 
        { 
            { "S", "\u2660" }, 
            { "H", "\u2665" },
            { "D", "\u2666" },
            { "C", "\u2663" }
        };

        public Card(string face, string suit)
        {
            this.Face = face;
            this.Suit = suit;
        }

        public string Face { get; }
        public string Suit { get; }

        public override string ToString()
        {
            return $"[{this.Face}{graphics[this.Suit]}]";
        }
    }
}
