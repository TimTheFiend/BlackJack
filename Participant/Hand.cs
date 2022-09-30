﻿using BlackJack.BicycleCards;
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

        //TODO 
        public string getCardsOnHand {
            get {
                string value = "";
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


        public static bool operator ==(Hand hand, Hand other) {
            return hand.getTotalHandValue == other.getTotalHandValue;
        }

        public static bool operator !=(Hand hand, Hand other) {
            return hand.getTotalHandValue != other.getTotalHandValue;
        }



        public void DebugPrint() {
            ConsoleWriter.WriteCard(cards.ToArray());
        }

        ////NOT IN USE
        //public bool TryAddCard(Card newCard) {
        //    //Increment Ace counter
        //    if (newCard.Value == CardValue.Ace) {
        //        totalAcesInHand++;
        //    }
        //    // Scenario: ACE+4+KING
        //    //   PRIOR TO FLOORING ACE = 25
        //    //   AFTER FLOORING ACE = 15
        //    handValue += newCard.getValue;

        //    //Check if adding the card to hand will bust the player
        //    //NOTE Need check so this doesn't happen as long as you have 1 ace in hand.
        //    if (handValue > maxHandValue) {
        //        if (totalAcesInHand > 0 && IsBustAcePreventable) {

        //        }
        //    }

        //    return false;
        //}

        ////NOT IN USE
        //private bool IsBustAcePreventable => handValue - 10 > maxHandValue;

        ////NOT IN USE
        //private int CalculateCardValue() {
        //    int amountAce = 0;
        //    int totalValue = 0;
        //    int aceValue = 11;  // Magic number

        //    foreach (Card card in cards) {
        //        if (card.Value == CardValue.Ace) {
        //            amountAce++;
        //            continue;
        //        }
        //        totalValue += card.getValue;
        //    }

        //    // If there's at least one Ace in the hand.
        //    if (amountAce > 0) {
        //        // In a non-bust hand with >1 Ace, only one can have a value of 11, and the rest are for certain valued at 1.
        //        if (IsHandBust(totalValue + aceValue + (amountAce - 1))) {
        //            //If the ace is valued at 11 the hand is a bust, add the collected value of the aces to totalValue.
        //            totalValue += amountAce;
        //        }
        //        else {
        //            // The hand isn't a bust with one Ace being valued at 11. Add that ace, and the collected remaining aces values to totalValue
        //            totalValue += aceValue + amountAce - 1;
        //        }
        //    }

        //    return totalValue;
        //}

        //private bool IsHandBust(int value) {
        //    int maxHandValue = 21;
        //    return value > maxHandValue;
        //}
    }
}
