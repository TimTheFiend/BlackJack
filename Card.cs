using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Card
    {
        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }
        public bool IsFaceUp { get; private set; } = true;

        private readonly string faceDownReturnString = "¿?";

        public Card(CardSuit suit, CardValue value) {
            Suit = suit;
            Value = value;
        }

        public Card(CardSuit suit, CardValue value, bool isFaceUp) : this(suit, value) {
            IsFaceUp = isFaceUp;
        }


        public int getValue {
            get {
                if (Value >= CardValue.Ten) {
                    return ((int)CardValue.Ten);
                }
                else if (Value >= CardValue.Two) {
                    return (int)Value;
                }
                return 11;

                //switch (Value) {
                //    case CardValue.Ace:
                //        return 11;
                //    case CardValue.Two:
                //    case CardValue.Three:
                //    case CardValue.Four:
                //    case CardValue.Five:
                //    case CardValue.Six:
                //    case CardValue.Seven:
                //    case CardValue.Eight:
                //    case CardValue.Nine:
                //        return ((int)Value);
                //    case CardValue.Ten:
                //    case CardValue.Jack:
                //    case CardValue.Queen:
                //    case CardValue.King:
                //        return ((int)CardValue.Ten);
                //    default:
                //        throw new Exception("Card-object does not have a value.");
                //}
            }
        }


        #region Console Print properties
        // Don't know how much - if at all - this is gonna be used.
        public override string ToString() {
            return IsFaceUp ? $"{this.Value} of {this.Suit}" : "X of X";
            //return $"{this.Value} of {this.Suit}";
        }

        public string getSuitAndValue => IsFaceUp ? $"{GetSuitUnicode}{GetValueChar}" : faceDownReturnString;

        public string getValueAndSuit => IsFaceUp ? $"{GetValueChar}{GetSuitUnicode}" : faceDownReturnString;


        private string GetSuitUnicode {
            get {
                switch (Suit) {
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

        private string GetValueChar {
            get {
                if (Value > CardValue.Ace && Value < CardValue.Jack) {
                    var value = ((int)Value);
                    return value.ToString();
                }
                return Value.ToString().Substring(0, 1);
            }
        }

        public ConsoleColor getColor => (int)Suit % 2 == 0 ? CardPrintout.CardBlack : CardPrintout.CardRed;
        #endregion
    }
}
