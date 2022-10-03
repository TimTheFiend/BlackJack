using BlackJack.BicycleCards;
using BlackJack.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BlackJack.Participant
{
    //TODO: refactor and reconsider access modifiers
    public class Hand
    {
        List<Card> cards;

        public List<Card> getHand => cards;

        public int handValue { get; private set; }
        public int handSize => cards.Count;

        /* Magic Numbers */
        private const int aceMaxValue = 11;
        private const int aceMinValue = 1;
        private const int maxHandValue = 21;
        
        /* CONSTRUCTOR */
        public Hand() {
            cards = new List<Card>();
        }

        /* PROPERTIES */
        public int getTotalHandValue {
            get {
                int totalValue = 0;
                foreach (Card card in cards.OrderByDescending(x => x.Value)) {
                    if (!card.IsFaceUp) {
                        continue;
                    }
                    if (card.Value == CardValue.Ace) {
                        totalValue += totalValue > 10 ? aceMinValue : aceMaxValue;
                        continue;
                    }
                    totalValue += card.getValue;
                }

                return totalValue;
            }
        }

        public bool isBust => getTotalHandValue > maxHandValue;

        private bool canPerformAction => cards.Count == 2;

        /// <summary>
        /// Returns <c>true</c> if hand contains an <see cref="CardValue.Ace"/> and a card with a value of 10;
        /// Otherwise returns <c>false</c>.
        /// </summary>
        public bool hasBlackJack => canPerformAction && getTotalHandValue == 21;

        public bool CanSplitPairs => canPerformAction && cards[0].Value == cards[1].Value;

        public bool CanDoubleDown => canPerformAction && getTotalHandValue > 8 && getTotalHandValue < 15;

        public Card GetSplitPair() {
            //ASSUME THEY CAN
            Card splitPair = cards[1];
            cards.Remove(splitPair);
            return splitPair;
        }

        //TODO 
        public string getCardsOnHand {
            get {
                string value = $"({getTotalHandValue}) - ";
                foreach (Card card in cards) {
                    value += String.Format("[{0}]", card.getSuitAndValue);
                }
                return value;
            }
        }

        public void TurnCardFaceUp() {
            var card = cards[0];
            cards[0] = card.SetFaceUp();
        }

        public void AddCard(Card newCard) {
            cards.Add(newCard);
        }

        public static bool operator >(Hand hand, Hand other) {
            return hand.getTotalHandValue > other.getTotalHandValue;
        }

        public static bool operator <(Hand hand, Hand other) {
            return hand.getTotalHandValue > other.getTotalHandValue;
        }



    }
}
