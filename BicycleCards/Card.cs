using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Printing;

namespace BlackJack.BicycleCards
{
    public class Card
    {
        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }
        public bool IsFaceUp { get; private set; } = true;

        private readonly string faceDownReturnString = "¿?";

        public Card(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        // Used exclusively for the first draw the dealer gets.
        public Card(CardSuit suit, CardValue value, bool isFaceUp) : this(suit, value)
        {
            IsFaceUp = isFaceUp;
        }

        /// <summary>
        /// Returns the value of the card, based on the rules of Black Jack.
        /// </summary>
        public int getValue
        {
            get
            {
                if (Value >= CardValue.Ten)
                {
                    return (int)CardValue.Ten;
                }
                else if (Value >= CardValue.Two)
                {
                    return (int)Value;
                }
                return 11;
            }
        }



        #region Console Print properties
        // Don't know how much - if at all - this is gonna be used.
        public override string ToString()
        {
            return IsFaceUp ? $"{Value} of {Suit}" : "X of X";
            //return $"{this.Value} of {this.Suit}";
        }

        public string getSuitAndValue => IsFaceUp ? $"{GetSuitUnicode}{GetValueChar}" : faceDownReturnString;

        public string getValueAndSuit => IsFaceUp ? $"{GetValueChar}{GetSuitUnicode}" : faceDownReturnString;


        private string GetSuitUnicode
        {
            get
            {
                switch (Suit)
                {
                    case CardSuit.Hearts:
                        return "♥";
                    case CardSuit.Spades:
                        return "♠";
                    case CardSuit.Diamonds:
                        return "♦";
                    case CardSuit.Clubs:
                        return "♣";
                    default:
                        throw new Exception("Card-object does not have a suit");
                }
            }
        }

        private string GetValueChar
        {
            get
            {
                if (Value > CardValue.Ace && Value < CardValue.Jack)
                {
                    var value = (int)Value;
                    return value.ToString();
                }
                return Value.ToString().Substring(0, 1);
            }
        }

        public ConsoleColor getColor => (int)Suit % 2 == 0 ? CardPrintout.CardBlack : CardPrintout.CardRed;
        #endregion

        //REMOVE
        public static int CalculateCardValue(params Card[] cards) {
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

        //REMOVE
        private static bool IsHandBust(int value) {
            int maxHandValue = 21;
            return value > maxHandValue;
        }

        //REMOVE
        private static bool IsHandBust(int totalValue, Card card, out int _totalValue) {
            //Avoid magic number
            int maxHandValue = 21;
            //mutable variable
            int value = card.getValue;


            if (card.Value == CardValue.Ace) {
                //If Ace(11) is bigger than 21
                if (totalValue + value > maxHandValue) {
                    value = 1;
                }
            }
            _totalValue = totalValue + value;
            return _totalValue > maxHandValue;
        }

        // TODO 
        // Extreme example: A;A;A;A;2;2;2;2;3;3;3; 1*4 + 2*4 + 3*3 = 21
        private static int CalculateCardValueAces(int totalValue, List<Card> aces) {
            if (aces.Count == 1) {

            }

            foreach (Card ace in aces) {
                if (totalValue + ace.getValue <= 21) {

                }
            }
            return 0;
        }
    }
}
