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

        private readonly string _faceDownReturnString = "¿?";
        private const ConsoleColor _colorBlack = ConsoleColor.DarkBlue;
        private const ConsoleColor _colorRed = ConsoleColor.DarkRed;
        private const ConsoleColor _colorFaceDown = ConsoleColor.DarkYellow;
        private const int _aceFullValue = 11;

        public Card(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
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
                // Ace full value.
                return _aceFullValue;
            }
        }

        public Card SetFaceDown() {
            IsFaceUp = false;
            return this;
        }

        public Card SetFaceUp() {
            IsFaceUp = true;
            return this;
        }

        #region Console Print properties
        public string getSuitAndValue => IsFaceUp ? $"{GetSuitUnicode}{GetValueChar}" : _faceDownReturnString;

        public string getValueAndSuit => IsFaceUp ? $"{GetValueChar}{GetSuitUnicode}" : _faceDownReturnString;


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

        public ConsoleColor getColor {
            get {
                if (!IsFaceUp) {
                    return _colorFaceDown;
                }
                return (int)Suit % 2 == 0 ? _colorBlack : _colorRed;
            }
        }

        #endregion
    }
}
