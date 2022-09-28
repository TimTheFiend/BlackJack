using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Deck
    {
        public List<Card> cards { get; private set; }

        public Deck() {
            GenerateDeck();
        }

        public void GenerateDeck() {
            cards = new List<Card>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit))) {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue))) {
                    cards.Add(new Card(suit, value));
                }
            }
            ShuffleDeck();
        }

        /// <summary>
        /// Fisher-Yates Shuffle algorithm.
        /// </summary>
        public void ShuffleDeck() {
            Random random = new Random();

            for (int i = 0; i < cards.Count - 1; i++) {
                int pos = random.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[pos];
                cards[pos] = temp;
            }
        }

        public Card DrawCard() {
            const int pos = 0;
            Card drawnCard = cards.ElementAt(pos);
            cards.RemoveAt(pos);

            return drawnCard;
        }


        public void PrintDebug() {
            Console.WriteLine("Generating Deck");
            GenerateDeck();
            Console.WriteLine("Number of cards:\t" + cards.Count);

            List<CardPrintout> printouts = new List<CardPrintout>();
            foreach (Card card in this.cards) {
                ConsoleWriter.WriteCard(card);
            }
        }
    }
}
