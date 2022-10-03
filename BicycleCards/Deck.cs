using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Printing;

namespace BlackJack.BicycleCards
{
    public class Deck
    {
        public List<Card> cards { get; private set; }
        private int cardsLeft => cards.Count;

        /// <summary>
        /// <em>"The six-deck game (312 cards) is the most popular" [..]</em><br></br>Makes it feel more fair.
        /// </summary>
        private readonly int amountOfDecks = 6;
        
        /// <summary>
        /// Returns true if <see cref="cards"/> size goes below half the original amount.
        /// </summary>
        public bool isReshuffleNeeded => cardsLeft < (52 * amountOfDecks) / 2;  //Value is arbitrary.



        public Deck()
        {
            GenerateDeck(amountOfDecks);
        }

        /// <summary>
        /// Initialises <see cref="cards"/>, and adds a standard deck of <see cref="Card"/>-object with a <see cref="CardValue"/> and <see cref="CardSuit"/>.
        /// </summary>
        private void GenerateDeck(int numberOfDecks = 1) {
            cards = new List<Card>();

            for (int i = 0; i < numberOfDecks; i++) {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit))) {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue))) {
                        cards.Add(new Card(suit, value));
                    }
                }
            }

        }

        /// <summary>
        /// Fisher-Yates Shuffle algorithm.
        /// </summary>
        public void ShuffleDeck()
        {
            Random random = new Random();

            for (int i = 0; i < cards.Count - 1; i++)
            {
                int pos = random.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[pos];
                cards[pos] = temp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReshuffleDeck() {
            GenerateDeck();
            //ShuffleDeck();
        }

        /// <summary>
        /// Draws a <see cref="Card"/> from itself and removes it, updates the <see cref="Console"/>.
        /// </summary>
        /// <returns>The <em>top</em> card.</returns>
        public Card DrawCard()
        {
            const int pos = 0;
            Card drawnCard = cards.ElementAt(pos);
            cards.RemoveAt(pos);

            UICardDrawer.DrawDeckSize(cardsLeft);

            return drawnCard;
        }


    }
}
