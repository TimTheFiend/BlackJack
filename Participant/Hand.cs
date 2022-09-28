using BlackJack.BicycleCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Participant
{
    public class Hand
    {
        List<Card> cards;

        private int totalAcesInHand = 0;
        public int handValue { get; private set; }
        private bool CanUseAce = true;

        /* Magic Numbers */
        private const int maxHandValue = 21;  //Magic number
        private const int aceMaxValue = 11;
        private const int aceMinValue = 1;


        public int getTotalHandValue {
            get {
                if (cards.Count == 0) {
                    return 0;
                }
                return CalculateCardValue();
            }
        }

        public Hand() {
            cards = new List<Card>();
        }


        //TODO
        public bool TryAddCard(Card newCard) {
            //Increment Ace counter
            if (newCard.Value == CardValue.Ace) {
                totalAcesInHand++;
            }
            // Scenario: ACE+4+KING
            //   PRIOR TO FLOORING ACE = 25
            //   AFTER FLOORING ACE = 15
            handValue += newCard.getValue;

            //Check if adding the card to hand will bust the player
            //NOTE Need check so this doesn't happen as long as you have 1 ace in hand.
            if (handValue > maxHandValue) {
                if (totalAcesInHand > 0 && IsBustAcePreventable) {

                }
            }

            return false;
        }


        private bool IsBustAcePreventable => handValue - 10 > maxHandValue;


        private int CalculateCardValue() {
            int amountAce = 0;
            int totalValue = 0;
            int aceValue = 11;  // Magic number

            foreach (Card card in cards) {
                if (card.Value == CardValue.Ace) {
                    amountAce++;
                    continue;
                }
                totalValue += card.getValue;
            }

            // If there's at least one Ace in the hand.
            if (amountAce > 0) {
                // In a non-bust hand with >1 Ace, only one can have a value of 11, and the rest are for certain valued at 1.
                if (IsHandBust(totalValue + aceValue + (amountAce - 1))) {
                    //If the ace is valued at 11 the hand is a bust, add the collected value of the aces to totalValue.
                    totalValue += amountAce;
                }
                else {
                    // The hand isn't a bust with one Ace being valued at 11. Add that ace, and the collected remaining aces values to totalValue
                    totalValue += aceValue + amountAce - 1;
                }
            }

            return totalValue;
        }

        private bool IsHandBust(int value) {
            int maxHandValue = 21;
            return value > maxHandValue;
        }
    }
}
